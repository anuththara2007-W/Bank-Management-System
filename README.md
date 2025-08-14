# 🏦 Bank Management System

<div align="center">
  
  ![C#](https://img.shields.io/badge/language-C%23-blue?style=for-the-badge&logo=c-sharp&logoColor=white)
  ![Framework](https://img.shields.io/badge/framework-.NET%20Framework-green?style=for-the-badge&logo=dotnet&logoColor=white)
  ![Windows Forms](https://img.shields.io/badge/UI-Windows%20Forms-lightblue?style=for-the-badge&logo=windows&logoColor=white)
  ![Database](https://img.shields.io/badge/database-SQL%20Server-orange?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
  ![Status](https://img.shields.io/badge/status-Active-success?style=for-the-badge)
  
  <h3>🚀 A C# Windows Forms Application for managing bank operations</h3>
  <p><em>Role-based access for Admins and Customers</em></p>
  
  [![GitHub stars](https://img.shields.io/github/stars/anuththara2007-W/Bank-Management-System?style=social)](https://github.com/anuththara2007-W/Bank-Management-System/stargazers)
  [![GitHub forks](https://img.shields.io/github/forks/anuththara2007-W/Bank-Management-System?style=social)](https://github.com/anuththara2007-W/Bank-Management-System/network)
  
</div>

---

## 📌 Overview

<div align="center">
  
```
🏦 COMPLETE BANK MANAGEMENT SOLUTION
════════════════════════════════════

   🔐 Secure Login System
   💳 Account Operations  
   🏦 Loan Management
   📜 Transaction Tracking
   👤 Profile Management
   📩 Support System
```

</div>

This project provides a complete bank management solution with:

- 🔐 **Secure login** for customers and admins
- 💳 **Account operations**: create, view, deposit, withdraw, transfer
- 🏦 **Loan system**: request, approve/reject, view loan history
- 📜 **Transaction tracking** with history and search
- 👤 **Profile & password management**
- 📩 **Support requests** for customer queries

---

## ⭐ Features

<div align="center">

### 🎯 Role-Based Feature Matrix

</div>

<table align="center">
<thead>
<tr>
<th>🔧 Feature</th>
<th>👑 Admin</th>
<th>👤 Customer</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>🔐 Login & Role Management</strong></td>
<td align="center">✅</td>
<td align="center">✅</td>
</tr>
<tr>
<td><strong>💳 Create/Edit Accounts</strong></td>
<td align="center">✅</td>
<td align="center">❌</td>
</tr>
<tr>
<td><strong>💰 Deposit / Withdraw</strong></td>
<td align="center">✅</td>
<td align="center">✅</td>
</tr>
<tr>
<td><strong>🔄 Transfer Funds</strong></td>
<td align="center">✅</td>
<td align="center">✅</td>
</tr>
<tr>
<td><strong>📊 View All Transactions</strong></td>
<td align="center">✅</td>
<td align="center">❌</td>
</tr>
<tr>
<td><strong>📋 View Own Transactions</strong></td>
<td align="center">❌</td>
<td align="center">✅</td>
</tr>
<tr>
<td><strong>🏦 Loan Approval System</strong></td>
<td align="center">✅</td>
<td align="center">❌</td>
</tr>
<tr>
<td><strong>📝 Loan Requests</strong></td>
<td align="center">❌</td>
<td align="center">✅</td>
</tr>
<tr>
<td><strong>👥 Customer Profile Management</strong></td>
<td align="center">✅</td>
<td align="center">✅</td>
</tr>
<tr>
<td><strong>🎫 Support Ticket System</strong></td>
<td align="center">✅</td>
<td align="center">✅</td>
</tr>
</tbody>
</table>

---

## 🗂 Project Structure & File Details

<div align="center">
  
### 📁 **Detailed Architecture Overview**

</div>

<details>
<summary><h3>🚀 Core Application Files</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Program.cs</strong></td>
<td>Entry point for launching the application</td>
</tr>
<tr>
<td><strong>App.config</strong></td>
<td>Stores the database connection string and app configuration</td>
</tr>
<tr>
<td><strong>Session.cs</strong></td>
<td>Holds current user session details (ID, Name, Role)</td>
</tr>
<tr>
<td><strong>DatabaseHelper.cs</strong></td>
<td>Handles SQL Server database connections and queries</td>
</tr>
<tr>
<td><strong>sqlCommand.cs</strong></td>
<td>Custom helper for running SQL commands</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>🔐 Authentication & Navigation</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Landing.cs / Landing.Designer.cs / Landing.resx</strong></td>
<td>Welcome screen; allows users to choose <strong>Login</strong> or exit</td>
</tr>
<tr>
<td><strong>Dashboard.cs / Dashboard.Designer.cs / Dashboard.resx</strong></td>
<td>Main Admin control panel after login</td>
</tr>
<tr>
<td><strong>CustomerDashboard.cs / CustomerDashboard.Designer.cs / CustomerDashboard.resx</strong></td>
<td>Main Customer control panel after login</td>
</tr>
<tr>
<td><strong>CustomerMain.cs / CustomerMain.Designer.cs / CustomerMain.resx</strong></td>
<td>Central navigation for customer-specific actions</td>
</tr>
<tr>
<td><strong>Main.cs / Main.Designer.cs / Main.resx</strong></td>
<td>Alternate navigation hub for admin/customer actions</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>💳 Account Management</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Account.cs / Account.Designer.cs / Account.resx</strong></td>
<td>Create and manage a single account's details</td>
</tr>
<tr>
<td><strong>Account1.cs</strong></td>
<td>Extra account-handling logic (legacy or extended)</td>
</tr>
<tr>
<td><strong>AccountPicker.cs / AccountPicker.Designer.cs / AccountPicker.resx</strong></td>
<td>Selection window for choosing an account to view/edit</td>
</tr>
<tr>
<td><strong>MyAccounts.cs / MyAccounts.Designer.cs / MyAccounts.resx</strong></td>
<td>View list of a customer's accounts and balances</td>
</tr>
<tr>
<td><strong>MyAccounts1.cs</strong></td>
<td>Extended functionality for account display</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>👥 Customer Management</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Customer.cs / Customer.Designer.cs / Customer.resx</strong></td>
<td>Create and edit customer details</td>
</tr>
<tr>
<td><strong>CustomerPicker.cs / CustomerPicker.Designer.cs / CustomerPicker.resx</strong></td>
<td>Selection tool for finding customers from a list</td>
</tr>
<tr>
<td><strong>Employee.cs / Employee.Designer.cs / Employee.resx</strong></td>
<td>Manage bank employees (admins can add/edit/remove)</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>💰 Transaction Handling</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>DepositWithdraw.cs / DepositWithdraw.Designer.cs / DepositWithdraw.resx</strong></td>
<td>Perform deposits and withdrawals for an account</td>
</tr>
<tr>
<td><strong>TransferFunds.cs / TransferFunds.Designer.cs / TransferFunds.resx</strong></td>
<td>Transfer funds between accounts</td>
</tr>
<tr>
<td><strong>Transactions.cs / Transactions.Designer.cs / Transactions.resx</strong></td>
<td>View and search all transactions in the system</td>
</tr>
<tr>
<td><strong>MyTransactions.cs / MyTransactions.Designer.cs / MyTransactions.resx</strong></td>
<td>Customers view their own transaction history</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>🏦 Loan System</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Loan.cs / Loan.Designer.cs / Loan.resx</strong></td>
<td>Admin view for all loan applications and statuses</td>
</tr>
<tr>
<td><strong>LoanRequest.cs / LoanRequest.Designer.cs / LoanRequest.resx</strong></td>
<td>Form for customers to request new loans</td>
</tr>
<tr>
<td><strong>MyLoans.cs / MyLoans.Designer.cs / MyLoans.resx</strong></td>
<td>Customers view their approved/rejected/pending loans</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>👤 User Profile & Support</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>Profile.cs / Profile.Designer.cs / Profile.resx</strong></td>
<td>View and update customer profile information</td>
</tr>
<tr>
<td><strong>ChangePassword.cs / ChangePassword.Designer.cs / ChangePassword.resx</strong></td>
<td>Change user account password</td>
</tr>
<tr>
<td><strong>Support.cs / Support.Designer.cs / Support.resx</strong></td>
<td>Submit support tickets or contact bank staff</td>
</tr>
</tbody>
</table>

</details>

<details>
<summary><h3>📊 Data & Resources</h3></summary>

<table>
<thead>
<tr>
<th>📄 File</th>
<th>🎯 Purpose</th>
</tr>
</thead>
<tbody>
<tr>
<td><strong>BankDBDataSet.xsd / .xsc / .xss / Designer.cs</strong></td>
<td>Typed dataset for database access</td>
</tr>
<tr>
<td><strong>BankDBDataSet1.xsd / .xsc / .xss / Designer.cs</strong></td>
<td>Secondary dataset definition for additional tables</td>
</tr>
<tr>
<td><strong>Properties/</strong></td>
<td>Assembly info, resources, and settings</td>
</tr>
<tr>
<td><strong>Resources/</strong></td>
<td>Images, icons, and UI assets used in forms</td>
</tr>
</tbody>
</table>

</details>

---


## 🗂 Project Structure & File Details

Below is a **detailed description** of each important file/form in the project.

### **Core Application Files**
| File | Purpose |
|------|---------|
| **Program.cs** | Entry point for launching the application. |
| **App.config** | Stores the database connection string and app configuration. |
| **Session.cs** | Holds current user session details (ID, Name, Role). |
| **DatabaseHelper.cs** | Handles SQL Server database connections and queries. |
| **sqlCommand.cs** | Custom helper for running SQL commands. |

---

### **Authentication & Navigation**
| File | Purpose |
|------|---------|
| **Landing.cs / Landing.Designer.cs / Landing.resx** | Welcome screen; allows users to choose **Login** or exit. |
| **Dashboard.cs / Dashboard.Designer.cs / Dashboard.resx** | Main Admin control panel after login. |
| **CustomerDashboard.cs / CustomerDashboard.Designer.cs / CustomerDashboard.resx** | Main Customer control panel after login. |
| **CustomerMain.cs / CustomerMain.Designer.cs / CustomerMain.resx** | Central navigation for customer-specific actions. |
| **Main.cs / Main.Designer.cs / Main.resx** | Alternate navigation hub for admin/customer actions. |

---

### **Account Management**
| File | Purpose |
|------|---------|
| **Account.cs / Account.Designer.cs / Account.resx** | Create and manage a single account's details. |
| **Account1.cs** | Extra account-handling logic (legacy or extended). |
| **AccountPicker.cs / AccountPicker.Designer.cs / AccountPicker.resx** | Selection window for choosing an account to view/edit. |
| **MyAccounts.cs / MyAccounts.Designer.cs / MyAccounts.resx** | View list of a customer's accounts and balances. |
| **MyAccounts1.cs** | Extended functionality for account display. |

---

### **Customer Management**
| File | Purpose |
|------|---------|
| **Customer.cs / Customer.Designer.cs / Customer.resx** | Create and edit customer details. |
| **CustomerPicker.cs / CustomerPicker.Designer.cs / CustomerPicker.resx** | Selection tool for finding customers from a list. |
| **Employee.cs / Employee.Designer.cs / Employee.resx** | Manage bank employees (admins can add/edit/remove). |

---

### **Transaction Handling**
| File | Purpose |
|------|---------|
| **DepositWithdraw.cs / DepositWithdraw.Designer.cs / DepositWithdraw.resx** | Perform deposits and withdrawals for an account. |
| **TransferFunds.cs / TransferFunds.Designer.cs / TransferFunds.resx** | Transfer funds between accounts. |
| **Transactions.cs / Transactions.Designer.cs / Transactions.resx** | View and search all transactions in the system. |
| **MyTransactions.cs / MyTransactions.Designer.cs / MyTransactions.resx** | Customers view their own transaction history. |

---

### **Loan System**
| File | Purpose |
|------|---------|
| **Loan.cs / Loan.Designer.cs / Loan.resx** | Admin view for all loan applications and statuses. |
| **LoanRequest.cs / LoanRequest.Designer.cs / LoanRequest.resx** | Form for customers to request new loans. |
| **MyLoans.cs / MyLoans.Designer.cs / MyLoans.resx** | Customers view their approved/rejected/pending loans. |

---

### **User Profile & Support**
| File | Purpose |
|------|---------|
| **Profile.cs / Profile.Designer.cs / Profile.resx** | View and update customer profile information. |
| **ChangePassword.cs / ChangePassword.Designer.cs / ChangePassword.resx** | Change user account password. |
| **Support.cs / Support.Designer.cs / Support.resx** | Submit support tickets or contact bank staff. |

---

### **Data & Resources**
| File | Purpose |
|------|---------|
| **BankDBDataSet.xsd / .xsc / .xss / Designer.cs** | Typed dataset for database access. |
| **BankDBDataSet1.xsd / .xsc / .xss / Designer.cs** | Secondary dataset definition for additional tables. |
| **Properties/** | Assembly info, resources, and settings. |
| **Resources/** | Images, icons, and UI assets used in forms. |

---

## 🛢 Database Overview

Main tables:
- **Users** → Stores login credentials and roles.
- **Customers** → Stores customer details.
- **Accounts** → Bank account records linked to customers.
- **Transactions** → Deposit, withdrawal, and transfer records.
- **Loans** → Loan applications and statuses.
- **SupportRequests** → Customer-submitted queries.

---

## ⚙️ Technologies Used
- **C#** with **Windows Forms**
- **.NET Framework**
- **SQL Server** (ADO.NET connection)
- **DataSets** for database operations


## ⚙️ Technologies Used

<div align="center">

```
🔧 TECHNOLOGY STACK
═══════════════════

┌─────────────────────┐
│   🖥️  Frontend      │
│   C# Windows Forms  │
└─────────────────────┘
           │
           ▼
┌─────────────────────┐
│   🚀  Framework     │
│   .NET Framework    │
└─────────────────────┘
           │
           ▼
┌─────────────────────┐
│   🗄️  Database      │
│   SQL Server        │
│   (ADO.NET)         │
└─────────────────────┘
           │
           ▼
┌─────────────────────┐
│   📊  Data Layer    │
│   DataSets          │
└─────────────────────┘
```

</div>

- **C#** with **Windows Forms**
- **.NET Framework**
- **SQL Server** (ADO.NET connection)
- **DataSets** for database operations

---

## 🚀 Setup & Installation

<div align="center">

### 🎯 **Quick Start Guide**

</div>

```bash
# 1️⃣ Clone the repository
git clone https://github.com/yourusername/Bank-Management-System.git

# 2️⃣ Navigate to project directory
cd Bank-Management-System

# 3️⃣ Open in Visual Studio
# Double-click the .sln file or open through Visual Studio

# 4️⃣ Configure Database Connection
# Update connection string in App.config

# 5️⃣ Build and Run
# Press F5 or click Start in Visual Studio
```

<div align="center">

### 🔧 **System Requirements**

| Component | Requirement |
|-----------|------------|
| **OS** | Windows 7/8/10/11 |
| **Framework** | .NET Framework 4.5+ |
| **Database** | SQL Server Express/Standard |
| **IDE** | Visual Studio 2017+ |

</div>

---

## 👨‍💻 Author & Ownership

<div align="center">
  

  
  ### **Anuththara Wickramasekara**
  
  🌐 **GitHub:** [@anuththara2007-W](https://github.com/anuththara2007-W)  
  📧 **Email:** anuththara2007.official@gmail.com
  
  [![Follow](https://img.shields.io/github/followers/anuththara2007-W?style=social)](https://github.com/anuththara2007-W)
  
</div>

---

## 📜 License & Attribution

<div align="center">

```
© 2025 Anuththara Wickramasekara — Some Rights Reserved
```

<table>
<tr>
<td align="center">
<h3>⚖️ Usage Terms</h3>
<p>You are free to use, modify, and share this project<br>
<strong>as long as proper credit is given</strong> to the original author.</p>
<p>A reference to this repository or the author's name<br>
must be included in any derivative works or publications.</p>
</td>
</tr>
</table>

---

### 🙏 **Show Your Support**

<p align="center">
  <strong>Give a ⭐ if this project helped you!</strong>
</p>

[![Star History Chart](https://api.star-history.com/svg?repos=anuththara2007-W/Bank-Management-System&type=Timeline)](https://star-history.com/#anuththara2007-W/Bank-Management-System&Timeline)

</div>

---

<div align="center">
  
  **Made with ❤️ by Anuththara Wickramasekara**
  
  <sub>🚀 Empowering banking solutions through technology</sub>
  
</div>
