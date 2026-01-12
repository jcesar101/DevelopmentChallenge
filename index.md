# Intelerad UK - Technical Assessment
### Senior Software Engineer Challenge | Julio Cesar Cortes Rios

Welcome to the technical submission for the Senior Software Engineer position. This project demonstrates a clean-code approach to game engine design.

---

## C# Development Challenge (Pig)

A robust implementation of the "Pig" dice game family, built with a focus on **extensibility** and **state integrity**.

### Key Architectural Highlights
* **Strategy Pattern**: Employs an `IPigVariation` interface to handle diverse rulesets (Standard, Big Pig, Hog) without modifying core engine logic.
* **Functional State Management**: Uses C# **Records** for `GameState` and `PlayerState` to ensure immutability and value-based equality.
* **Domain-Driven Design (DDD)**: Clear separation between the core domain, variation logic, and the console interface.
* **Modern Stack**: Developed using **C# 14** and **.NET 10**.

[**Go to Development Challenge Docs**](docs/getting-started.md)

---

## Quick Navigation

* [**Getting Started**](docs/getting-started.md) - Build, run, and test the C# project.
* [**Technical Introduction**](docs/introduction.md) - Deep dive into design decisions and strategy patterns.
* [**GitHub Repository**](https://github.com/jcesar101/DevelopmentChallenge) - Access the full source code.