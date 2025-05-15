# Tank vs Zombie 2.0

**Tank vs Zombie 2.0** is a top-down survival game built in Unity. Players must survive across enemy-infested maps, battling ghosts, enemy tanks, and special threats. Developed as the final project for our game development course.

---

## 🎮 Game Features

- **Multiple Modes**
  - Solo Player
  - Two-Player Co-op
  - Player + Server AI support tank

- **Map-Based Progression**
  - Ghost Valley
  - Crimson Arsenal
  - Steel Undead
  - Maintenance Scene (End Screen)

- **Core Gameplay**
  - Survive for 5 minutes or meet map-specific goals
  - One-hit death or health-based survival (map-dependent)
  - Enemy spawners, smart tracking, and shooting AI
  - Victory and Game Over screens

- **Sound & UI**
  - Button sounds and gameplay SFX
  - Background music in Maintenance Scene only
  - Floating score effects and animated transitions

---

## 🧪 Controls

**Player 1**
- Move: `W`, `A`, `S`, `D`
- Shoot: `Space`

**Player 2**
- Move: `Arrow Keys`
- Shoot: `K`

**Server AI**
- Automatically follows players and shoots enemies

---

## 🧩 Scene Flow (Build Order)

> The game should be played **starting from the `MainMenu` scene**.  
> **Do not run the `IntroSplash` scene manually** — it's only used at build startup.

1. `IntroSplash` *(only shows when you build the game)*
2. `MainMenu` *(main hub)*
3. `Mode` *(select player mode)*
4. `WaitScene` *(loading transition)*
5. `GhostValley` *(map 1: destroy ghosts)*
6. `WaitScene`
7. `CrimsonArsenal` *(map 2: survive tank ambushes)*
8. `WaitScene`
9. `SteelUndead` *(map 3: final battle)*
10. `WaitScene`
11. `Maintenance` *(end screen with background audio)*

---

## 🧪 How to Test the Game in Unity

1. Open the project in **Unity 2022.3 LTS or newer**
2. Open the scene named: `MainMenu` (`Assets/Scenes/MainMenu.unity`)
3. Click the **Play button** in Unity Editor
4. Choose a mode and play through maps

✅ Do **not** run `IntroSplash` or `WaitScene` directly during testing — they are only for build transitions.

---

## 🛠️ How to Build the Game

To create a playable version:

1. In Unity, go to **File > Build Settings**
2. Click **"Add Open Scenes"** to include all necessary scenes, or manually add them in order listed above
3. Choose your target platform (e.g., Windows)
4. Click **Build**
5. The game will start at `IntroSplash` → then `MainMenu`

---

## 🧾 Project Folder Structure

```
Assets/
├── Scenes/              # All game maps and menus
├── Scripts/             # Player, enemy, UI, audio, spawner logic
├── Prefabs/             # Tanks, bullets, ghosts, UI elements
├── Audio/               # SFX and background sounds
├── UI/                  # Canvas and animated overlays
```

---

## 👥 Team Members

- **Tai Truong** (Project Lead & Programmer)

---

## 📜 License

For educational use at SDSU. This game was developed as a final project and is not intended for commercial distribution.
