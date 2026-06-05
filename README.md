# iREDO SOAP API
# SoapAPI

Supplier purchase orderservice
- A company uses a system to manage suppliers, purchase orders, invoices, and warehouse stock.

It is built with:

- ASP.NET Core 8
- Microsoft.EntityFrameworkCore (v9.0.0)
- Microsoft.EntityFrameworkCore.Design (v9.0.0)
- Pomelo.EntityFrameworkCore.MySql (v9.0.0)
- SoapCore (v1.2.1.13)
- Docker

# Technologies
- MYSQL Database (8.0)

---
# Features
# Project Structure
```text
SoapApi
│
├── Contracts
│   └── ISupplierPurchaseOrderService.cs
│
├── Data
│   ├── AppDbContext.cs
│   └── Configurations
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
|
├── Requests
│   ├── GetSupplierByIdRequest.cs
│   └── CreatePurchaseOrderRequest.cs
│
└── Responses
│   ├── GetSupplierByIdResponse.cs
│   └── CreatePurchaseOrderResponse.cs
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
│   └── SupplierPurchaseOrderService.cs
├── .env
├── Program.cs
├── appsettings.json
```

# Running the Service
## Run with Docker

```bash

docker compose down -v

docker compose up -d

dotnet ef migrations add InitialCreate

dotnet ef database update

```
Or if the migration already exists:
```bash
docker compose up -d

dotnet ef database update
```

## Run witout Docker
First install MySQL locally and create ```soapdb```.
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run

```

## SOAP WSDL:
[Click Here SupplierPurchaseOrderService.asmx?wsdl](https://localhost:7034/SupplierPurchaseOrderService.asmx?wsdl)


# Database Structure
## Tables
- suppliers
- purchase_orders
- purchase_order_lines
- products
- erp_audit_logs

# Entity Relationships
# Swagger
# Non Protected Endpoints
## READ:
```
 GetSupplierById
```
```
 GetPurchaseOrderById
```
## Change data:
```
 CreatePurchaseOrder
```
```
 UpdatePurchaseOrderStatus
```
## SOAP faults

```
 SupplierNotFoundFault
```
```
 PurchaseOrderNotFoundFault
```
```
 InvalidOrderStatusFault
```



# Protected Endpoints
# Security Notes
# Environment Variables
	```.env
	SOAP_DB_CONTAINER_NAME=soap-mysql
	SOAP_DB_PORT=3307

	SOAP_USER=soapuser
	SOAP_USER_PASSWORD=soap123

	SOAP_DB_PASSWORD=root123
	```
