# 🛒 **Inventory & POS Management System**

This **Inventory & POS Management System** is designed to streamline the process of managing inventory, point of sale (POS), vendor transactions, and multiple warehouse operations. With real-time updates and automatic features, this system ensures seamless management for both admins and shop users. 

---

## 🌟 **Features**

### 1. **User Roles**
- **Shop Users** 👩‍💻:
  - Access only the **POS System** to manage sales transactions.
  - **Real-time inventory updates** at the point of sale.
  - Submit requests to **Admin** for products that are unavailable in the shop.

- **Admin Users** 👨‍💼:
  - Full access to all system features.
  - Manage **inventory**, **orders**, and **vendor transactions**.
  - Approve or fulfill product requests from shop users.
  - View and edit **vendor sales/purchase history**.

- **Vendor Users** 🏢:
  - Sell products directly from the **warehouse**.
  - Maintain a **purchase and sales history**.

---

### 2. **Inventory Management** 📦
- **Automatic Inventory Update**: 
  - **Real-time updates** on product availability when a sale is made.
  - If a **parent product** is sold, the inventory of its **sub-products** is automatically updated.

- **Multiple Warehouse Support** 🏢🏢:
  - **Track inventory per warehouse**. Each warehouse has its own set of products and stock levels.
  - Products are sold and updated based on the warehouse from which they are sourced.

- **Threshold Alerts** ⚠️:
  - **Stock-level monitoring**: The system automatically sends an **alert** when inventory for any product falls below a pre-defined threshold, helping users avoid stockouts.

---

### 3. **POS System** 💳
- **Shop User Access**: 
  - Shop users can view and manage shop-specific inventory and process sales through the POS interface.
  - **Real-time inventory update** as sales are processed.

- **Vendor Order Fulfillment** 📦:
  - Vendors will fulfill orders from the warehouse and their **sales/purchase history** is maintained within the system for tracking and reporting.

---

### 4. **Invoice Generation** 🧾
- **Walk-in Customer Invoices**:
  - Walk-in customers receive **thermal invoices** at the point of sale, providing a quick and efficient receipt.

- **Vendor Invoices**:
  - Vendors receive detailed **A4 invoices** via **WhatsApp** for record-keeping and transaction purposes.

---

### 5. **Product Management** 🛍️
- **Sub-Product Handling**:
  - Products can have **sub-products** (e.g., accessories, components). When a **parent product** is sold, the inventory for the sub-products automatically updates.

---

### 6. **Admin Control & Monitoring** 👨‍💻
- **Comprehensive Admin Dashboard**:
  - View and manage **shop inventory**, **warehouse inventory**, and **vendor data**.
  - Admins can approve or deny product requests submitted by shop users for fulfillment from warehouses.

- **Vendor History**:
  - Maintain a **record** of **sales and purchases** made by vendors to track transactions over time.

---

## 🔧 **Technical Overview**

### 💻 **Tech Stack**
- **Frontend**: React, HTML5, CSS3, JavaScript
- **Backend**: Node.js, Express.js
- **Database**: MongoDB (or any other relational database)
- **Messaging**: WhatsApp API (for sending vendor invoices)
- **Real-Time Data**: WebSockets (for real-time inventory updates)

---

## 🚀 **Setup & Installation**

### Prerequisites:
- **Node.js** (v12.x or higher)
- **MongoDB** (for database)
- **WhatsApp API** (for invoice sending)
  
### Steps to Run Locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/hypersgamingarena/IMS.git
