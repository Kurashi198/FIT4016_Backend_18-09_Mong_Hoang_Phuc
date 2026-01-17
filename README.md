# Order Management Application (Entity Framework Core)

## 1. Project Overview

This project is an ASP.NET Core MVC application for managing **Products** and **Orders** using **Entity Framework Core (Code First)**.

The system includes two main tables:
- **Products**
- **Orders**

Relationship:
- One **Product** can have many **Orders** (1 – N relationship).
- Each **Order** belongs to one **Product**.

---

## 2. Database Design

### Products Table
- id (int, primary key, auto increment)
- name (string, not null, unique)
- sku (string, not null, unique)
- description (string, nullable)
- price (decimal, not null)
- stock_quantity (int, not null)
- category (string, not null)
- created_at (timestamp)
- updated_at (timestamp)

### Orders Table
- id (int, primary key, auto increment)
- product_id (int, foreign key → products.id, not null)
- order_number (string, not null, unique)
- customer_name (string, not null)
- quantity (int, not null)
- customer_email (string, not null, unique)
- order_date (date, not null)
- delivery_date (date, nullable)
- created_at (timestamp)
- updated_at (timestamp)

---

## 3. Main Features

### 🔹 Orders CRUD (Create – Read – Update – Delete)

#### Create Order
- Validate Order Number format: `ORD-YYYYMMDD-XXXX`
- Validate Customer Name (2–100 characters)
- Validate Customer Email (email format, unique)
- Validate Product existence
- Validate Quantity > 0 and ≤ stock_quantity
- Validate Order Date ≤ current date
- Validate Delivery Date ≥ Order Date
- Show validation errors in **English**

#### Read Orders
- Display all Orders in a table
- Pagination: 10 orders per page
- Search by Order Number or Customer Name
- Display:
  - Order Number
  - Customer Name
  - Customer Email
  - Product Name
  - Quantity
  - Order Date
  - Status (Pending / Delivered)
- Show total records and paging info

#### Update Order
- Editable fields:
  - Customer Name
  - Customer Email
  - Quantity
  - Delivery Date
- NOT editable:
  - Order Number
  - Product
- Input validation same as Create
- Show success and error messages in English

#### Delete Order
- Delete only Orders (not Products)
- Show confirmation dialog before deleting
- Show success/error message after delete

---

## 4. Technology Stack

- ASP.NET Core MVC
- Entity Framework Core (Code First)
- SQL Server
- Razor Views

---

## 5. Project Structure

