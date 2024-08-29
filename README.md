# Hierarchical-Menu-System
The Hierarchical Menu Management System is a console-based application designed to facilitate the creation and management of hierarchical menus for console-based applications.
This system enables developers to build and navigate complex menu structures efficiently, using both interfaces and delegates.

The core of the system is the 'MainMenu' class, which allows developers to define and manage a hierarchical menu structure.
This class supports creating menu items at various levels, handling user input, and executing corresponding actions based on the user's selection.
The menu displays options in a user-friendly format, supports navigation between different menu levels, and performs actions such as executing operations or displaying sub-menus.

The project is implemented in **C#** and **.NET**, utilizing key technologies to ensure functionality and flexibility.
**C#** is used for implementing object-oriented programming principles, including **classes**, **interfaces**, and **delegates**.
**.NET** is employed for structuring the application and managing dependencies between projects.
**Visual Studio** serves as the integrated development environment (IDE) for writing, debugging, and running the application.

**Interfaces** are used to define contracts for menu item operations and ensure a consistent approach to handling menu actions.
**Delegates (Action<T>)** are employed to enable dynamic method invocation for handling menu actions.
**Collections** are utilized to manage and organize menu items, including hierarchical relationships between menu levels.
**Custom exception handling** manages errors such as invalid menu selections or navigation issues.

The solution consists of three projects:

1. **Ex04.Menus.Interfaces**: Implements the menu system using **interfaces**. This project is a **Class Library (.dll)** and provides the core logic for managing menu structures and operations.
2. **Ex04.Menus.Events**: Implements the menu system using **delegates (Action<T> or similar)**. This project is also a **Class Library (.dll)** and focuses on dynamic method invocation for menu actions.
3. Ex04.Menus.Test: Demonstrates the use of the above two projects by creating and managing two different menu systems, each with a three-level menu structure. This project is an **application (.exe)** that runs and tests the functionality of the menu systems.

The test application builds two menus: one using the interface-based approach and one using the delegate-based approach. Each menu features options to display version information, count uppercase letters, and show date/time, providing a practical demonstration of the menu management system.

This approach ensures a well-structured and flexible system for managing hierarchical menus, supporting easy integration into various console-based applications.
