# Order Management System

**Shipping API Integration for Panda International**

A comprehensive warehouse management and shipping integration platform built for [Panda International](https://www.pandainternationalllc.com/), a third-party logistics (3PL) provider. This system streamlines order processing, inventory management, and shipping operations with real-time FedEx API integration.

> **Outstanding Senior Project Award** — University of Southern Indiana, 2024

---

## Overview

This enterprise-grade order management system was developed as a senior capstone project to solve real-world logistics challenges for Panda International. The platform manages the complete order lifecycle—from inbound receiving through outbound shipping—with integrated FedEx API support for automated label generation and real-time tracking.

### Key Accomplishments

- Reduced manual shipping label creation time by **automating FedEx API integration**
- Designed normalized SQL database schema supporting **15+ entity relationships**
- Implemented **role-based access control** for warehouse staff and administrators
- Built **RESTful API endpoints** for real-time shipping updates

---

## Features

### Dashboard
![Dashboard](docs/dashboard.png)
- Real-time order statistics and KPIs
- Recent orders with status tracking
- Shipping activity feed

### Order Management
- **Platform Orders** — Multi-channel order management (Amazon, eBay, direct)
- **Inbound Orders** — Track incoming shipments and receiving
- **Outbound Shipping** — Process parcel and freight shipments

### Inventory Control
- Real-time stock levels across multiple warehouses
- SKU-based product tracking
- Automated inventory adjustments on order fulfillment

### Shipping Integration
- **FedEx API** — OAuth2 authentication and label generation
- Support for multiple service types (Ground, Express, 2Day, Overnight)
- Real-time tracking with shipment status updates

### Billing & User Management
- Customer billing account management
- Role-based access control
- Secure authentication system

---

## Tech Stack

| Layer | Technology |
|-------|------------|
| **Backend** | C# / ASP.NET Core 8.0 MVC |
| **Database** | SQL Server (Entity Framework Core) |
| **API Integration** | FedEx Ship API (OAuth2, REST) |
| **Frontend** | Razor Views, Bootstrap 5 |
| **Tools** | Visual Studio 2022, Postman, Git |

---

## Architecture

```
OrderManagementSystem/
├── Controllers/          # 22 MVC controllers
│   ├── HomeController.cs
│   ├── ShippingController.cs      # FedEx label generation
│   ├── ShippingApiController.cs   # REST API endpoints
│   ├── PlatformOrderController.cs
│   └── ...
├── Models/               # 35 entity models
│   ├── FedExLabelRequest.cs
│   ├── Order.cs
│   ├── Inventory.cs
│   └── ...
├── Views/                # Razor views
│   ├── Home/
│   ├── Shipping/
│   └── Shared/_Layout.cshtml
├── Services/             # Business logic
│   ├── FedExAuthService.cs
│   └── FedExShippingService.cs
├── Data/                 # EF Core DbContext
└── sql/                  # Database scripts
```

---

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) or SQL Server
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/PryceHedrick/OrderManagementSystem.git
   cd OrderManagementSystem
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Open in browser**
   ```
   http://localhost:5138
   ```

### Demo Credentials
- **Email:** admin@pandaoms.com
- **Password:** (any)

---

## API Endpoints

### Shipping API

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/Shipping/CreateShippingLabel` | Generate FedEx shipping label |

**Example Request:**
```json
{
  "originAddress": {
    "streetLines": ["1234 Logistics Way"],
    "city": "Evansville",
    "stateOrProvinceCode": "IN",
    "postalCode": "47715",
    "countryCode": "US"
  },
  "destinationAddress": { ... },
  "packageDetails": {
    "weight": 5.0,
    "dimensions": { "length": 12, "width": 10, "height": 8 }
  },
  "serviceType": "FEDEX_GROUND"
}
```

---

## Project Context

### About

This system was developed during the **Fall 2024 semester** as part of the Computer Science Senior Capstone at the University of Southern Indiana. The project addressed real operational needs for Panda International, a logistics company requiring modernized order and shipping management.

### Team

- **Pryce Hedrick** — Backend Development, API Integration, Database Design
- 2 additional team members

### Development Timeline

| Phase | Timeline |
|-------|----------|
| Requirements & Design | August 2024 |
| Core Development | September–October 2024 |
| FedEx API Integration | November 2024 |
| Testing & Deployment | December 2024 |

---

## Recognition

This project was awarded the **Outstanding Senior Project Award** at the University of Southern Indiana's 2024 Computer Science Capstone Showcase, recognizing excellence in:

- Technical implementation and code quality
- Real-world problem solving
- Professional documentation and presentation

---

## Screenshots

### Login
![Login](docs/login.png)

### Dashboard
![Dashboard](docs/dashboard.png)

### Shipping Labels
![Shipping](docs/shipping.png)

### Tracking
![Tracking](docs/tracking.png)

---

## License

This project was developed for Panda International as part of an academic capstone. All rights reserved.

---

## Contact

**Pryce Hedrick**  
[LinkedIn](https://www.linkedin.com/in/pryce-hedrick) • [GitHub](https://github.com/PryceHedrick) • [prycehedrick@gmail.com](mailto:prycehedrick@gmail.com)
