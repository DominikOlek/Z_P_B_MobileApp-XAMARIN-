# Integrated Security Platform (ZPB)
## This is only mobile part

## 📌 Project Objective
The objective of the Integrated Security Platform (ZPB) was to create a comprehensive system supporting both authorities and citizens in the fields of public safety, road traffic, and health. The platform consolidates data from various institutions and makes it available in one place, with the aim of:

- **Raising safety awareness** – via a mobile app featuring first‑aid advice, guidelines for talking to the 112 dispatcher, a fines tariff, and access to epidemiological data.
- **Simplifying access to information** – centralizing medical data (prescriptions, quarantine), traffic fines, and penalty points.

---

## 🧩 Core System Components

### 1. Mobile App & Website
For citizens, it enables:
- Viewing issued fines, penalty points, and violation history  
- Checking the status of driving privileges  
- Accessing quarantine and court‑imposed ban information  
- A knowledge base on safety procedures  

### 2. Web Application (ASP.NET Core)
For authorities, features depend on user role:
- **Traffic Officer**: issuing fines, verifying driver data, viewing history  
- **Commander**: managing users and tariff entries, reviewing fines and bans  
- **Administrator**: automating server processes (BAT scripts), managing scheduled tasks  
- **Sanitary Inspector**: quarantine control, editing epidemiological records  
- **Driving Exam Centre (WORD)**: accessing information on drivers banned from driving  
- **Driver**: self‑service access to personal data after verification  

### 3. Server & Database
- **Technology**: ASP.NET Core, Entity Framework Core, SQL Server  
- **Security**: JWT (JSON Web Token), two‑step validation (client + server)  
- **Automation**: daily batch queries (BAT) to:
  - Verify fine payments  
  - Check for statute‑of‑limitations expirations  
  - Restore driving privileges  
  - Update quarantine and ban statuses  

### 4. Web API
- REST‑based architecture  
- Handles HTTP requests (authentication, data management, email sender)  
- Data mapping via DTOs and AutoMapper  
- Error logging with NLog  
- JSON handling with Newtonsoft.JSON  
- Email services via MailKit  

---

## 🛠 Technologies
- **Backend**: ASP.NET Core, Entity Framework Core, JWT, AutoMapper, FluentValidation, NLog, MailKit  
- **Frontend**: HTML5, CSS3, JavaScript, jQuery, AJAX, Xamarin  
- **Database**: MSSQL Server, managed via ORM  
- **Mail Server**: sends notifications for fines, loss/restoration of privileges, etc.  

---

## ⚙️ Example Use Cases
- Traffic Officer logs in → searches driver by national ID → issues a fine using the tariff → sends it → receives confirmation  
- Commander logs in → reviews fines → updates tariff entries → changes payment status  
- Sanitary Inspector adds a quarantine record → system warns the Traffic Officer when issuing a fine  
- Driver uses the app to check their penalty points, active fines, and history  

---

## 🔐 Security & Compliance
- Adapted to 2022 legislative changes  
- Fines older than 2 years are archived  
- Special point‑limit handling for novice drivers  
- Two‑stage data validation  
- JWT tokens refreshed every 15 minutes  

---

## 🚀 Potential Uses
- Emergency backup for existing police systems  
- Inspiration for modernizing current solutions  
- Framework for splitting into smaller, interoperable subsystems  
