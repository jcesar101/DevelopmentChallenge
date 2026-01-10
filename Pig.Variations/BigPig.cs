namespace Pig.Variations;

using Pig.Core;

/// <summary>
/// Implements the "Big Pig" variation of the game, played with two dice.
/// This version rewards doubles and provides a significant bonus for rolling two ones.
/// </summary>
public class BigPig(int winningScore = 100) : IPigVariation
{
    /// <summary>
    /// Gets the display name for the variation.
    /// </summary>
    public string Name => "Big Pig";

    /// <summary>
    /// Gets the score required to win the game.
    /// </summary>
    public int WinningScore => winningScore;

    /// <summary>
    /// Big Pig is strictly played with two dice.
    /// </summary>
    public int DiceCount => 2;

    /// <summary>
    /// Evaluates a two-dice roll according to Big Pig rules:
    /// <list type="bullet">
    /// <item><description>Double 1s: 25 points, turn continues.</description></item>
    /// <item><description>Single 1: 0 points, turn ends.</description></item>
    /// <item><description>Other Doubles: Sum of dice multiplied by 2, turn continues.</description></item>
    /// <item><description>Standard Roll: Sum of dice, turn continues.</description></item>
    /// </list>
    /// </summary>
    /// <param name="dice">The values of the two dice rolled.</param>
    /// <returns>A <see cref="RollResult"/> reflecting the Big Pig scoring rules.</returns>
    public RollResult ProcessRoll(IEnumerable<int> dice)
    {
        var d = dice.ToList();
        
        // Rule: Double 1s are a high-value bonus in this variant
        if (d[0] == 1 && d[1] == 1) 
            return new RollResult(25, false);

        // Rule: Any single 1 results in a bust (loss of turn bank)
        if (d[0] == 1 || d[1] == 1) 
            return new RollResult(0, true);

        // Rule: Doubles (2-2 through 6-6) award double the sum of the dice
        if (d[0] == d[1]) 
            return new RollResult((d[0] + d[1]) * 2, false);
        
        // Standard non-matching roll
        return new RollResult(d[0] + d[1], false);
    }
}