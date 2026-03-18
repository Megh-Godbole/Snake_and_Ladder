namespace Snake_and_Ladder.Models
{
    /// <summary>
    /// Represents a player in the game, tracking their name,
    /// current board position, and number of turns taken.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Display name of the player.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Current position on the board (0 = start, 100 = win).
        /// Clamped to a maximum of 100 by the game engine.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Total number of turns this player has taken.
        /// </summary>
        public int TurnCount { get; set; }

        /// <summary>
        /// Indicates whether this player has reached or exceeded position 100.
        /// </summary>
        public bool HasWon => Position >= 100;

        /// <summary>
        /// The player's display name.
        /// </summary>
        /// <param name="name">Player Name in String.</param>
        public Player(string name)
        {
            Name = name;
            Position = 0;
            TurnCount = 0;
        }
    }
}