namespace Pig.Variations;

using Pig.Core;

/// <summary>
/// Implements the "Hog" variation of the game.
/// Unlike standard Pig, Hog is a single-action strategy game where the player 
/// decides how many dice to roll at once, but only receives one throw per turn.
/// </summary>
public class Hog(int diceToRoll, int winningScore = 100) : IPigVariation
{
    /// <summary>
    /// Gets the display name, including the number of dice chosen for this instance.
    /// </summary>
    public string Name => $"Hog ({diceToRoll} dice)";

    /// <summary>
    /// Gets the score required to win the game.
    /// </summary>
    public int WinningScore => winningScore;

    /// <summary>
    /// The number of dice the player has committed to roll for their single turn attempt.
    /// </summary>
    public int DiceCount => diceToRoll;

    /// <summary>
    /// Evaluates the Hog roll:
    /// <list type="bullet">
    /// <item><description>If any die is a 1: The player scores 0 and the turn ends.</description></item>
    /// <item><description>Otherwise: The player scores the sum of all dice and the turn ends.</description></item>
    /// </list>
    /// </summary>
    /// <param name="dice">The collection of dice values rolled in the single attempt.</param>
    /// <returns>A <see cref="RollResult"/> where <see cref="RollResult.IsTurnOver"/> is always true.</returns>
    public RollResult ProcessRoll(IEnumerable<int> dice)
    {
        // Rule: If any dice are 1, the player "hogs" the dice and gets 0 for the turn.
        if (dice.Any(d => d == 1)) 
            return new RollResult(0, true);

        // Rule: On a successful roll, sum the dice. 
        // In this variant, the turn ends automatically after the first roll.
        return new RollResult(dice.Sum(), true); 
    }
}