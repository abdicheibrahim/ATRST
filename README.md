# 📦 Scientific Research Management System

## 📝 Description
A web application for managing scientific research projects, developed using **ASP.NET Core MVC**, **Entity Framework Core**, and a **layered architecture** (Repository, Service, MVC).

---

## 🚀 Main Features
- Authentication and role management: **Admin**, **Expert**, **Researcher**, **Leader**, **Member**
- Project creation and management by **Leaders**
- **Join requests** / **Invitations** between researchers
- **Scientific validation** by Experts
- **Administrative approval** of projects
- **Multilingual interface**: 🇫🇷 French, 🇬🇧 English, 🇩🇿 Arabic

---

## 🛠️ Technologies Used

| Area            | Technologies                                                                 |
|------------------|-------------------------------------------------------------------------------|
| Backend          | ASP.NET Core MVC, C#                                                         |
| Database         | MySQL, Entity Framework Core                                                 |
| Authentication   | ASP.NET Core Identity                                                        |
| Frontend         | HTML, CSS, Bootstrap 5                                                       |
| Architecture     | Repository Pattern, Service Layer, Specification Pattern                    |
| Tools            | Git, GitHub                                                                  |

---

## 🔐 Authentication & Security
- Authentication using **ASP.NET Identity**
- Role and access **management**
- Route protection using `[Authorize]`
- **Server-side** and **client-side** validation

---

## 📦 Database
- **Migrations** managed via EF Core
- Main relationships:
  - A **Researcher** can be either a **Leader** or a **Member**, not both
  - A **Project** can have multiple members
  - **Experts** validate the projects
- Main tables:
  - Projects, Researchers, Experts, Memberships, Invitations

---

## 🌍 Internationalization
Language support:
- 🇫🇷 French (default)
- 🇬🇧 English
- 🇩🇿 Arabic

**Dynamic selection** via cookie or URL parameter.

---

## 📸 Screenshots *(optional)*

> Add screenshots of the dashboard, project creation page, etc.

---

## 👨‍💻 Author

**Abdiche Ibrahim**  
Full Stack .NET Developer  
📧 [abdicheibrahim@gmail.com](mailto:abdicheibrahim@gmail.com)  
📍 Algeria