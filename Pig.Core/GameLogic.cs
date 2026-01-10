namespace Pig.Core;

/// <summary>
/// Contains the core state-transition logic for the Pig dice game.
/// This class is designed using functional programming principles, where each method 
/// acts as a pure state transformer.
/// </summary>
public static class GameLogic
{
    /// <summary>
    /// Processes a dice roll and transforms the current game state into a new state snapshot.
    /// Handles turn-ending logic (busts), score accumulation, and single-throw banking (Hog style).
    /// </summary>
    /// <param name="state">The current immutable snapshot of the game.</param>
    /// <param name="variation">The rule set determining how the roll should be scored.</param>
    /// <param name="dice">The values of the dice rolled in this action.</param>
    /// <returns>A new <see cref="GameState"/> record representing the game after the roll.</returns>
    public static GameState Roll(GameState state, IPigVariation variation, IEnumerable<int> dice)
    {
        if (state.IsGameOver) return state;

        var result = variation.ProcessRoll(dice);
        string diceDisplay = string.Join(", ", dice);

        if (result.IsTurnOver)
        {
            // Calculation for "Auto-Banking": Only keep the bank if the roll itself wasn't a bust (score > 0).
            int pointsToBank = (result.Score == 0) ? 0 : state.CurrentTurnScore + result.Score;

            // Project new player list with updated scores where applicable
            var updatedPlayers = state.Players.Select((p, i) => 
                i == state.CurrentPlayerIndex 
                    ? p with { TotalScore = (result.LoseTotalScore ? 0 : p.TotalScore + pointsToBank) } 
                    : p).ToList();
            
            bool win = updatedPlayers[state.CurrentPlayerIndex].TotalScore >= variation.WinningScore;

            return state with
            {
                Players = updatedPlayers.AsReadOnly(),
                CurrentTurnScore = 0,
                CurrentPlayerIndex = win ? state.CurrentPlayerIndex : (state.CurrentPlayerIndex + 1) % state.Players.Count,
                IsGameOver = win,
                LastRollDescription = result.Score == 0 
                    ? $"Rolled {diceDisplay}. BUST! Lost banked points." 
                    : $"Rolled {diceDisplay}. Turn concluded."
            };
        }

        // Standard progression for mid-turn rolls
        return state with
        {
            CurrentTurnScore = state.CurrentTurnScore + result.Score,
            LastRollDescription = $"Rolled {diceDisplay}. +{result.Score} to bank."
        };
    }

    /// <summary>
    /// Finalizes the current player's turn by banking their accumulated turn score into their total score.
    /// </summary>
    /// <param name="state">The current immutable snapshot of the game.</param>
    /// <param name="variation">The rule set used to verify the win condition.</param>
    /// <returns>A new <see cref="GameState"/> record with updated total scores and a new active player.</returns>
    public static GameState Hold(GameState state, IPigVariation variation)
    {
        // Guard against banking an empty turn or an already finished game
        if (state.IsGameOver || state.CurrentTurnScore == 0) return state;

        int newScore = state.Players[state.CurrentPlayerIndex].TotalScore + state.CurrentTurnScore;
        
        var updatedPlayers = state.Players.Select((p, i) => 
            i == state.CurrentPlayerIndex ? p with { TotalScore = newScore } : p).ToList();

        bool win = newScore >= variation.WinningScore;

        return state with
        {
            Players = updatedPlayers.AsReadOnly(),
            CurrentTurnScore = 0,
            IsGameOver = win,
            // Only advance the player index if the game isn't over
            CurrentPlayerIndex = win ? state.CurrentPlayerIndex : (state.CurrentPlayerIndex + 1) % updatedPlayers.Count,
            LastRollDescription = win ? "Wins the game!" : "Held and banked points."
        };
    }
}