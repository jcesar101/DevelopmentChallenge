# Getting Started

Follow these instructions to build, run, and test the Pig Dice Game on your local machine.

## Prerequisites

* **.NET 10 SDK**: Ensure you have the latest SDK installed.
* **Terminal**: Access to Bash, PowerShell, or a standard Command Prompt.

---

## Installation

1.  **Clone the repository**:
    ```bash
    git clone [https://github.com/jcesar101/DevelopmentChallenge](https://github.com/jcesar101/DevelopmentChallenge)
    cd DevelopmentChallenge
    ```

2.  **Build the solution**:
    ```bash
    dotnet build
    ```

---

## Execution

To launch the game, run the console project. You will be prompted to select your game variation (Standard, Big Pig, or Hog) and enter player names.

```bash
dotnet run --project Pig.Console/Pig.Console.csproj
```

---

## Running the Game
Launch the console application to start playing. You will be prompted to select a game variation and enter player names.

```bash
dotnet run --project Pig.Console/Pig.Console.csproj
```

---

## Running Tests
The project includes a comprehensive suite of xUnit tests to ensure the integrity of the game rules and state transitions.

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"
```

## Project Structure

```plaintext
/DevelopmentChallenge
  ├── Pig.Core          # Immutable GameState and Logic Transformer
  ├── Pig.Variations    # Rule implementations (Standard, BigPig, Hog)
  ├── Pig.Console       # CLI User Interface and Input Handling
  └── Pig.Tests         # Unit tests with deterministic data
```

---