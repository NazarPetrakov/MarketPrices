using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPrices.Presentation.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private ISender? _sender;

        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
