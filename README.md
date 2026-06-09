# iREDO SOAP API

#### Table of Contents

- [Business Case](#business-case)
- [Technologies](#technologies)
- [Features](#features)
- [Project Structure](#project-structure)
- [Database Structure](#database-structure)
- [Running the Service](#running-the-service)
    - [Environment Variables]
    - [Run Locally with Docker Database]
    - [Run Fully with Docker]
    - [Fix 3: First Time Setup]
    - [Docker Compose Architecture]
- [SOAP Endpoint](#soap-endpoint)
    - [WSDL] 
- [SOAP Operations](#soap-operations)
- [SOAP Faults](#soap-faults)
- [Testing](#testing)
- [Future Improvements](#future-improvements)
- [Security Considerations](#security-considerations)





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

### Faults

* AuthenticationFault
* NotFoundFault
* ConflictFault
* ValidationFault

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
│   ├── AuthenticationFault.cs
│   ├── ConflictFault.cs
│   ├── NotFoundFault.cs
│   └── ValidationFault.cs
│
├── Security
│   ├── SecurityValidator.cs
│   └── InputValidator.cs
|
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
# ----------------------
# SOAP API
# ----------------------
SOAP_API_CONTAINER_NAME=soap-api
SOAP_API_PORT=5298
SOAP_API_KEY=ey.......
# ----------------------
# SOAP MYSQL DB
# ----------------------
SOAP_DB_CONTAINER_NAME=soap-mysql
SOAP_DB_PORT=3307
SOAP_DB_NAME=soapdb

SOAP_DB_USER=soapuser
SOAP_DB_USER_PASSWORD=soap123

SOAP_DB_ROOT_PASSWORD=root123

```

---

### Run Locally with Docker Database

Start the MySQL container:

```bash
docker compose up -d
```

Apply migrations:

```bash
dotnet ef database update
```

Run the API locally:

```bash
dotnet run
```
### Run Fully with Docker
Build and start all containers:
```
docker compose up -d --build
```
The SOAP API will be available at:
```
http://localhost:5298/SupplierPurchaseOrderService.asmx
```

### Fix 3: First Time Setup

Current:

```bash
docker compose up -d
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

---
___

### Docker Compose Architecture

The application consists of:

* SOAP API Container
* MySQL Database Container

```text
Client
   │
   ▼
SOAP API
   │
   ▼
MySQL
```



Start all services:

```bash
docker compose up -d --build
```

Stop all services:

```bash
docker compose down
```

Remove containers and database volume:

```bash
docker compose down -v
```


___


### First Time Setup

If no migration exists:

```bash
docker compose up -d
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

### AuthenticationFault
Returned when a request contains an invalid or missing access token.

### ConflictFault

Returned when a requested operation would result in an invalid business state.

### NotFoundFault

Returned when a requested entity cannot be found.

### ValidationFault

Returned when request data fails validation rules.

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
* AuthenticationFault test
* ConflictFault test
* NotFoundFault test
* ValidationFault test

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
## Security Considerations

### Authentication

The service validates an access token for all SOAP operations.

This simulates communication between trusted backend systems such as:

- API Gateway
- Warehouse Service
- ERP Service
- Invoice Service

Unauthorized requests return AuthenticationFault.

### SQL Injection

Protected through Entity Framework Core LINQ queries and parameterized SQL generation.

### XSS

Limited risk because the service returns XML responses and does not render user input as HTML. Input validation is implemented on request data.

### CSRF

The service is designed for system-to-system communication within a microservice architecture. Requests are received from trusted clients such as an API Gateway or other backend services. Since authentication is not based on browser cookies or user sessions, traditional CSRF attacks are not applicable.

[Back to top](#table-of-contents)
___ 

## Author

Created as part of the iREDO API exam project. 

[Back to top](#table-of-contents)

___
---

