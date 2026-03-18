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
        /// <summary>
        /// The winning position on the board.
        /// </summary>
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
        /// Evaluates a position and applies any snake or ladder effect.
        /// If the position exceeds <see cref="WinPosition"/>, it is clamped.
        /// </summary>
        /// <param name="position">The raw position after a dice roll.</param>
        /// <param name="boardEvent">
        /// Outputs a description of what happened:
        /// "snake", "ladder", "win", or "none".
        /// </param>
        /// <returns>The final resolved position.</returns>
        public int Evaluate(int position, out string boardEvent)
        {
            // Overshoot — player must land exactly on 100 or stay put
            if (position > WinPosition)
            {
                boardEvent = "overshoot";
                return position - (position - WinPosition); // bounce back
            }

            if (Snakes.TryGetValue(position, out int snakeTail))
            {
                boardEvent = "snake";
                return snakeTail;
            }

            if (Ladders.TryGetValue(position, out int ladderTop))
            {
                boardEvent = "ladder";
                return ladderTop;
            }

            boardEvent = position == WinPosition ? "win" : "none";
            return position;
        }
    }
}