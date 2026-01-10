namespace Pig.Core;

/// <summary>
/// Represents an immutable snapshot of a player's standing within the game.
/// </summary>
/// <remarks>
/// Since this is a record type, it provides built-in value-based equality. 
/// In the functional game loop, player scores are updated by creating a 
/// new instance of this record with an updated <see cref="TotalScore"/>.
/// </remarks>
/// <param name="Name">The display name or identifier for the player.</param>
/// <param name="TotalScore">The cumulative banked score earned by the player across all completed turns.</param>
public record PlayerState(string Name, int TotalScore);