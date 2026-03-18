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
        private readonly Board _board;
        private readonly Dice  _dice;
        private readonly List<Player> _players;

        /// <param name="players">List of players participating in the game.</param>
        public GameEngine(List<Player> players)
        {
            _board   = new Board();
            _dice    = new Dice();
            _players = players;
        }

        /// <summary>
        /// Starts and runs the game until a player wins or all players quit.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("  Game started! Good luck everyone.\n");

            // Cycle through players in order, repeating until someone wins
            while (true)
            {
                foreach (Player player in _players)
                {
                    ConsoleHelper.PrintDivider();
                    Console.WriteLine($"  🎲 {player.Name}'s turn  |  Position: {player.Position}");

                    if (!ConsoleHelper.AskYesNo("  Roll the dice?"))
                    {
                        Console.WriteLine($"\n  {player.Name} chose to quit. Bye!");
                        return;
                    }

                    TakeTurn(player);

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
        /// Handles the roll-again-on-6 rule and board evaluation after every roll.
        /// </summary>
        /// <param name="player">The player whose turn it is.</param>
        private void TakeTurn(Player player)
        {
            player.TurnCount++;

            int roll = _dice.Roll();
            Console.WriteLine($"\n  Rolled: {roll}");

            // Move and evaluate — stop early if the player wins mid-turn
            player.Position = EvaluateMove(player.Position, roll);
            if (player.HasWon) return;

            // Standard Snake & Ladder rule: rolling a 6 earns a bonus roll
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
        /// Applies a dice roll to a position and resolves any board event (snake/ladder/overshoot).
        /// </summary>
        /// <param name="currentPosition">The player's position before the roll.</param>
        /// <param name="roll">The dice value rolled.</param>
        /// <returns>The resolved final position.</returns>
        private int EvaluateMove(int currentPosition, int roll)
        {
            int rawPosition  = currentPosition + roll;
            int finalPosition = _board.Evaluate(rawPosition, out string boardEvent);

            ConsoleHelper.PrintBoardEvent(boardEvent, rawPosition, finalPosition);

            return finalPosition;
        }
    }
}