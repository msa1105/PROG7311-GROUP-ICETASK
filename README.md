# PROG7311-GROUP-ICETASK
# 💸 FinanceTrack

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-blueviolet)
![C%23](https://img.shields.io/badge/C%23-Backend-239120)
![SQLite](https://img.shields.io/badge/SQLite-Database-003B57)
![Docker](https://img.shields.io/badge/Docker-Supported-2496ED)
![Status](https://img.shields.io/badge/Status-Student%20Project-success)



## 📌 Project Overview

**FinanceTrack** is a personal finance management web application built using **ASP.NET Core MVC**.

The application helps users track and view their financial information through a simple dashboard that displays income, expenses, balances, receivables, payables, assets, and liabilities.

FinanceTrack was designed to provide users with a clear financial overview while demonstrating good software development practices such as MVC architecture, service-based logic, database integration, and clean project organization.



## ✨ Key Features

✅ User login and logout functionality  
✅ Dashboard with financial summary cards  
✅ Monthly income vs expenses chart  
✅ Ledger section for financial transactions  
✅ Receivable and payable tracking  
✅ Assets and liabilities section  
✅ Responsive web interface  
✅ Service-based business logic  
✅ Entity Framework Core database integration  
✅ Docker support for containerized deployment  



## 🛠️ Technologies Used

| Technology | Purpose |
|---|---|
| **ASP.NET Core MVC** | Web application framework |
| **C#** | Backend programming language |
| **Entity Framework Core** | Database access and ORM |
| **SQLite** | Local application database |
| **HTML** | Page structure |
| **CSS** | Styling and layout |
| **JavaScript** | Frontend interactivity |
| **Chart.js** | Dashboard charts |
| **Docker** | Containerization |
| **GitHub Actions** | Automation support |



## 🗂️ Project Structure

```text
FinanceTrack/
│
├── Controllers/
│   ├── AccountController.cs
│   ├── HomeController.cs
│   └── TransactionsController.cs
│
├── Controllers/Api/
│   └── TransactionsApiController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Interfaces/
│   └── ITransactionService.cs
│
├── Models/
│   └── Application models
│
├── Services/
│   └── TransactionService.cs
│
├── Views/
│   └── Razor views
│
├── wwwroot/
│   └── CSS, JavaScript, images, and static files
│
├── Program.cs
├── appsettings.json
├── Dockerfile
├── docker-compose.yml
└── README.md
👥 Team Roles
🎨 Member 1: Frontend & UI/UX Specialist

Focus: Views and wwwroot

Responsible for designing the user interface, improving page layouts, styling the application, ensuring responsiveness, and creating a smooth user experience.

🧩 Member 2: Backend API & Controllers

Focus: Controllers

Responsible for handling HTTP requests, routing logic, controller actions, validation, page navigation, and connecting the user interface to the service layer.

⚙️ Member 3: Core Business Logic & Services

Focus: Services and Interfaces

Responsible for implementing financial calculations, transaction processing, service methods, and keeping business logic separate from controllers.

🗄️ Member 4: Data & Infrastructure Architect

Focus: Data, Models, and database configuration

Responsible for database structure, Entity Framework Core setup, model design, database integrity, and data configuration.

🚀 Member 5: DevOps, QA & Maintenance Lead

Focus: Dockerfile, docker-compose.yml, Program.cs, deployment, and maintenance

Responsible for containerization, application startup configuration, CI/CD support, deployment preparation, code reviews, and maintaining application stability.

🖥️ Main Application Pages
📊 Dashboard

The dashboard provides a quick financial overview using summary cards and a monthly income vs expenses chart.

Includes:

Total balance
Net position
Net profit
Income vs expenses graph
📒 Ledger

The ledger page displays financial transaction records and gives users an organized view of income and expenses.

💰 Receivable / Payable

This page tracks money owed to the user and money the user still needs to pay.

🏦 Assets & Liabilities

This section helps users view their assets and liabilities to better understand their overall financial position.

🏗️ Application Architecture

FinanceTrack follows the Model-View-Controller architectural pattern.

🧱 Model

Models define the structure of the application data.

🖼️ View

Views are Razor pages responsible for displaying the user interface.

🧭 Controller

Controllers handle requests, process user actions, call services, and return views or responses.

⚙️ Service Layer

The service layer contains business logic and financial processing rules.

🗄️ Data Layer

The data layer uses Entity Framework Core to communicate with the SQLite database.

🧠 Design Principles
✅ Separation of Concerns

The system separates responsibilities between controllers, services, models, views, and the data layer. This makes the code easier to understand, maintain, and extend.

✅ Dependency Injection

Services are injected into controllers instead of being created directly inside controller classes. This improves flexibility and supports cleaner code.

✅ MVC Pattern

The project uses ASP.NET Core MVC to keep application logic structured and organized.

✅ Service-Based Logic

Financial calculations and transaction-related logic are handled in services instead of being placed directly inside controllers.

🔌 Controller & API Functionality

The controller layer is responsible for:

Handling browser requests
Returning Razor views
Managing navigation between pages
Validating incoming data
Calling service methods
Returning structured API responses where needed

The application also includes an API controller for transaction-related functionality, allowing transaction data to be accessed in a structured way.

🧾 Database Information

FinanceTrack uses Entity Framework Core with SQLite.

The database is configured in Program.cs using an application database context.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

The application can create the database automatically when it starts, depending on the configuration.

🐳 Docker Support

FinanceTrack includes Docker support for containerized deployment.

Included files:

Dockerfile
docker-compose.yml

To run the application using Docker:

docker-compose up --build
⚙️ Installation & Setup
📌 Prerequisites

Before running the application, make sure you have:

Visual Studio 2022 or later
.NET SDK
Git
Docker Desktop, optional
▶️ Running the Application in Visual Studio
Clone the repository:
git clone https://github.com/msa1105/PROG7311-ALTF4-ICETASK.git
Open the project in Visual Studio.
Open the FinanceTrack.csproj file.
Restore NuGet packages if required:
Build > Restore NuGet Packages
Set FinanceTrack as the startup project.
Run the application by pressing:
F5

or by clicking the green play button in Visual Studio.

💻 Running the Application Using Command Line

Navigate to the project folder:

cd FinanceTrack

Run the following commands:

dotnet restore
dotnet build
dotnet run

After the application starts, open the localhost link shown in the terminal.

Example:

https://localhost:53096
📸 Screenshots

Add final screenshots before submission.

📊 Dashboard
<img width="1600" height="999" alt="image" src="https://github.com/user-attachments/assets/0b2e2709-ed58-4696-ab86-fea89c3b8211" />

📒 Ledger
<img width="1600" height="903" alt="image" src="https://github.com/user-attachments/assets/2e8ae9be-bdc6-4fda-84f4-f2b0fb02a0f0" />

💰 Receivable / Payable
<img width="1600" height="920" alt="image" src="https://github.com/user-attachments/assets/061b1eef-bb67-44a8-b3b4-2ea858392072" />

🏦 Assets & Liabilities
<img width="1600" height="840" alt="image" src="https://github.com/user-attachments/assets/7ab84caa-52f6-4e6d-b9e8-8caa2dc350c8" />

🔮 Future Improvements
Add full transaction create, edit, and delete functionality
Add category-based spending analysis
Add date range filters
Add transaction search functionality
Add user-specific financial records
Improve dashboard chart interactivity
Add export to PDF or CSV
Improve mobile responsiveness
Add notifications for upcoming payments
Add budgeting goals and alerts
👨‍💻 Contributors
Member 1: Frontend & UI/UX Specialist
Member 2: Backend API & Controllers
Member 3: Core Business Logic & Services
Member 4: Data & Infrastructure Architect
Member 5: DevOps, QA & Maintenance Lead
✅ Conclusion

FinanceTrack is a structured ASP.NET Core MVC finance tracking application that demonstrates financial data management, dashboard reporting, MVC architecture, service-layer logic, database integration, and deployment support.

The application provides a strong foundation for managing personal finances and can be extended with more advanced analytics, reporting, and user-specific financial features.
