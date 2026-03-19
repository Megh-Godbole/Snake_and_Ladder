// =============================================================================
// File        : Engine/GameEngine.cs
// Project     : Snake and Ladder — Console Game
// Author      : Megh Godbole (Ganesh)
// Created     : March 18, 2026
// Description : Core game loop. Orchestrates turn order, dice rolling,
//               board position evaluation, and win detection for all players.
//               Separated from Program.cs to keep the entry point thin and
//               to make this class independently unit-testable without
//               requiring a running console session.
// =============================================================================

using System;
using System.Collections.Generic;
using Snake_and_Ladder.Helpers;
using Snake_and_Ladder.Models;

namespace Snake_and_Ladder.Engine
{
    /// <summary>
    /// Core game loop. Orchestrates turn order, dice rolling,
    /// board evaluation, and win detection for all players.
    /// Separating this from Program.cs keeps the entry point thin
    /// and this class independently testable.
    /// </summary>
    public class GameEngine
    {
        // ── Dependencies ─────────────────────────────────────────────────────
        private readonly Board        _board;
        private readonly Dice         _dice;
        private readonly List<Player> _players;

        /// <summary>
        /// Initializes the game engine with the given list of players.
        /// Internally creates its own <see cref="Board"/> and <see cref="Dice"/> instances.
        /// </summary>
        /// <param name="players">List of players participating in the game.</param>
        public GameEngine(List<Player> players)
        {
            _board   = new Board();
            _dice    = new Dice();
            _players = players;
        }

        /// <summary>
        /// Starts and runs the full game loop until a player wins or
        /// any player chooses to quit on their turn.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("  Game started! Good luck everyone.\n");

            // Cycle through all players repeatedly until the game ends
            while (true)
            {
                foreach (Player player in _players)
                {
                    ConsoleHelper.PrintDivider();
                    Console.WriteLine($"  🎲 {player.Name}'s turn  |  Position: {player.Position}");

                    // Give the player the option to quit gracefully
                    if (!ConsoleHelper.AskYesNo("  Roll the dice?"))
                    {
                        Console.WriteLine($"\n  {player.Name} chose to quit. Bye!");
                        return;
                    }

                    TakeTurn(player);

                    // Check for win immediately after the turn ends
                    if (player.HasWon)
                    {
                        ConsoleHelper.PrintWinMessage(player.Name, player.TurnCount);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Executes a single turn for the given player.
        /// Handles the standard roll, applies board effects, then handles
        /// the bonus roll if a 6 was rolled — applying board effects again.
        /// </summary>
        /// <param name="player">The player whose turn it is.</param>
        private void TakeTurn(Player player)
        {
            player.TurnCount++;

            // ── Primary roll ─────────────────────────────────────────────────
            int roll = _dice.Roll();
            Console.WriteLine($"\n  Rolled: {roll}");

            player.Position = EvaluateMove(player.Position, roll);

            // Stop early if the player wins on the primary roll
            if (player.HasWon) return;

            // ── Bonus roll on 6 ──────────────────────────────────────────────
            // Standard rule: rolling a 6 earns a bonus roll.
            // Both rolls count toward movement — evaluated separately so
            // snakes and ladders are checked after each individual roll.
            if (roll == Dice.MaxValue)
            {
                Console.WriteLine("  🎲 Rolled a 6! Bonus roll incoming...");

                int bonusRoll = _dice.Roll();
                Console.WriteLine($"  Bonus Roll: {bonusRoll}");

                player.Position = EvaluateMove(player.Position, bonusRoll);
            }

            Console.WriteLine($"\n  📍 {player.Name} is now on position {player.Position}.");
        }

        /// <summary>
        /// Applies a dice roll to the player's current position,
        /// evaluates the result on the board, and prints the outcome.
        /// </summary>
        /// <param name="currentPosition">The player's position before this roll.</param>
        /// <param name="roll">The dice value to apply.</param>
        /// <returns>The resolved final position after board effects.</returns>
        private int EvaluateMove(int currentPosition, int roll)
        {
            int rawPosition   = currentPosition + roll;
            int finalPosition = _board.Evaluate(rawPosition, out string boardEvent);

            // Delegate all UI feedback to ConsoleHelper — keeps this method clean
            ConsoleHelper.PrintBoardEvent(boardEvent, rawPosition, finalPosition);

            return finalPosition;
        }
    }
}
