using System;

namespace Snake_and_Ladder.Models
{
    /// <summary>
    /// Represents a standard six-sided dice.
    /// Uses a single shared <see cref="Random"/> instance to avoid
    /// seed-collision issues that occur when instantiating Random in tight loops.
    /// </summary>
    public class Dice
    {
        private static readonly Random _random = new();

        /// <summary>
        /// Minimum face value on the dice (inclusive).
        /// </summary>
        public const int MinValue = 1;

        /// <summary>
        /// Maximum face value on the dice (inclusive).
        /// </summary>
        public const int MaxValue = 6;

        /// <summary>
        /// Rolls the dice and returns a random value between 1 and 6.
        /// </summary>
        /// <returns>An integer between 1 and 6.</returns>
        public int Roll() => _random.Next(MinValue, MaxValue + 1);
    }
}