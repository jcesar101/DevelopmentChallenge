# Introduction

# Development Challenge: Pig Dice Game
----
## Overview
This repository contains a robust, extensible implementation of the classic "Pig" dice game and its popular variations. The project was developed as part of a technical assessment, prioritizing **clean code**, **domain-driven design (DDD)**, and **architectural flexibility**.

---

## Core Architectural Decisions

### 1. Strategy Pattern for Game Variations
To accommodate four distinct rule sets (Standard 1-Dice, Standard 2-Dice, Big Pig, and Hog) without creating a "God Object," I implemented the **Strategy Pattern**. 
* **`IPigVariation` Interface:** Defines the contract for turn logic and scoring.
* **Rule Agnosticism:** The core game engine doesn't know which version it is playing; it simply processes the `RollResult` provided by the strategy.

### 2. Functional State Management
I utilized C# **Records** for `PlayerState` and `GameState`. This ensures:
* **Immutability:** State cannot be modified unexpectedly; instead, a new state is returned via a "transformer" logic.
* **Value-Based Equality:** Simplifies testing and debugging by comparing data rather than object references.

### 3. Separation of Concerns
The solution is divided into four distinct layers:
* **Pig.Core:** The "Brain." Contains domain records and the state transition logic.
* **Pig.Variations:** The "Rules." Encapsulates specific game mechanics.
* **Pig.Console:** The "Skin." Handles I/O, user interaction, and the game loop.
* **Pig.Tests:** The "Safety Net." xUnit tests covering all edge cases.

### 4. Deterministic Testing
Rather than relying on `System.Random` in unit tests—which leads to flaky results—the test suite uses **Deterministic Unit Testing**. I inject specific dice arrays into the state transformer to verify that the game reacts correctly to specific outcomes (e.g., rolling double 1s in Big Pig).

---

## Tech Stack
* **Language:** C# 14
* **Runtime:** .NET 10 (Current preview/latest support)
* **Test Framework:** xUnit

---