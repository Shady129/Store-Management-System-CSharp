# 🛒 Store Management System

A C# console-based Store Management System designed to manage items, customers, orders, inventory, and sales reports.

The project follows a layered architecture and focuses on applying business rules, custom validation, file-based data persistence, inventory management, and practical order workflows.

---

## 🚀 Features

### 📦 Item Management
- ➕ Add new items
- 📋 View all items
- 🔎 Find items by ID
- ✏️ Update item information
- 🗑️ Delete items
- 💰 Manage prices and stock quantities
- 🚫 Prevent orders when an item is out of stock

### 👥 Customer Management
- ➕ Add new customers
- 📋 View all customers
- 🔎 Find customers by ID
- ✏️ Update customer information
- 🗑️ Delete customers
- 📧 Email validation
- 📱 Phone number validation

### 🛒 Order Management
- ➕ Create new orders
- 📋 View all orders
- 🔎 Find orders by ID
- ✏️ Update existing orders
- 🗑️ Delete orders
- 👤 Link orders to customers
- 📦 Link orders to items
- 🚫 Prevent orders that exceed available stock
- 📉 Automatically reduce stock after creating an order
- 🔄 Recalculate stock when an order is updated
- ♻️ Restore stock when an order is deleted
- 🔁 Handle changing the item inside an existing order

### 📊 Sales Reports
- 💵 Total Sales Summary
- 👥 Sales by Customer
- 📦 Sales by Item
- 🏆 Top 5 Customers by Revenue
- 🥇 Top 5 Items by Revenue

---

## 🏗️ Project Architecture

The solution is organized into multiple layers:

```text
StoreApp
│
├── 🖥️ StoreApp
│   └── Console UI / Menus
│
├── 🧠 BL
│   └── Business Logic & Validation
│
├── 💾 DAL
│   └── Data Access & File Persistence
│
├── 📦 Domains
│   └── Entities & Custom Attributes
│
└── 🛠️ Utilites
    └── Shared Utilities
```

### 🖥️ Presentation Layer
Handles console menus, user input, user interaction, and displaying results.

### 🧠 Business Logic Layer
Handles business rules, validation, duplicate ID protection, and communication between the UI and data layer.

### 💾 Data Access Layer
Handles reading, writing, updating, and deleting persistent data.

### 📦 Domain Layer
Contains the main entities:
- Item
- Customer
- Order
- BaseEntity

It also contains the custom validation attributes used by the application.

---

## 🛡️ Custom Validation

The project includes a custom validation system using C# attributes.

```csharp
[Required]
[MinLength(5)]
[MaxLength(50)]
public string Name { get; set; }

[Range(0, 1000)]
public int Quantity { get; set; }
```

Customer validation includes:

```csharp
[Required]
[Email]
public string Email { get; set; }

[Required]
[Phone(11)]
public string Phone { get; set; }
```

---

## 📦 Inventory Management

Inventory is synchronized with order operations.

### ➕ Creating an Order

```text
Available Stock
      ↓
Create Order
      ↓
Validate Requested Quantity
      ↓
Reduce Item Stock
```

Example:

```text
Stock Before  : 10
Order Quantity: 4
Stock After   : 6
```

The system prevents an order when the requested quantity exceeds the available stock.

If stock reaches `0`, the item is treated as out of stock.

### ✏️ Updating an Order

When an order quantity changes, the application recalculates the inventory.

```text
Original Stock : 10
Original Order : 4
Current Stock  : 6

Updated Order  : 7
New Stock      : 3
```

The application also handles changing the item assigned to an existing order.

```text
Old Item
   ↓
Restore Old Quantity
   ↓
New Item
   ↓
Validate Available Stock
   ↓
Reduce New Item Quantity
```

### 🗑️ Deleting an Order

Deleting an order restores its quantity to the related item's inventory.

```text
Current Stock : 3
Order Quantity: 7

Delete Order
     ↓
New Stock: 10
```

---

## 📊 Reporting System

The application provides:

### 💵 Total Sales Summary
Displays:
- Total Orders
- Total Revenue

### 👥 Sales by Customer
Displays:
- Customer ID
- Customer Name
- Number of Orders
- Revenue

### 📦 Sales by Item
Displays:
- Item ID
- Item Name
- Quantity Sold
- Revenue

### 🏆 Top 5 Customers
Ranks customers based on generated revenue.

### 🥇 Top 5 Items
Ranks items based on generated revenue.

---

## ⚠️ Error Handling

The application handles multiple invalid scenarios:

- ❌ Duplicate IDs
- ❌ Invalid customer IDs
- ❌ Invalid item IDs
- ❌ Invalid order IDs
- ❌ Invalid email addresses
- ❌ Invalid phone numbers
- ❌ Invalid price ranges
- ❌ Invalid quantities
- ❌ Orders exceeding available stock
- ❌ Ordering an out-of-stock item

Custom exceptions are used to separate business logic errors from data-access errors.

---

## 💾 Data Persistence

Application data is stored using file-based persistence.

Items, customers, and orders remain available after the application is closed and restarted.

---

## 🧪 Tested Workflows

The complete application workflow was manually tested:

```text
Items
  ↓
Customers
  ↓
Orders
  ↓
Inventory Updates
  ↓
Order Updates
  ↓
Order Deletion
  ↓
Sales Reports
```

Tested scenarios include:

- ✅ Item CRUD operations
- ✅ Customer CRUD operations
- ✅ Order CRUD operations
- ✅ Duplicate ID validation
- ✅ Custom field validation
- ✅ Invalid entity IDs
- ✅ Stock availability validation
- ✅ Out-of-stock protection
- ✅ Stock reduction after creating orders
- ✅ Stock recalculation after updating orders
- ✅ Stock restoration after deleting orders
- ✅ Changing items inside existing orders
- ✅ Sales calculations
- ✅ Sales by customer
- ✅ Sales by item
- ✅ Top customers
- ✅ Top items

---

## 🛠️ Technologies Used

- 💻 C#
- ⚙️ .NET
- 🖥️ Console Application
- 📁 File Handling
- 🧩 Generic Programming
- 🏷️ Custom Attributes
- 🛡️ Custom Validation
- ⚠️ Exception Handling
- 🏗️ Layered Architecture
- 🔎 LINQ

---

## 🎯 What I Practiced

This project helped me practice:

- Building a multi-layer C# application
- Separating UI, business logic, domain models, and data access
- Applying business rules to practical workflows
- Building reusable generic components
- Creating custom validation attributes
- Managing relationships between customers, items, and orders
- Synchronizing inventory with order operations
- Handling application errors
- Working with persistent file storage
- Using LINQ for querying and reporting
- Testing complete application workflows

---

## 👨‍💻 Author

**Shady Mahmoud**

🔗 GitHub: https://github.com/Shady129

Built as part of my C# and .NET backend development learning journey.
