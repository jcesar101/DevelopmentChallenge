using System;

namespace Pig.Console;

/// <summary>
/// Provides a centralized utility for capturing and validating user input from the command line.
/// </summary>
public static class InputHandler
{
    /// <summary>
    /// Displays a prompt and captures a string response from the user.
    /// </summary>
    /// <param name="prompt">The message displayed to the user explaining what input is expected.</param>
    /// <returns>The string entered by the user, or an empty string if the input was null.</returns>
    public static string GetString(string prompt)
    {
        System.Console.Write($"{prompt}: ");
        return System.Console.ReadLine() ?? string.Empty;
    }

    /// <summary>
    /// Prompts the user for a numeric value and ensures it falls within a specific range.
    /// This method will loop indefinitely until a valid integer within the bounds is provided.
    /// </summary>
    /// <param name="prompt">The message displayed to the user.</param>
    /// <param name="min">The inclusive minimum allowed value.</param>
    /// <param name="max">The inclusive maximum allowed value.</param>
    /// <returns>A validated integer between <paramref name="min"/> and <paramref name="max"/>.</returns>
    public static int GetInt(string prompt, int min, int max)
    {
        while (true)
        {
            System.Console.Write($"{prompt} ({min}-{max}): ");
            if (int.TryParse(System.Console.ReadLine(), out int result) && result >= min && result <= max)
                return result;
            
            System.Console.WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
        }
    }

    /// <summary>
    /// Captures a simple yes/no response based on a single key press.
    /// </summary>
    /// <param name="prompt">The question to display to the user.</param>
    /// <returns><c>true</c> if the 'Y' key was pressed; otherwise, <c>false</c>.</returns>
    public static bool GetYesNo(string prompt)
    {
        System.Console.Write($"{prompt} (y/n): ");
        var key = System.Console.ReadKey(false).Key;
        System.Console.WriteLine();
        return key == ConsoleKey.Y;
    }

    /// <summary>
    /// Halts execution and waits for the user to press any key. 
    /// Useful for preventing the console from closing or clearing before the user can read the output.
    /// </summary>
    public static void Wait()
    {
        System.Console.WriteLine("\nPress any key to continue...");
        System.Console.ReadKey(true);
    }
}