// =============================================================================
// File        : Models/Dice.cs
// Project     : Snake and Ladder — Console Game
// Author      : Megh Godbole (Ganesh)
// Created     : March 18, 2026
// Description : Represents a standard six-sided dice. Encapsulates all
//               randomness behind a single shared Random instance to prevent
//               seed-collision issues that occur when new Random() is called
//               repeatedly in quick succession (a known issue pre-.NET 6).
// =============================================================================

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
        // Shared across all Dice instances — avoids repeated seed issues
        private static readonly Random _random = new();

        /// <summary>Minimum face value on the dice (inclusive).</summary>
        public const int MinValue = 1;

        /// <summary>Maximum face value on the dice (inclusive).</summary>
        public const int MaxValue = 6;

        /// <summary>
        /// Rolls the dice and returns a random value between 1 and 6.
        /// </summary>
        /// <returns>An integer between <see cref="MinValue"/> and <see cref="MaxValue"/>.</returns>
        public int Roll() => _random.Next(MinValue, MaxValue + 1);
    }
}
