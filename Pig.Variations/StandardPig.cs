namespace Pig.Variations;

using Pig.Core;

/// <summary>
/// Implements the classic "Standard Pig" rules using a single six-sided die.
/// This variation serves as the foundation for the "push-your-luck" mechanic.
/// </summary>
public class StandardPig(int winningScore = 100) : IPigVariation
{
    /// <summary>
    /// Gets the display name for this variation.
    /// </summary>
    public string Name => "Standard Pig (Single Die)";

    /// <summary>
    /// Gets the score threshold required to win the game.
    /// </summary>
    public int WinningScore => winningScore;

    /// <summary>
    /// Standard Pig is traditionally played with exactly one die.
    /// </summary>
    public int DiceCount => 1;

    /// <summary>
    /// Processes a single die roll according to the classic Pig rules:
    /// <list type="bullet">
    /// <item><description>Rolling a 1: The player scores 0 for the turn and the turn ends (Bust).</description></item>
    /// <item><description>Rolling 2-6: The face value is added to the turn bank, and the player may choose to roll again.</description></item>
    /// </list>
    /// </summary>
    /// <param name="dice">The collection containing the single die result.</param>
    /// <returns>A <see cref="RollResult"/> indicating the score and whether the turn is over.</returns>
    public RollResult ProcessRoll(IEnumerable<int> dice)
    {
        int roll = dice.First();
        
        // Rule: If a 1 is rolled, the turn score is lost and the turn ends.
        if (roll == 1) 
            return new RollResult(0, true); 
        
        // Rule: Face values 2 through 6 are added to the bank; the player continues their turn.
        return new RollResult(roll, false); 
    }
}