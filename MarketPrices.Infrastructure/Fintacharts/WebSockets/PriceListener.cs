using MarketPrices.Application.Abstracts;
using MarketPrices.Infrastructure.Fintacharts.Price;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.WebSockets;
using System.Text;

namespace MarketPrices.Infrastructure.Fintacharts.WebSockets
{
    internal class PriceListener : BackgroundService, IPriceSubscription
    {
        private const string WEB_SOCKET_URL = "wss://platform.fintacharts.com/api/streaming/ws/v1/realtime";

        private static readonly TimeSpan StaleTtl = TimeSpan.FromMinutes(10);
        private static readonly TimeSpan CleanupInterval = TimeSpan.FromMinutes(5);

        private ClientWebSocket? _wsClient;
        private readonly IPriceStore _priceStore;
        private readonly ILogger<PriceListener> _logger;

        public PriceListener(IPriceStore priceStore, ILogger<PriceListener> logger)
        {
            _priceStore = priceStore;
            _logger = logger;
        }

        public async Task StartSubscription(Guid instrumentId)
        {
            _priceStore.Touch(instrumentId.ToString());

            if (_wsClient?.State is not WebSocketState.Open)
            {
                _logger.LogWarning("WebSocket client is not connected. Cannot send subscription request.");
                return;
            }

            var request = new PriceRequest(instrumentId, true);
            await SendAsync(request);
            _logger.LogInformation("Sent subscription request for instrument {InstrumentId}", instrumentId);
        }

        public async Task StopSubscription(Guid instrumentId)
        {
            _priceStore.Remove(instrumentId.ToString());

            if (_wsClient?.State is not WebSocketState.Open)
            {
                _logger.LogWarning("WebSocket client is not connected. Cannot send unsubscription request.");
                return;
            }

            var request = new PriceRequest(instrumentId, false);
            await SendAsync(request);
            _logger.LogInformation("Sent unsubscription request for instrument {InstrumentId}", instrumentId);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _wsClient = new ClientWebSocket();
            _wsClient.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);

            await _wsClient.ConnectAsync(
                new Uri(WEB_SOCKET_URL), stoppingToken);
            _logger.LogInformation("WebSocket client connected to Fintacharts streaming API.");

            using var cleanupTimer = new PeriodicTimer(CleanupInterval);

            var pingTask = KeepAliveLoopAsync(stoppingToken);
            var receiveTask = ReceiveLoopAsync(stoppingToken);
            var cleanupTask = CleanupLoopAsync(cleanupTimer, stoppingToken);

            await Task.WhenAll(receiveTask, cleanupTask, pingTask);
        }

        private async Task KeepAliveLoopAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested
                && _wsClient!.State == WebSocketState.Open)
            {
                var pingMessage = new { type = "ping" };
                _logger.LogInformation("Sending ping to keep WebSocket connection alive.");
                await SendAsync(pingMessage);
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
            }
        }

        private async Task ReceiveLoopAsync(CancellationToken ct)
        {
            var buffer = new byte[1024 * 16];
            while (!ct.IsCancellationRequested
                && _wsClient!.State == WebSocketState.Open)
            {
                var result = await _wsClient.ReceiveAsync(buffer, ct);

                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                _logger.LogInformation("Received message: {Message}", message);

                var price = JsonConvert.DeserializeObject<PriceAskResponse>(message);
                if (price is not null
                        && price.Ask is not null
                        && price.Type == "l1-update")
                {
                    _priceStore.UpdatePrice(price.InstrumentId.ToString(), price.Ask.Price);
                }
            }
        }

        private async Task CleanupLoopAsync(PeriodicTimer timer, CancellationToken ct)
        {
            while (await timer.WaitForNextTickAsync(ct))
            {
                var stale = _priceStore.GetStale(StaleTtl).ToList();
                foreach (var instrumentId in stale)
                {
                    _logger.LogInformation("Unsubscribing stale instrument {InstrumentId}", instrumentId);
                    _priceStore.Remove(instrumentId);

                    if (_wsClient?.State is WebSocketState.Open)
                    {
                        var request = new PriceRequest(Guid.Parse(instrumentId), false);
                        await SendAsync(request);
                    }
                }
            }
        }

        private async Task SendAsync<T>(T payload)
        {
            var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            _logger.LogInformation("Sent message: {Message}", json);

            var buffer = Encoding.UTF8.GetBytes(json);

            await _wsClient!.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_wsClient != null)
            {
                await _wsClient.CloseAsync(WebSocketCloseStatus.NormalClosure,
                    "Background service stopped", cancellationToken);
            }
            await base.StopAsync(cancellationToken);
        }
    }
}
