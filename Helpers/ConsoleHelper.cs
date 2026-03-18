using System;

namespace Snake_and_Ladder.Helpers
{
    /// <summary>
    /// Utility class for styled console output and user input handling.
    /// Centralizes all UI concerns so game logic stays clean.
    /// </summary>
    public static class ConsoleHelper
    {
        // ── Color constants ──────────────────────────────────────────────────
        private const ConsoleColor SnakeColor  = ConsoleColor.Red;
        private const ConsoleColor LadderColor = ConsoleColor.Green;
        private const ConsoleColor WinColor    = ConsoleColor.Yellow;
        private const ConsoleColor InfoColor   = ConsoleColor.Cyan;
        private const ConsoleColor DimColor    = ConsoleColor.DarkGray;

        /// <summary>Prints a styled section divider.</summary>
        public static void PrintDivider()
        {
            WriteColored("─────────────────────────────────────────", DimColor);
            Console.WriteLine();
        }

        /// <summary>Prints the game title banner.</summary>
        public static void PrintBanner()
        {
            Console.Clear();
            WriteColored("╔══════════════════════════════════════════╗\n", InfoColor);
            WriteColored("║        🐍  Snake and Ladder  🪜           ║\n", InfoColor);
            WriteColored("╚══════════════════════════════════════════╝\n", InfoColor);
            Console.WriteLine();
        }

        /// <summary>
        /// Prints feedback to the console based on the board event type.
        /// </summary>
        /// <param name="boardEvent">One of: "snake", "ladder", "overshoot", "win", "none".</param>
        /// <param name="oldPos">Position before the event.</param>
        /// <param name="newPos">Position after the event.</param>
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
                    WriteColored($"  ↩️  Overshot! Bounced back to {newPos}.\n", DimColor);
                    break;
            }
        }

        /// <summary>Prints the win message for a player.</summary>
        /// <param name="playerName">Name of the winning player.</param>
        /// <param name="turns">Number of turns it took to win.</param>
        public static void PrintWinMessage(string playerName, int turns)
        {
            Console.WriteLine();
            WriteColored($"  🎉 Congratulations {playerName}! You won in {turns} turns!\n", WinColor);
        }

        /// <summary>
        /// Prompts the user for a yes/no response and returns true if "y" was entered.
        /// Loops until valid input is received.
        /// </summary>
        /// <param name="prompt">The question to display.</param>
        public static bool AskYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y") return true;
                if (input == "n") return false;

                WriteColored("  ⚠️  Invalid input. Please enter 'y' or 'n'.\n", ConsoleColor.DarkYellow);
            }
        }

        /// <summary>
        /// Prompts the user for a player name, enforcing non-empty input.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        public static string AskPlayerName(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string name = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(name)) return name;

                WriteColored("  ⚠️  Name cannot be empty. Try again.\n", ConsoleColor.DarkYellow);
            }
        }

        /// <summary>
        /// Prompts for an integer within a given range, looping on invalid input.
        /// </summary>
        public static int AskInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;

                WriteColored($"  ⚠️  Please enter a number between {min} and {max}.\n", ConsoleColor.DarkYellow);
            }
        }

        /// <summary>Writes text to the console in a specified color, then resets.</summary>
        private static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}