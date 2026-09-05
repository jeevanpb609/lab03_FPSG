# Football Penalty Game ⚽

A 2D Unity game where you control a player to score goals against a goalkeeper.

---

## 🎮 Controls

| Key | Action |
|-----|--------|
| **Arrow Keys** | Move player |
| **Space (1st press)** | Attach ball to player |
| **Space (2nd press)** | Shoot ball |
| **ESC** | Exit game |

---

## ⚽ Gameplay

1. Move player to the ball using arrow keys
2. Press **Space** to pick up the ball
3. Press **Space** again to shoot toward the goal
4. Score increases when ball crosses the goal line
5. Ball respawns after 1 second

---

## ✨ Features
- Player movement with arrow keys
- Ball attachment and shooting mechanic
- Goal detection with score counter
- Ball respawn after each goal
- Simple AI goalkeeper movement
- Score tracking with UI display

---

## 📁 Project Structure
- `Assets/_PenaltyGame/` - All game files
  - `Prefabs/` - Reusable game objects
  - `Scenes/` - Game scene
  - `Scripts/` - All C# scripts
  - `Sprites/` - Game graphics

---
 
## 📁 Scripts

| Script | Purpose |
|--------|---------|
| `PlayerController` | Movement, ball attach, shoot |
| `GoalkeeperAI` | AI movement along goal line |
| `GoalDetector` | Goal detection, score, delay |
| `ScoreManager` | Score tracking & UI |
| `BallManager` | Ball spawn & respawn |

---

## 🛠️ Technologies Used
- Unity 6
- C#
- TextMeshPro for UI
