## **Development Challenge**

### Pig (Dice game)

[![CI and Deploy Docs](https://github.com/jcesar101/DevelopmentChallenge/actions/workflows/main.yml/badge.svg)](https://github.com/jcesar101/DevelopmentChallenge/actions/workflows/main.yml)

#### 

#### The Game Rules

<sub>[taken from here](https://en.wikipedia.org/wiki/Pig_(dice_game))</sub>

- Standard Pig (1 dice)
  
  - Goal: Reach 100 points
  
  - Turn: Roll a die repeatedly. Add the roll to a "running total."
  
  - The "1" Rule: If you roll a 1, your turn ends and you gain zero points for that turn
  
  - Hold: You may choose to "hold," adding your running total to your score and passing the turn

- Standard Pig (2 dice)
  
  - Same as 1-dice version, except for "1" rule: If a single 1 is rolled, the player scores nothing and the turn ends. If two 1s are rolled, the player's entire score is lost and the turn ends

- Big Pig
  
  - Same as 2-dice version, except: If two 1s are rolled, the payer adds 25 to the turn total. If other doubles are rolled, the player adds twice the value of each dice to the turn total.

- Hog
  
  - Similar as 1-dice version, but with a variable number of dice to roll. If they roll any 1s, they score zero for their turn; otherwise they score the sum of the dice.  The target score might be different from 100



#### Design decisions and assumptions

- The four variants of the game listed in the previous section were chosen to simplify the implementation and demonstrate the flexibility of changing playing variations
- For the design, the **Strategy** pattern enables the flexibility to choose from different game rules using the `IPigVariation` interface. The logic is "rule-agnostic"—it simply follows the `RollResult` instructions provided by the selected variation
- For the `PlayerState` and `GameState` records were used for built-in value-based equality and immutability
- To minimize repeated code **Global Usings** are included
- xUnit was chosen for the unit tests
- To create the project structure, a **Domain-driven** approach with **Separation of concerns** was selected to separate the game logic, the rule variations, the user interface and the tests:
  
  ```
  /DevelopmentChallenge
    ├── Pig.Core          # GameState records and GameLogic transformer
    ├── Pig.Variations    # Standard, BigPig, Hog, and Two-Dice logic
    ├── Pig.Console       # UI, InputHandler, and Program loop
    └── Pig.Tests         # xUnit tests for all core logic
  ```
- The project uses **Deterministic Unit Testing**. Instead of relying on random numbers, we pass specific dice arrays (e.g., `[1, 1]`) into the state transformer to verify that the resulting `GameState` reflects the correct score and active player
- The code was written in C#14 / .NET 10.0.101 to benefit from the latest features and longest support. It could also be run on Windows, Linux and macOS without modification



#### Setup and running/testing the game

- Prerequisites: [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Installation
  
  ```bash
  # Clone the repository
  git clone https://github.com/jcesar101/DevelopmentChallenge
  
  # Build the solution
  dotnet build
  ```
- Execution

- ```bash
  dotnet run --project Pig.Console/Pig.Console.csproj
  ```
- Running tests
  
  ```bash
  dotnet test
  ```
