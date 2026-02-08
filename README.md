# Hierarchical Menu System (C# / .NET)

A reusable **console-based hierarchical menu framework**, implemented **twice**:
1. **Interfaces-based** menu actions
2. **Delegates/Events-based** menu actions (`Action<T>`)

This project was built as an OOP exercise focusing on **polymorphism**, **interfaces**, and **delegates/events**, while providing a clean way to build multi-level console menus for other applications.

---

## What this project includes

This Visual Studio solution contains **3 projects**:

- **Menus.Interfaces** *(Class Library)* – Menu infrastructure using interfaces
- **Menus.Events** *(Class Library)* – Menu infrastructure using delegates/events (`Action<T>`)
- **Menus.Test** *(Console App)* – Demo application that builds and runs two menus (one per technique)

---

## Features

- Multi-level menu navigation (unlimited depth)
- Clear menu rendering per level (title, numbered items, and `0` for Back/Exit)
- Input validation + friendly re-prompting on invalid choices
- `Console.Clear()` between screens for a clean UX
- Leaf-items execute actions; after completion the current menu re-displays

---

## Demo menu specification (Test project)

The test application demonstrates two menus (interfaces first, then delegates/events), each with **3 levels**.

Top level:
1. **Version and Capitals**
   - **Show Version** → prints: `App Version: 24.2.4.9504`
   - **Count Capitals** → asks for text input and prints number of uppercase letters
2. **Show Date/Time**
   - **Show Time** → prints current time
   - **Show Date** → prints current date

---

## Getting started

### Prerequisites
- **Visual Studio 2022** (recommended) with **.NET** workload
- Or any IDE that can build **C#/.NET** solutions

### Run the demo (from source)
1. Open the solution: `Hierarchical Menu System.sln`
2. Set **Menus.Test** as the Startup Project
3. Run (F5 / Ctrl+F5)

---

## Download & Run (Releases)

If you prefer running the project without opening it in an IDE, download the ready-to-run build from **GitHub Releases**:

- **Latest Release page:** https://github.com/dorhaboosha/Hierarchical-Menu-System/releases/latest

### Option A: Download the executable (recommended)
1. Download the ZIP release asset:
   - `HierarchicalMenuSystem-V1.0.0.zip`
2. **Extract** the ZIP to a folder (don’t run the exe from inside the zip).
3. Run:
   - `Menus.Test.exe`

> Note: Keep all extracted files together in the same folder (the `.exe` and the `.dll` files), otherwise the app may not start.

### Option B: Download source code (GitHub auto-generated)
In the release page, GitHub also provides **Source code (zip)** / **Source code (tar.gz)**.  
These are auto-generated snapshots of the repository at that release tag (for developers who want to build locally).

---
