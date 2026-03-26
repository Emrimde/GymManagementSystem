## 🏋️ Gym Management System

A full-featured gym management system designed to streamline staff operations and improve customer experience. The solution consists of two integrated applications: an employee desktop app and a client web app.

---

## 🚀 Features

### 👨‍💼 Employee Application (WPF)

* Register and manage clients
* Track gym entries
* Manage membership plans
* Organize personal trainings & group classes
* Control class capacity and schedules
* Manage staff (roles, data, employment info)

### 🌐 Client Web Application (Angular)

* Create accounts and manage profiles
* Purchase memberships online
* Book group classes and personal trainings
* Access trainer schedules and participation

### 🧑‍🏫 Trainer Zone

* View upcoming classes
* Monitor assigned sessions and attendance

---

## 🧠 Architecture

* Centralized data management
* Consistent data shared between applications
* REST API handling business logic and communication

---

## 🛠️ Tech Stack

* **Frontend:** Angular, HTML, CSS, Bootstrap
* **Desktop App:** WPF
* **Backend:** ASP.NET Core (C# Web API)
* **Database:** PostgreSQL

---

## ⚙️ How to Run

### 📦 1. Setup Project

* Clone repository or download ZIP
* Extract files and open solution in **Visual Studio**
* Select `GymManagementSystem.sln`

---

### 🗄️ 2. Setup Database (PostgreSQL)

* Create database:

```bash
GymManagementSystemDb
```

* Restore file "backup" from main folder:

* Update connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "ConnectionString": "Server=localhost;Database=GymManagementSystemDb;Username=postgres;Password=YOUR_PASSWORD;Port=5432;"
}
```

---

### 🔐 3. Configure Secrets (Optional)

In `GymManagementSystem.API` run:

```bash
dotnet user-secrets set "Jwt:Key" "your_key"
```

#### 📧 SMTP (Optional)

SMTP is used in some parts of the system (e.g. email notifications).
If not configured, these features will not work, but the application will run normally.

```bash
dotnet user-secrets set "Smtp:Password" "your_password"
```

---

### ▶️ 4. Run Backend + WPF

* Set startup project to:
  👉 **API + WPF**

* Run the application

Default credentials:

```bash
Email: owner@gym.local
Password: Owner123!
```

---

### 🌐 5. Run Web Application (Angular)

```bash
cd GymManagementSystem.Web
npm install
ng serve
```

Open in browser:

```bash
http://localhost:4200
```





