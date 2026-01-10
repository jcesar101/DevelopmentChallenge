namespace Pig.Variations;

using Pig.Core;

/// <summary>
/// Implements the "Two-Dice Pig" variation. 
/// This version increases the stakes by adding a "Snake Eyes" penalty 
/// that can reset a player's entire game progress.
/// </summary>
public class TwoDicePig(int winningScore = 100) : IPigVariation
{
    /// <summary>
    /// Gets the display name for this variation.
    /// </summary>
    public string Name => "Two-Dice Pig";

    /// <summary>
    /// Gets the score required to win the game.
    /// </summary>
    public int WinningScore => winningScore;

    /// <summary>
    /// This variation is played with two dice per roll.
    /// </summary>
    public int DiceCount => 2;

    /// <summary>
    /// Processes a two-dice roll according to the following rules:
    /// <list type="bullet">
    /// <item>
    /// <term>Snake Eyes (Two 1s)</term>
    /// <description>The player loses their entire total score and the turn ends.</description>
    /// </item>
    /// <item>
    /// <term>Single 1</term>
    /// <description>The player scores 0 for the turn and the turn ends.</description>
    /// </item>
    /// <item>
    /// <term>No 1s</term>
    /// <description>The sum of both dice is added to the turn bank.</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="dice">The values of the two dice rolled.</param>
    /// <returns>A <see cref="RollResult"/> reflecting the outcome of the two-dice throw.</returns>
    public RollResult ProcessRoll(IEnumerable<int> dice)
    {
        var diceList = dice.ToList();
        int d1 = diceList[0];
        int d2 = diceList[1];

        // Rule: "Snake Eyes" - Double 1s wipes the player's total cumulative score.
        if (d1 == 1 && d2 == 1) 
            return new RollResult(0, IsTurnOver: true, LoseTotalScore: true);
        
        // Rule: A single 1 wipes only the current turn's bank.
        if (d1 == 1 || d2 == 1) 
            return new RollResult(0, IsTurnOver: true);

        // Standard success: Sum both dice and allow the player to roll again.
        return new RollResult(d1 + d2, IsTurnOver: false);
    }
}