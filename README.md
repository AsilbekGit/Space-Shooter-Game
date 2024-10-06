# Space Shooter Game

A 2D space shooter game built using Unity, where players control a spaceship to destroy enemies, collect power-ups, and survive as long as possible. The game includes multiple power-up features such as shields and multi-shot abilities, giving the player temporary enhancements to face waves of enemies.

## Documentation on Google Docs and Presentation on Canva
Google Docs - https://docs.google.com/document/d/13A81vZo_E5Fa48ZfIxXzjhUEmNsp5r8_3Pm6865HX-E/edit
Canva Presentation -  https://www.canva.com/design/DAGSz6npT8A/mdmwOFwhlerV4LDOSul-Bw/edit?utm_content=DAGSz6npT8A&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton
## Table of Contents

- [Features](#features)
- [Setup](#setup)
- [How to Play](#how-to-play)
- [Power-Ups](#power-ups)
- [Scripts Overview](#scripts-overview)
- [Future Improvements](#future-improvements)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Player Controls**: Move the player spaceship freely within the screen boundaries and shoot missiles to destroy enemies.
- **Enemies**: Waves of enemies move towards the player that must be shot down.
- **Power-Ups**: Collect power-ups like shields and multi-shot to increase your survival chances.
- **Game Over Screen**: Displays game over information when the player runs out of lives.
- **Score System**: Gain points for each enemy destroyed.

## Setup

### Prerequisites

- Unity 2021.3 or higher
- Basic knowledge of Unity and C#

### Installation

1. Clone this repository:
2. Open the project in Unity.
3. Ensure all scenes and assets are properly loaded.
4. Press the Play button in Unity to start testing.

## How to Play

- **Movement**: Use the **W**, **A**, **S**, **D** keys or the **arrow keys** to move the spaceship in all directions.
- **Shooting**: Press the **Space** key to shoot missiles.
- **Objective**: Destroy incoming enemies, collect power-ups, and survive as long as possible.
- **Lives**: The player starts with 3 lives. If an enemy hits the player, a life is lost.
- **Score**: Gain points for each enemy destroyed. The score increases with each obstacle eliminated.

## Power-Ups

- **Shield Power-Up**: Grants temporary immunity to damage. The shield appears around the player and lasts for a specified duration.
- **Multi-Shot Power-Up**: Allows the player to shoot three missiles at once for a limited time, increasing attack capability.

### How to Add Power-Ups

1. **Shield Power-Up**: Spawns randomly on the screen. When collected, the player becomes temporarily invincible. The shield power-up sprite is visible around the player.
2. **Multi-Shot Power-Up**: Spawns randomly, and when collected, the player will fire multiple missiles simultaneously.

## Scripts Overview

### PlayerController.cs

Handles player movement, shooting, and interaction with power-ups. Key features include:

- **Movement Controls**: Control the player's position on the screen.
- **Shooting Logic**: Manage single and multi-shot modes.
- **Collision Handling**: Handle interactions between the player and enemies or power-ups.
- **Score Tracking**: Update the score each time an enemy is destroyed.

### GameManager.cs

Manages the overall game state, including spawning enemies and power-ups.

- **Spawn Enemies**: Uses `InvokeRepeating` to generate enemies at intervals.
- **Spawn Power-Ups**: Spawns random power-ups during gameplay at set intervals.
- **Game Over Logic**: Handles the game-over state and displays the game-over UI.
- **Score Management**: Keeps track of the player's score during gameplay.

### PowerUpController.cs

Controls the behavior of power-ups in the game, including movement and player interaction.

- **Movement**: Moves power-ups downward across the screen.
- **Collision**: Detects collision with the player and activates respective power-up abilities.

### MissileController.cs

Handles missile behavior, including movement and collision detection.

- **Movement**: Moves the missile upwards on the screen.
- **Collision**: Detects when a missile hits an enemy and triggers an explosion effect.

### enemyController.cs

Controls enemy behavior, including their movement patterns.

- **Movement**: Moves enemies downward towards the player, making them obstacles to avoid or destroy.
- **Score Increment**: Increments the player's score when destroyed.

## Future Improvements

- **New Power-Ups**: Add more types of power-ups, such as speed boost or health regeneration.
- **Enemy Variety**: Introduce different types of enemies with unique behaviors.
- **Audio**: Add sound effects for shooting, explosions, and power-up collection.

## Contributing

1. Fork the repository.
2. Create your feature branch (`git checkout -b feature/NewFeature`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature/NewFeature`).
5. Open a Pull Request.

---





Thank you for checking out the Space Shooter Game! Have fun playing, and don't forget to collect those power-ups!
