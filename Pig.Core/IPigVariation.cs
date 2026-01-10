namespace Pig.Core;

/// <summary>
/// Defines the contract for a Pig game variation. 
/// Implementations of this interface encapsulate specific scoring rules, 
/// dice counts, and turn-ending conditions.
/// </summary>
public interface IPigVariation
{
    /// <summary>
    /// The display name of the variation (e.g., "Big Pig" or "Hog").
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The total score required for a player to win the game under this variation.
    /// </summary>
    int WinningScore { get; }

    /// <summary>
    /// The number of dice used for a single roll action in this variation.
    /// </summary>
    int DiceCount { get; }

    /// <summary>
    /// Evaluates the outcome of a dice roll based on the specific rules of the variation.
    /// </summary>
    /// <param name="dice">The collection of dice values rolled.</param>
    /// <returns>
    /// A <see cref="RollResult"/> containing the score for the roll 
    /// and flags indicating if the turn is over or if the total score is lost.
    /// </returns>
    RollResult ProcessRoll(IEnumerable<int> dice);
}