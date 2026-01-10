using Pig.Core;
using Pig.Variations;
using Pig.Console;

System.Console.WriteLine("=== The Pig Dice Game (Immutable State Edition) ===");

/**
 * 1. SELECT VARIATION
 */
System.Console.WriteLine("\nChoose your game variant:");
System.Console.WriteLine("1. Standard Pig (Single Die)");
System.Console.WriteLine("2. Two-Dice Pig");
System.Console.WriteLine("3. Big Pig (Doubles Bonus)");
System.Console.WriteLine("4. Hog (Risk multiple dice)");

int choice = InputHandler.GetInt("Selection", 1, 4);

IPigVariation selectedVariation = choice switch
{
    1 => new StandardPig(),
    2 => new TwoDicePig(),
    3 => new BigPig(),
    4 => new Hog(InputHandler.GetInt("How many dice for the Hog variant?", 2, 10)),
    _ => new StandardPig()
};

/**
 * 2. SETUP PLAYERS
 */
int playerCount = InputHandler.GetInt("Number of players", 2, 10);

List<PlayerState> initialPlayers = [];

for (int i = 1; i <= playerCount; i++)

{

    string name = InputHandler.GetString($"Player {i} Name");

    initialPlayers.Add(new PlayerState(name, 0));

}

/**
 * 3. INITIALIZE STATE
 */
// We create the first snapshot of the game
var game = new GameState(

    Players: initialPlayers.AsReadOnly(),

    CurrentPlayerIndex: 0,

    CurrentTurnScore: 0,

    IsGameOver: false,

    LastRollDescription: "Game Started"

);

/**
 * 4. START GAME EXECUTION
 */
RunGame(selectedVariation, game);

#region Game Flow Orchestrators

/// <summary>
/// Manages the primary interactive game loop.
/// Coordinates between user input and the functional state transitions defined in GameLogic.
/// </summary>
/// <param name="variation">The specific Pig rule set to apply during this session.</param>
/// <param name="currentState">The initial starting state of the game.</param>
void RunGame(IPigVariation variation, GameState currentState)
{
    // The 'game' variable acts as the current pointer to an immutable state snapshot.
    GameState game = currentState;

    while (!game.IsGameOver)
    {
        RenderInterface(game, variation);
        
        var action = InputHandler.GetString("[R]oll or [H]old?").ToUpper();

        if (action == "R")
        {
            // Execute the 'Roll' transformation
            var dice = Enumerable.Range(0, variation.DiceCount)
                                 .Select(_ => Random.Shared.Next(1, 7))
                                 .ToList();

            game = GameLogic.Roll(game, variation, dice);

            // Provide UI feedback if the state indicates a turn-ending event (Bust/Hog-end)
            if (game.CurrentTurnScore == 0 && !game.IsGameOver)
            {
                System.Console.WriteLine($"\nResult: {game.LastRollDescription}");
                InputHandler.Wait();
            }
        }
        else if (action == "H")
        {
            // Execute the 'Hold' transformation
            game = GameLogic.Hold(game, variation);
        }
    }
    
    RenderFinalVictory(game);
}

/// <summary>
/// Clears the console and renders the current game standings, active player, and bank.
/// </summary>
void RenderInterface(GameState state, IPigVariation variation)
{
    System.Console.Clear();
    System.Console.WriteLine($"Mode: {variation.Name} | Target: {variation.WinningScore}");
    DisplayScores(state.Players);
    
    var activePlayer = state.Players[state.CurrentPlayerIndex];
    System.Console.WriteLine($"\n>> {activePlayer.Name}'s Turn");
    System.Console.WriteLine($"Current Bank: {state.CurrentTurnScore}");
    System.Console.WriteLine($"Status: {state.LastRollDescription}");
}

/// <summary>
/// Prints a formatted table of players and their current total scores.
/// </summary>
void DisplayScores(IReadOnlyList<PlayerState> players)
{
    System.Console.WriteLine("-------------------------");
    foreach (var p in players) 
        System.Console.WriteLine($"{p.Name.PadRight(15)}: {p.TotalScore}");
    System.Console.WriteLine("-------------------------");
}

/// <summary>
/// Displays the final standings and declares the winner once the game state is flagged as over.
/// </summary>
void RenderFinalVictory(GameState finalState)
{
    System.Console.Clear();
    System.Console.WriteLine("=== FINAL SCORES ===");
    DisplayScores(finalState.Players);
    var winner = finalState.Players[finalState.CurrentPlayerIndex];
    System.Console.WriteLine($"\nGame Over! {winner.Name} WINS with {winner.TotalScore} points!");
}

#endregion