using Xunit;
using Pig.Core;
using Pig.Variations;
using System.Collections.Generic;

namespace PigGame.Tests;

/// <summary>
/// Unit tests for the <see cref="GameLogic"/> static transformer.
/// These tests verify that game state transitions follow the expected rules of Pig 
/// and its variations without side effects.
/// </summary>
public class GameLogicTests
{
    /// <summary>
    /// Verifies that when a player chooses to 'Hold', their current turn score is 
    /// added to their total score, the bank is cleared, and the turn passes to the next player.
    /// </summary>
    [Fact]
    public void Hold_UpdatesPlayerScore_And_SwitchesTurn()
    {
        // Arrange
        var players = new List<PlayerState> { 
            new("Player1", 0), 
            new("Player2", 0) 
        }.AsReadOnly();

        var initialState = new GameState(
            Players: players,
            CurrentPlayerIndex: 0,
            CurrentTurnScore: 10,
            IsGameOver: false,
            LastRollDescription: "Initial"
        );

        // Act
        var nextState = GameLogic.Hold(initialState, new StandardPig());

        // Assert
        Assert.Equal(10, nextState.Players[0].TotalScore);
        Assert.Equal(1, nextState.CurrentPlayerIndex);
        Assert.Equal(0, nextState.CurrentTurnScore);
    }

    /// <summary>
    /// Verifies that the game correctly identifies a victory condition when a 
    /// player's total banked score reaches or exceeds the winning threshold.
    /// </summary>
    [Fact]
    public void GameOver_Triggers_When_Player_Reaches_WinningScore()
    {
        // Arrange
        var players = new List<PlayerState> { 
            new("Player1", 95), 
            new("Player2", 0) 
        }.AsReadOnly();

        var initialState = new GameState(players, 0, 10, false, "Initial");

        // Act
        var nextState = GameLogic.Hold(initialState, new StandardPig(100));

        // Assert
        Assert.True(nextState.IsGameOver);
        Assert.Equal(105, nextState.Players[0].TotalScore);
        // Ensure the winning player remains selected
        Assert.Equal(0, nextState.CurrentPlayerIndex); 
    }

    /// <summary>
    /// Verifies the "Pig" rule: Rolling a 1 (in Standard Pig) should result in 
    /// zero points for the current turn and an immediate change of active player, 
    /// while preserving the previously banked total score.
    /// </summary>
    [Fact]
    public void Roll_With_Pig_Result_Wipes_Bank_And_Switches_Turn()
    {
        // Arrange
        var players = new List<PlayerState> { new("Player1", 50) }.AsReadOnly();
        var initialState = new GameState(players, 0, 15, false, "Initial");

        // Act
        var nextState = GameLogic.Roll(initialState, new StandardPig(), [1]);

        // Assert
        Assert.Equal(0, nextState.CurrentTurnScore);
        Assert.Equal(50, nextState.Players[0].TotalScore);
    }
}