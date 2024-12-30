This application was developed for Web Application module, as coursework portfolio project @

### **Prerequisites**
In order to run this project, you need the following installed on your computer:  
- **.NET 8.0 SDK**  
- **Node.js (v16 or later)**:  
- **Angular CLI**: You can install this using `npm install -g @angular/cli`  
- **SQL Server**: A local SQL Server instance for the database

---

### **Setup and Run Instructions**
Follow these steps to set up and run the project:

#### **1. Clone the Repository**
Clone the repository using HTTP or SSH. You can do it by just downloading the ZIP file as well.

#### **2. Set Up the API (Backend)**
1. Open the backend project folder in your IDE.  
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Update the database (THIS IS IMPORTANT):
   ```bash
   dotnet ef database update
   ```
4. Run the API:
   ```bash
   dotnet run
   ```

#### **3. Set Up the SPA (Frontend)**
1. Navigate to the Angular project folder:

2. Install dependencies:
   ```bash
   npm install
   ```
3. Start the development server:
   ```bash
   ng serve -o
   ```
4. Access the SPA in your browser at `http://localhost:4200`.

---

### **What the Project is about?**
This project is a **Real Estate Listing Web Application** that allows users to manage property listings and agent details. The application has two main parts:

1. **API (Backend)**:
   - Developed using **ASP.NET Core Web API** with **Entity Framework** for database management.
   - Features a data model with two tables: `Properties` and `Agents`.
   - Provides CRUD operations using Swagger.

2. **Single Page Application (Frontend)**:
   - Built with **Angular**.
   - Performs real-time CRUD operations on the backend through REST API.
   - Includes intuitive forms, listing pages, and search functionality for user convenience.
