namespace Pig.Tests;

/// <summary>
/// Contains regression tests for the various Pig game strategies.
/// Validates that each implementation of <see cref="IPigVariation"/> correctly 
/// calculates scores and turn-state flags according to its specific rule set.
/// </summary>
public class VariationTests
{
    /// <summary>
    /// Validates the Standard Pig rule: Rolling a single '1' results in zero points 
    /// and ends the player's turn.
    /// </summary>
    [Fact]
    public void StandardPig_RollingOne_EndsTurn()
    {
        var variant = new StandardPig();
        var result = variant.ProcessRoll([1]);
        
        Assert.True(result.IsTurnOver);
        Assert.Equal(0, result.Score);
    }

    /// <summary>
    /// Validates the Two-Dice Pig "Snake Eyes" rule: Rolling two '1's results in 
    /// the loss of the player's entire accumulated game score.
    /// </summary>
    [Fact]
    public void TwoDicePig_DoubleOnes_WipesTotalScore()
    {
        var variant = new TwoDicePig();
        var result = variant.ProcessRoll([1, 1]);
        
        Assert.True(result.IsTurnOver);
        Assert.True(result.LoseTotalScore);
    }

    /// <summary>
    /// Validates the Big Pig bonus: Rolling doubles (excluding ones) should 
    /// reward twice the sum of the dice and allow the turn to continue.
    /// </summary>
    [Fact]
    public void BigPig_DoubleSixes_ScoresDoubleValue()
    {
        var variant = new BigPig();
        var result = variant.ProcessRoll([6, 6]);
        
        Assert.Equal(24, result.Score); // (6+6) * 2
        Assert.False(result.IsTurnOver);
    }

    /// <summary>
    /// Validates the Big Pig special case: Double ones are worth a fixed 
    /// 25 points rather than wiping the score.
    /// </summary>
    [Fact]
    public void BigPig_DoubleOnes_ScoresTwentyFive()
    {
        var variant = new BigPig();
        var result = variant.ProcessRoll([1, 1]);
        
        Assert.Equal(25, result.Score);
        Assert.False(result.IsTurnOver);
    }

    /// <summary>
    /// Validates the Hog rule: If any die in a multi-die throw is a '1', 
    /// the entire throw scores zero and the turn ends.
    /// </summary>
    [Fact]
    public void Hog_AnyOneInBatch_ReturnsZero()
    {
        var variant = new Hog(5); 
        var result = variant.ProcessRoll([2, 4, 1, 5, 6]);
        
        Assert.True(result.IsTurnOver);
        Assert.Equal(0, result.Score);
    }

    /// <summary>
    /// Validates the Hog Single-Throw mechanic: A successful throw should 
    /// automatically bank the points and advance the turn without requiring a separate 'Hold' action.
    /// </summary>
    [Fact]
    public void Hog_SuccessfulRoll_AddsPointsAndSwitchesPlayer()
    {
        // Arrange
        var players = new List<PlayerState> { new("Player1", 10), new("Player2", 0) }.AsReadOnly();
        var state = new GameState(players, 0, 0, false, "Initial");
        var hog = new Hog(4, 100);

        // Act
        var nextState = GameLogic.Roll(state, hog, [2, 3, 4, 5]);

        // Assert
        Assert.Equal(24, nextState.Players[0].TotalScore); // 10 + 14
        Assert.Equal(1, nextState.CurrentPlayerIndex);     // Auto-switched
        Assert.Equal(0, nextState.CurrentTurnScore);       
    }
}