// =============================================================================
// File        : Models/Player.cs
// Project     : Snake and Ladder — Console Game
// Author      : Megh Godbole (Ganesh)
// Created     : March 18, 2026
// Description : Represents a single player in the game. Acts as a pure data
//               model — holds state only (name, position, turn count) and
//               exposes a computed property for win detection. No game logic
//               lives here; that responsibility belongs to GameEngine.
// =============================================================================

namespace Snake_and_Ladder.Models
{
    /// <summary>
    /// Represents a player in the game, tracking their name,
    /// current board position, and number of turns taken.
    /// </summary>
    public class Player
    {
        /// <summary>Display name of the player.</summary>
        public string Name { get; }

        /// <summary>
        /// Current position on the board (0 = start, 100 = win).
        /// Clamped to a maximum of 100 by the game engine.
        /// </summary>
        public int Position { get; set; }

        /// <summary>Total number of turns this player has taken.</summary>
        public int TurnCount { get; set; }

        /// <summary>
        /// Returns true when this player has reached or exceeded position 100.
        /// Evaluated after every move by the game engine.
        /// </summary>
        public bool HasWon => Position >= 100;

        /// <summary>
        /// Initializes a new player with the given display name.
        /// Position and TurnCount both start at zero.
        /// </summary>
        /// <param name="name">The player's display name.</param>
        public Player(string name)
        {
            Name      = name;
            Position  = 0;
            TurnCount = 0;
        }
    }
}
