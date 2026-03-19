// =============================================================================
// File        : Models/Board.cs
// Project     : Snake and Ladder — Console Game
// Author      : Megh Godbole (Ganesh)
// Created     : March 18, 2026
// Description : Represents the 100-square Snake and Ladder game board.
//               Owns all snake and ladder position mappings and exposes
//               a single Evaluate() method that resolves any raw position
//               (after a dice roll) into a final board position, applying
//               snake slides, ladder climbs, or overshoot bouncing as needed.
// =============================================================================

using System.Collections.Generic;

namespace Snake_and_Ladder.Models
{
    /// <summary>
    /// Represents the Snake and Ladder game board.
    /// Holds all snake and ladder mappings and exposes methods
    /// to evaluate a position after a move.
    /// </summary>
    public class Board
    {
        /// <summary>The winning position on the board.</summary>
        public const int WinPosition = 100;

        /// <summary>
        /// Maps snake head positions to their tail positions.
        /// Landing on a key slides the player down to the value.
        /// </summary>
        private static readonly Dictionary<int, int> Snakes = new()
        {
            { 17, 7  }, { 54, 34 }, { 62, 19 }, { 64, 60 },
            { 87, 24 }, { 93, 73 }, { 95, 75 }, { 99, 78 }
        };

        /// <summary>
        /// Maps ladder bottom positions to their top positions.
        /// Landing on a key climbs the player up to the value.
        /// </summary>
        private static readonly Dictionary<int, int> Ladders = new()
        {
            { 4,  14 }, { 9,  31 }, { 20, 38 }, { 28, 84 },
            { 40, 59 }, { 51, 67 }, { 63, 81 }, { 71, 91 }
        };

        /// <summary>
        /// Evaluates a raw position after a dice roll and applies any board effects.
        /// Handles overshoot (bounce back), snake slides, and ladder climbs.
        /// </summary>
        /// <param name="position">The raw position after adding the dice roll.</param>
        /// <param name="boardEvent">
        /// Outputs a string describing what happened:
        /// <list type="bullet">
        ///   <item><c>"snake"</c> — player slid down a snake</item>
        ///   <item><c>"ladder"</c> — player climbed a ladder</item>
        ///   <item><c>"overshoot"</c> — player exceeded 100 and bounced back</item>
        ///   <item><c>"win"</c> — player landed exactly on 100</item>
        ///   <item><c>"none"</c> — normal move with no special event</item>
        /// </list>
        /// </param>
        /// <returns>The final resolved board position.</returns>
        public int Evaluate(int position, out string boardEvent)
        {
            // Overshoot rule — bounce back by the overflow amount
            // e.g. position 98 + roll 5 = 103 → bounces to 97
            if (position > WinPosition)
            {
                boardEvent = "overshoot";
                return WinPosition - (position - WinPosition);
            }

            // Check for snake at this position (slides player down)
            if (Snakes.TryGetValue(position, out int snakeTail))
            {
                boardEvent = "snake";
                return snakeTail;
            }

            // Check for ladder at this position (climbs player up)
            if (Ladders.TryGetValue(position, out int ladderTop))
            {
                boardEvent = "ladder";
                return ladderTop;
            }

            // No special event — return position as-is
            boardEvent = position == WinPosition ? "win" : "none";
            return position;
        }
    }
}
