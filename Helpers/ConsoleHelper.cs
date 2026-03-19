// =============================================================================
// File        : Helpers/ConsoleHelper.cs
// Project     : Snake and Ladder — Console Game
// Author      : Megh Godbole (Ganesh)
// Created     : March 18, 2026
// Description : Static utility class that centralizes all console UI concerns —
//               styled output (color-coded messages, banners, dividers) and
//               validated user input (yes/no prompts, player names, integers).
//               By isolating all console interaction here, the Models and Engine
//               classes remain free of UI dependencies and are easier to test.
// =============================================================================

using System;

namespace Snake_and_Ladder.Helpers
{
    /// <summary>
    /// Static utility class for styled console output and validated user input.
    /// Centralizes all UI concerns so that game logic classes stay clean and
    /// independently testable without a live console session.
    /// </summary>
    public static class ConsoleHelper
    {
        // ── Color constants ──────────────────────────────────────────────────
        // Defined once here so color choices are consistent and easy to change
        private const ConsoleColor SnakeColor  = ConsoleColor.Red;
        private const ConsoleColor LadderColor = ConsoleColor.Green;
        private const ConsoleColor WinColor    = ConsoleColor.Yellow;
        private const ConsoleColor InfoColor   = ConsoleColor.Cyan;
        private const ConsoleColor WarnColor   = ConsoleColor.DarkYellow;
        private const ConsoleColor DimColor    = ConsoleColor.DarkGray;

        // ── Output methods ───────────────────────────────────────────────────

        /// <summary>Prints a styled horizontal divider between turns.</summary>
        public static void PrintDivider()
        {
            WriteColored("─────────────────────────────────────────", DimColor);
            Console.WriteLine();
        }

        /// <summary>
        /// Clears the console and prints the game title banner.
        /// Called once at application startup.
        /// </summary>
        public static void PrintBanner()
        {
            Console.Clear();
            WriteColored("╔══════════════════════════════════════════╗\n", InfoColor);
            WriteColored("║        🐍  Snake and Ladder  🪜           ║\n", InfoColor);
            WriteColored("╚══════════════════════════════════════════╝\n", InfoColor);
            Console.WriteLine();
        }

        /// <summary>
        /// Prints a color-coded message based on the board event that occurred.
        /// Only prints for meaningful events — silent for "none" and "win"
        /// (the win message is handled separately by <see cref="PrintWinMessage"/>).
        /// </summary>
        /// <param name="boardEvent">
        /// One of: <c>"snake"</c>, <c>"ladder"</c>, <c>"overshoot"</c>, <c>"win"</c>, <c>"none"</c>.
        /// </param>
        /// <param name="oldPos">The raw position before the board event was applied.</param>
        /// <param name="newPos">The final position after the board event was applied.</param>
        public static void PrintBoardEvent(string boardEvent, int oldPos, int newPos)
        {
            switch (boardEvent)
            {
                case "snake":
                    WriteColored($"  🐍 Oops! Snake at {oldPos} → slides down to {newPos}!\n", SnakeColor);
                    break;

                case "ladder":
                    WriteColored($"  🪜 Nice! Ladder at {oldPos} → climbs up to {newPos}!\n", LadderColor);
                    break;

                case "overshoot":
                    WriteColored($"  ↩️  Overshot 100! Bounced back to {newPos}.\n", DimColor);
                    break;

                // "win" and "none" produce no extra output here
                default:
                    break;
            }
        }

        /// <summary>
        /// Prints the win congratulations message with the player's name and turn count.
        /// </summary>
        /// <param name="playerName">Name of the winning player.</param>
        /// <param name="turns">Total number of turns it took to win.</param>
        public static void PrintWinMessage(string playerName, int turns)
        {
            Console.WriteLine();
            WriteColored($"  🎉 Congratulations {playerName}! You won in {turns} turns!\n", WinColor);
        }

        // ── Input methods ────────────────────────────────────────────────────

        /// <summary>
        /// Displays a yes/no prompt and loops until the user enters a valid response.
        /// </summary>
        /// <param name="prompt">The question to display to the user.</param>
        /// <returns><c>true</c> if the user entered "y"; <c>false</c> if they entered "n".</returns>
        public static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y") return true;
                if (input == "n") return false;

                // Any other input is invalid — ask again
                WriteColored("  ⚠️  Invalid input. Please enter 'y' or 'n'.\n", WarnColor);
            }
        }

        /// <summary>
        /// Prompts the user for a non-empty player name, looping on blank input.
        /// </summary>
        /// <param name="prompt">The prompt to display before the input field.</param>
        /// <returns>A trimmed, non-empty string entered by the user.</returns>
        public static string AskPlayerName(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string name = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(name)) return name;

                WriteColored("  ⚠️  Name cannot be empty. Please try again.\n", WarnColor);
            }
        }

        /// <summary>
        /// Prompts the user for an integer within a specified inclusive range,
        /// looping until a valid value is entered.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <param name="min">Minimum acceptable value (inclusive).</param>
        /// <param name="max">Maximum acceptable value (inclusive).</param>
        /// <returns>A valid integer between <paramref name="min"/> and <paramref name="max"/>.</returns>
        public static int AskInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;

                WriteColored($"  ⚠️  Please enter a number between {min} and {max}.\n", WarnColor);
            }
        }

        // ── Private helpers ──────────────────────────────────────────────────

        /// <summary>
        /// Writes text to the console in a specified color, then resets to default.
        /// Always resets even if an exception is thrown, preventing color bleed.
        /// </summary>
        /// <param name="text">The text to display.</param>
        /// <param name="color">The <see cref="ConsoleColor"/> to use.</param>
        private static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
