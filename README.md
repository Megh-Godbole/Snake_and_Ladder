# 🐍 Snake and Ladder — Console Game (C#)

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)
![Language](https://img.shields.io/badge/Language-C%23-239120?style=flat&logo=csharp)
![Platform](https://img.shields.io/badge/Platform-Cross--Platform-blue?style=flat)
![License](https://img.shields.io/badge/License-MIT-green?style=flat)

A fully playable, multi-player **Snake and Ladder** console game built in C# (.NET 9), following SOLID principles, clean architecture, and professional coding practices.

---

## 📋 Table of Contents

- [Features](#-features)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [How to Play](#-how-to-play)
- [Game Rules](#-game-rules)
- [Board Layout](#-board-layout)
- [Architecture](#-architecture)
- [Author](#-author)

---

## ✨ Features

- 🎮 **1–4 Player** support with custom player names
- 🎲 **Roll-again on 6** rule fully implemented
- 🐍 **8 Snakes** and **8 Ladders** on a classic 100-square board
- ↩️ **Overshoot protection** — bounces back if you exceed square 100
- ✅ **Input validation** — handles invalid input gracefully throughout
- 🎨 **Color-coded console output** — snakes in red, ladders in green, wins in gold
- 📊 **Turn counter** — tracks how many turns each player took to win
- 🧱 **Clean architecture** — Models, Engine, and Helpers separated by responsibility

---

## 📁 Project Structure

```
Snake_and_Ladder/
│
├── Models/
│   ├── Dice.cs           # Six-sided dice with shared Random instance
│   ├── Player.cs         # Player state (name, position, turn count)
│   └── Board.cs          # Board layout, snake/ladder maps, position evaluator
│
├── Engine/
│   └── GameEngine.cs     # Core game loop, turn management, win detection
│
├── Helpers/
│   └── ConsoleHelper.cs  # All console UI: colors, prompts, input validation
│
├── Program.cs            # Entry point — bootstraps players and starts engine
└── Snake_and_Ladder.csproj
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9) or later

### Installation

```bash
# Clone the repository
git clone https://github.com/Megh-Godbole/Snake_and_Ladder.git

# Navigate to the project folder
cd Snake_and_Ladder

# Build the project
dotnet build

# Run the game
dotnet run
```

---

## 🎮 How to Play

1. Launch the game with `dotnet run`
2. Enter the number of players (1–4)
3. Enter each player's name
4. On your turn, press `y` to roll the dice
5. The game automatically handles snakes, ladders, and bonus rolls for a 6
6. First player to land **exactly on square 100** wins!
7. Press `n` on any turn to quit the game

---

## 📏 Game Rules

| Rule | Detail |
|------|--------|
| Starting position | All players start at square **0** |
| Winning condition | Land exactly on square **100** |
| Overshoot | If your roll exceeds 100, you bounce back by the overflow amount |
| Roll a 6 | You earn a **bonus roll** — the 6 still counts toward your move |
| Snake | Landing on a snake's head slides you **down** to its tail |
| Ladder | Landing on a ladder's base climbs you **up** to its top |
| Turn order | Players take turns in the order they were added |

---

## 🗺️ Board Layout

### 🐍 Snakes (Head → Tail)

| Head | Tail |
|------|------|
| 17   | 7    |
| 54   | 34   |
| 62   | 19   |
| 64   | 60   |
| 87   | 24   |
| 93   | 73   |
| 95   | 75   |
| 99   | 78   |

### 🪜 Ladders (Base → Top)

| Base | Top |
|------|-----|
| 4    | 14  |
| 9    | 31  |
| 20   | 38  |
| 28   | 84  |
| 40   | 59  |
| 51   | 67  |
| 63   | 81  |
| 71   | 91  |

---

## 🏛️ Architecture

This project follows **SOLID principles** and separates concerns across focused classes:

| Class | Responsibility |
|-------|---------------|
| `Dice` | Rolls a random number 1–6 using a single shared `Random` instance |
| `Player` | Holds player state — name, position, turn count, and win status |
| `Board` | Owns the snake/ladder maps and evaluates any position after a move |
| `GameEngine` | Orchestrates the game loop, turn order, dice rolls, and win detection |
| `ConsoleHelper` | Handles all console output and validated user input — keeps logic classes clean |
| `Program` | Thin entry point — collects setup input and wires everything together |

> **Why a single `Random` instance?**  
> Instantiating `new Random()` in a tight loop risks getting the same seed (especially pre-.NET 6), producing repeated dice values. A single `static readonly Random` shared across all rolls avoids this entirely.

---

## 👤 Author

**Megh Godbole (Ganesh)**  
Full-Stack Software Engineer  
📧 megh.godbole7492@gmail.com  
🐙 [github.com/Megh-Godbole](https://github.com/Megh-Godbole)

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

*Built with ❤️ and C# — because every great developer has a Snake and Ladder game somewhere in their repo.*
