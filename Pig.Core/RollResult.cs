namespace Pig.Core;

/// <summary>
/// Represents the outcome of a single roll action as evaluated by a game variation.
/// This acts as a data transfer object between the variation rules and the core game logic.
/// </summary>
/// <param name="Score">The points earned from the current roll (0 if the roll resulted in a bust).</param>
/// <param name="IsTurnOver">Indicates if the player's turn must end immediately (e.g., rolled a 1 or completed a Hog throw).</param>
/// <param name="LoseTotalScore">A critical failure flag. If <c>true</c>, the player's entire lifetime score is reset to 0 (used for "Snake Eyes").</param>
public record RollResult(int Score, bool IsTurnOver, bool LoseTotalScore = false);