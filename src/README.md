# Expense Tracker API

## Overview
The Expense Tracker API will allow users to:
1. **Log expenses**: Add expense details like amount, category, date, and description.
2. **Categorize expenses**: Group expenses under categories like Food, Travel, etc.
3. **Track spending trends**: Generate reports by category, month, or custom date range.
4. **Manage budgets**: Set budgets for categories and track progress.
5. **Group expenses**: Share expenses with groups (e.g., trips, parties) and calculate balances.

---

## Domain Entities
1. **User**: Represents an individual using the system.
   - Properties: `Id`, `Name`, `Email`, `PasswordHash`, etc.

2. **Expense**: Represents a single expense record.
   - Properties: `Id`, `UserId`, `Amount`, `CategoryId`, `Date`, `Description`.

3. **Category**: Represents expense categories.
   - Properties: `Id`, `Name`, `UserId` (for user-specific categories).

4. **Budget**: Represents a budget for a specific category and period.
   - Properties: `Id`, `UserId`, `CategoryId`, `Amount`, `StartDate`, `EndDate`.

5. **Group**: Represents a shared expense group.
   - Properties: `Id`, `Name`, `OwnerId`.

6. **GroupExpense**: Represents an expense in a group.
   - Properties: `Id`, `GroupId`, `Amount`, `Description`, `Date`.

---

## Use Cases
1. **User Management**:
   - Register a user.
   - Login and secure sessions with JWT tokens.

2. **Expense Management**:
   - Add, update, or delete expenses.
   - Retrieve expenses for a specific time frame.
   - Search/filter expenses by category, date, or amount.

3. **Category Management**:
   - Add, edit, or delete categories.
   - List all user-specific and default categories.

4. **Budget Management**:
   - Set a budget for a specific category and date range.
   - Get notifications when nearing or exceeding a budget.

5. **Group Expense Management**:
   - Create groups and invite members.
   - Add group expenses and calculate individual shares.
   - Settle balances within groups.

6. **Reports**:
   - Generate monthly spending reports.
   - Summarize expenses by category.

---

## Technology Stack
- **Framework**: ASP.NET Core 8.
- **Database**: SQL Server or PostgreSQL using EF Core 8.
- **Authentication**: JWT with Identity or custom authentication.
- **Caching**: Redis for frequently accessed reports.
- **Testing**: xUnit for unit testing, FluentAssertions for assertions.

---

## Architecture Layers

1. **Core/Domain Layer**:
   - Define entities like `Expense`, `Category`, `Budget`, etc.
   - Create interfaces for services: `IExpenseService`, `IReportService`, etc.
   - Add business rules, such as:
     - No negative expense amounts.
     - Enforcing unique categories per user.

2. **Application Layer**:
   - Implement use cases such as:
     - `AddExpenseHandler` to handle expense creation.
     - `GenerateMonthlyReportHandler` for report generation.
   - Map domain entities to DTOs using tools like AutoMapper.

3. **Infrastructure Layer**:
   - Use EF Core for data access and repository patterns:
     - `IExpenseRepository`, `ICategoryRepository`.
   - Send email notifications using a mail service like SendGrid.

4. **Presentation Layer**:
   - Create controllers for endpoints:
     - `ExpensesController`: `/api/expenses`
     - `CategoriesController`: `/api/categories`
     - `ReportsController`: `/api/reports`
   - Validate inputs using FluentValidation.

---

## Applying SOLID Principles

- **SRP**:
   - Separate services for handling expenses, budgets, and reports.
   - Keep controllers thin by delegating logic to services.

- **OCP**:
   - Make report generation extensible by supporting custom filters.
   - Allow adding new notification mechanisms (e.g., SMS) without changing existing code.

- **LSP**:
   - Ensure repository implementations can replace their interfaces seamlessly.

- **ISP**:
   - Use focused interfaces like `IExpenseRepository` and `ICategoryRepository`.

- **DIP**:
   - Inject service and repository dependencies using .NET's built-in DI container.

---

## Example API Endpoints

### 1. User Management
- `POST /api/auth/register`: Register a user.
- `POST /api/auth/login`: Authenticate and get a JWT.

### 2. Expense Management
- `POST /api/expenses`: Add a new expense.
- `GET /api/expenses`: Get expenses (with optional filters).
- `PUT /api/expenses/{id}`: Update an expense.
- `DELETE /api/expenses/{id}`: Delete an expense.

### 3. Category Management
- `POST /api/categories`: Add a new category.
- `GET /api/categories`: Get all categories.

### 4. Budget Management
- `POST /api/budgets`: Set a budget.
- `GET /api/budgets`: Get all budgets.

### 5. Reports
- `GET /api/reports/monthly`: Get a monthly expense report.

### 6. Group Expense Management
- `POST /api/groups`: Create a group.
- `POST /api/groups/{id}/expenses`: Add a group expense.
- `GET /api/groups/{id}/balances`: Get group balances.

---