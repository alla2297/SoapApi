# iREDO SOAP API

#### Table of Contents

- [Business Case]{# Business Case}
- [Technologies]
- [Features]
- [Project Structure]
- [Database Structure]
- [Running the Service]
    - [Environment Variables]
    - [Run with Docker]
- [SOAP Endpoint]
    - [WSDL] 
- [SOAP Operations]
- [Testing]
- [Security Notes]
- [Future Improvements]
- [Common Issues]




## Business Case

Supplier Purchase Order Service

A company uses a system to manage suppliers, purchase orders, invoices, and warehouse stock.

External systems can use this SOAP API to:

* Retrieve supplier information
* Retrieve purchase order information
* Create purchase orders
* Update purchase order status

[Back to top](#table-of-contents)

---
## Technologies

### Backend

* ASP.NET Core 8
* SoapCore 1.2.1.13
* Entity Framework Core 9
* Pomelo.EntityFrameworkCore.MySql 9
* DotNetENV 3.2.1

### Database

* MySQL 8.0

### Containerization

* Docker
* Docker Compose

[Back to top](#table-of-contents)

---
## Features

### Read Operations

* GetSupplierById
* GetPurchaseOrderById

### Change Operations

* CreatePurchaseOrder
* UpdatePurchaseOrderStatus

### SOAP Faults

* SupplierNotFoundFault
* PurchaseOrderNotFoundFault
* InvalidOrderStatusFault

### Additional Features

* WSDL generation
* Entity Framework Core integration
* MySQL database persistence
* Audit logging
* Docker support

[Back to top](#table-of-contents)

---
## Project Structure

```text
SoapApi
│
├── Contracts
│   └── ISupplierPurchaseOrderService.cs
│
├── Data
│   └── AppDbContext.cs
│
├── DTO
│   ├── Requests
│   │   ├── GetSupplierByIdRequest.cs
│   │   ├── GetPurchaseOrderByIdRequest.cs
│   │   ├── CreatePurchaseOrderRequest.cs
│   │   └── UpdatePurchaseOrderStatusRequest.cs
│   │
│   └── Responses
│       ├── GetSupplierByIdResponse.cs
│       ├── GetPurchaseOrderByIdResponse.cs
│       ├── CreatePurchaseOrderResponse.cs
│       └── UpdatePurchaseOrderStatusResponse.cs
│
├── Models
│   ├── Supplier.cs
│   ├── PurchaseOrder.cs
│   ├── PurchaseOrderLine.cs
│   ├── Product.cs
│   └── AuditLog.cs
│
├── Faults
│   ├── SupplierNotFoundFault.cs
│   ├── PurchaseOrderNotFoundFault.cs
│   └── InvalidOrderStatusFault.cs
│
├── Services
│   ├── SupplierPurchaseOrderService.cs
│   └── AuditService.cs
│
├── Migrations
├── Program.cs
├── appsettings.json
└── .env
```
[Back to top](#table-of-contents)

---
## Database Structure

### Tables

| Table              | Purpose                |
| ------------------ | ---------------------- |
| Suppliers          | Supplier master data   |
| PurchaseOrders     | Purchase order header  |
| PurchaseOrderLines | Purchase order details |
| Products           | Product catalog        |
| AuditLogs          | Tracks SOAP operations |

### Relationships

```text
Supplier
    │
    └── PurchaseOrders
            │
            └── PurchaseOrderLines
                    │
                    └── Product
```
[Back to top](#table-of-contents)

---
## Running the Service

### Environment Variables

Create a `.env` file:

```env
SOAP_DB_CONTAINER_NAME=soap-mysql
SOAP_DB_PORT=3307

SOAP_USER=soapuser
SOAP_USER_PASSWORD=soap123

SOAP_DB_PASSWORD=root123

```

---

### Run with Docker

Start MySQL:

```bash
docker compose up -d
```

Apply migrations:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

---

### First Time Setup

If no migration exists:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

---

### Reset Database

```bash
docker compose down -v
docker compose up -d
dotnet ef database update
```

[Back to top](#table-of-contents)

---
## SOAP Endpoint

```text
http://localhost:5298/SupplierPurchaseOrderService.asmx
```

### WSDL

```text
http://localhost:5298/SupplierPurchaseOrderService.asmx?wsdl
```
[Click Here SupplierPurchaseOrderService.asmx?wsdl](https://localhost:7034/SupplierPurchaseOrderService.asmx?wsdl)

[Back to top](#table-of-contents)

---

## SOAP Operations

### GetSupplierById

Returns supplier information by supplier ID.

### GetPurchaseOrderById

Returns purchase order information by purchase order ID.

### CreatePurchaseOrder

Creates a new purchase order.

### UpdatePurchaseOrderStatus

Updates the status of an existing purchase order.

[Back to top](#table-of-contents)

---

## SOAP Faults

### SupplierNotFoundFault

Returned when a supplier cannot be found.

### PurchaseOrderNotFoundFault

Returned when a purchase order cannot be found.

### InvalidOrderStatusFault

Returned when an unsupported order status is provided.

Valid statuses:

* Pending
* Approved
* Shipped
* Cancelled

[Back to top](#table-of-contents)

---
## Testing

The project includes a Postman collection containing:

Positive:

* GetSupplierById
* GetPurchaseOrderById
* CreatePurchaseOrder
* UpdatePurchaseOrderStatus

Negative 
* SupplierNotFoundFault test
* PurchaseOrderNotFoundFault test
* InvalidOrderStatusFault test

[Back to top](#table-of-contents)

---

# Future Improvements

* Add more endpoints
* Improve error handling
* Add better input validation
* Add more response types
* Improve code structure and organization
* Add unit tests
* Improve API documentation
* Add support for additional data models

[Back to top](#table-of-contents)

---

## Author

Created as part of the iREDO API exam project. 

[Back to top](#table-of-contents)

___
---