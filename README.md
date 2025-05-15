# Tank vs Zombie 2.0

**Tank vs Zombie 2.0** is a top-down survival game built in Unity. Players must navigate hostile zones filled with ghosts, enemy tanks, and special threats across multiple maps. This game was created as the final project for our game development course.

---

## 🎮 Game Features

- **Multiplayer Modes**
  - Solo Mode (Player 1 only)
  - Co-op Mode (Player 1 + Player 2)
  - Player + Server AI (autonomous support tank)

- **Survival Mechanics**
  - Each map has its own win condition (time survival, kill count, etc.)
  - One-hit death or life points depending on the map
  - Unique level goals and difficulty scaling

- **Maps & Progression**
  - **Ghost Valley** – survive and destroy ghosts
  - **Crimson Arsenal** – face tank ambushes with limited health
  - **Steel Undead** – final survival zone before Maintenance
  - **Maintenance Scene** – transition scene with background sound

- **UI & Sound**
  - Intro & transition boards
  - Victory / Game Over screens
  - Button and gameplay sound effects
  - Background audio in Maintenance scene only

---

## 🧪 Controls

**Player 1:**
- Move: `W` `A` `S` `D`
- Shoot: `Space`

**Player 2:**
- Move: `Arrow Keys`
- Shoot: `K`

**Server AI:**
- Moves automatically
- Shoots enemies on detection

---

## 🧩 How to Play

1. Launch the game through the **Main Menu**
2. Select a play mode (Solo, Co-op, or Server AI)
3. Survive on each map by completing the conditions
4. Watch for victory or game over transitions
5. Reach the Maintenance screen to end the final stage

---

## 🛠️ Project Structure

```
Assets/
├── Scenes/              # Each game map and menu scene
├── Scripts/             # Player, Enemy, UI, Spawner, Manager scripts
├── Prefabs/             # Players, Enemies, Bullets, UI Prefabs
├── Audio/               # Sound clips used in gameplay and UI
└── UI/                  # Canvases, Buttons, Text, and FX
```

---

## 📁 Notes for Rebuilding

- Built using **Unity 2022+**
- Recommended to open via **Unity Hub**
- Make sure to allow time for initial Unity re-import (Library folder excluded from Git)

---

## 👥 Team Members

- **Tai Truong** (Project Lead, Programmer)
- **James Johnson** (Designer, UI)
- **Kyle Kucharski** (Data & Assets)
- **Wissam Almasri** (Testing & Support)

---

## 📜 License

Educational project submitted to SDSU for course credit. Not intended for commercial use.
