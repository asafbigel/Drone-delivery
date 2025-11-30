# 🚁 Drone Delivery Simulation

A **C# project** simulating a drone-based delivery system.  
This project demonstrates **software engineering principles** such as clean Object-Oriented Design, modular architecture, and test-driven development practices.

---

## 📖 Table of Contents
- [Overview](#-overview)
- [Features](#-features)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Code Example](#-code-example)
- [Architecture](#-architecture)
- [Tests](#-tests)
- [Future Improvements](#-future-improvements)
- [License](#-license)

---

## 🔎 Overview
The **Drone Delivery System** simulates real-world package delivery using autonomous drones.  
It covers the process from **order placement** by customers, to **drone assignment** and **successful delivery**.

This project highlights:
- Object-Oriented Programming (OOP) design.
- Encapsulation of business logic in dedicated classes.
- Unit testing of core functionalities.
- Extendable architecture for future improvements.

---

## ✨ Features
- 📦 **Order Management** – customers can place and track orders.  
- 🚁 **Drone Scheduling** – automatically assigns available drones to deliveries.  
- ⏳ **Simulation Engine** – step-by-step simulation of drone operations.  
- ✅ **Unit Tests** – validating system correctness.  

---

## 📂 Project Structure
This project is structured into several layers, following a clean architecture approach:

```
Drone-delivery/
│── dotNet5782_9647_3571.sln  # Visual Studio Solution file
│── BL/                      # Business Logic Layer
│   ├── BlApi/               # Interfaces for BL
│   ├── BO/                  # Business Objects
│   └── Simulator.cs         # Drone simulation logic
│── DalFacade/               # Data Access Layer Facade (Interfaces for DAL)
│   └── DO/                  # Data Objects
│── DalObject/               # Concrete implementation of DAL using in-memory collections
│── DLXML/                   # Concrete implementation of DAL using XML files
│── PL/                      # Presentation Layer (WPF User Interface)
│   ├── ...                  # XAML and C# files for the UI
│── ConsoleUI/               # Console User Interface
│── Targil0/                 # Initial project setup/testing
│── xml/                     # XML data files for DLXML implementation
│── BaseStationsXml.xml      # XML file for Base Stations data
└── README.md                # Project documentation
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download) or later
- Visual Studio / VS Code with C# extension

### Installation
```bash
git clone https://github.com/asafbigel/Drone-delivery.git
cd Drone-delivery
```

### Run the Application
1. Open the solution file `dotNet5782_9647_3571.sln` in Visual Studio.
2. Set the `PL` project as the startup project.
3. Run the application (F5 or Ctrl+F5).

### Run Unit Tests
```bash
dotnet test
```

---

## 🏗 Architecture

The system is built using **N-tier architecture** with clear separation of concerns:

```mermaid
graph TD
    PL[Presentation Layer (WPF)] --> BL[Business Logic Layer]
    BL --> DalFacade[Data Access Layer Facade]
    DalFacade --> DalObject[DAL Object (In-memory)]
    DalFacade --> DLXML[DAL XML (XML Files)]
    ConsoleUI[Console UI] --> BL


---

## 🧪 Tests
The project includes unit tests for:
- Drone assignment logic  
- Order creation and tracking  
- Simulation flow  

Run with:
```bash
dotnet test
```

---

## 🔮 Future Improvements
- 📊 **Advanced Scheduling** – use shortest path, priority queues, or load balancing.  
- 🌍 **Geolocation Support** – simulate real maps and delivery distances.  
- 🔋 **Battery Management** – model drone battery and charging cycles.  
- 🌐 **UI / Dashboard** – visualize drones and orders in real-time.  
- ☁ **Cloud Integration** – connect to external APIs for scalability.  

---

## 📜 License
This project is licensed under the MIT License.  
Feel free to use, modify, and share it.
