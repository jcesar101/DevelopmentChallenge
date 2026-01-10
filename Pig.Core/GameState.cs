using System.Collections.Generic;

namespace Pig.Core;

/// <summary>
/// Represents an immutable snapshot of the Pig game state at a specific point in time.
/// </summary>
/// <remarks>
/// This record type ensures thread safety and state integrity by preventing 
/// modification after creation. Any change to the game state results in a 
/// new instance of this record via the 'with' expression.
/// </remarks>
/// <param name="Players">An immutable list of all participating players and their current total scores.</param>
/// <param name="CurrentPlayerIndex">The index of the player whose turn it currently is.</param>
/// <param name="CurrentTurnScore">The points accumulated in the current turn that have not yet been banked.</param>
/// <param name="IsGameOver">A flag indicating whether a player has reached the winning score.</param>
/// <param name="LastRollDescription">A text-based summary of the most recent action for display in the UI.</param>
public record GameState(
    IReadOnlyList<PlayerState> Players,
    int CurrentPlayerIndex,
    int CurrentTurnScore,
    bool IsGameOver,
    string LastRollDescription
);