## Quick Start
 
**Prerequisites:** [Docker Desktop](https://www.docker.com/products/docker-desktop/)
 
1. Clone the repository:
   ```bash
   git clone https://github.com/NazarPetrakov/MarketPrices.git
   cd MarketPrices
   ```
 
2. Build and run:
   ```bash
   docker compose up -d --build
   ```
 
## Services
 
| Service  | URL                   |
|----------|-----------------------|
| API      | http://localhost:80   |
| Adminer  | http://localhost:8080 |
 
## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/assets` | Returns a paginated list of assets with "simulation" provider |
| GET | `/api/assets/{id}/price` | Returns the latest price for a specific asset |

### GET `/api/assets`

**Query Parameters:**

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| pageSize | integer | 10 | Number of assets per page |
| pageNumber | integer | 1 | Page number to retrieve |

---

### GET `/api/assets/{id}/price`

**Path Variables:**

| Parameter | Type          | Description                 |
|-----------|---------------|-----------------------------|
| id        | string (UUID) | The unique asset identifier |

 

 
