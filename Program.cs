using System;
using System.Collections.Generic;
using Snake_and_Ladder.Engine;
using Snake_and_Ladder.Helpers;
using Snake_and_Ladder.Models;

namespace Snake_and_Ladder
{
    /// <summary>
    /// Entry point. Keeps Program.cs thin — only responsible for
    /// bootstrapping the game (collecting setup input, wiring dependencies).
    /// All game logic lives in <see cref="GameEngine"/>.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.PrintBanner();

            // ── Collect number of players ────────────────────────────────────
            int playerCount = ConsoleHelper.AskInt("  How many players? (1–4): ", 1, 4);

            // ── Collect player names ─────────────────────────────────────────
            List<Player> players = new();
            for (int i = 1; i <= playerCount; i++)
            {
                string name = ConsoleHelper.AskPlayerName($"  Enter name for Player {i}: ");
                players.Add(new Player(name));
            }

            Console.WriteLine();

            // ── Start the game ───────────────────────────────────────────────
            GameEngine game = new(players);
            game.Run();

            Console.WriteLine("\n  Thanks for playing! Bye 👋");
        }
    }
}