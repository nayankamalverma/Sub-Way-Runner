# Sub-way-Runner

<div align="center">
  <img src="https://via.placeholder.com/600x300/4ecdc4/ffffff?text=Sub-way-Runner+Game" alt="Sub-way-Runner Game Screenshot" width="600">
</div>

A endless 3D runner game built with Unity #D

## 🎯 Features
- Endless 3D running gameplay
- Smooth player controls with lane switching
- Dynamic obstacle generation
- Building environment with procedural generation
- Score tracking and speed progression
- Object pooling for optimal performance

## 🎮 Controls
- **Jump**: W or ↑ Arrow key
- **Move Left**: A or ← Arrow key  
- **Move Right**: D or → Arrow key

## 🏗️ Code Architecture

```
GameManager (ServiceLocator)
                    |
    +---------------+---------------+
    |               |               |
PlayerService  LevelService  ScoreService
    |               |
PlayerController    |
    |       +-------+-------+
    |       |               |
    |  ObstacleService BuildingService
    |       |               |
    |  ObstaclePool    BuildingPool
    |
    +-- EventService (Injected into all components)
        |
+-------+-------+-------+
|       |       |       |
Player Level Obstacle Building
```
## 🎨 Game Elements

| Element | Description |
|---------|-------------|
| ![Player](https://via.placeholder.com/50x50/4ecdc4/ffffff?text=P) | Player character  |
| ![Obstacle](https://via.placeholder.com/50x50/ff6b6b/ffffff?text=O) | Obstacles to avoid (Trains and barriers) |
| ![Building](https://via.placeholder.com/50x50/9b59b6/ffffff?text=B) | Background buildings  |

## 📊 Performance Features
- **Object Pooling**: Reuses game objects for better performance
- **Service Architecture**: Clean separation of concerns
- **Event-Driven**: Loose coupling between components
- **Memory Management**: Efficient cleanup of unused objects

## 🔧 Development

The game follows a clean architecture pattern:
- **GameManager**: Central service locator
- **Services**: PlayerService, LevelService, ScoreService, ObstacleService, BuildingService  
- **Event System**: Central communication hub
- **Object Pools**: Memory-efficient object management

## 📊 Assets 
- **Train Kit**: https://kenney.nl/assets/train-kit
- ** Polygon-sample pack**: https://assetstore.unity.com/packages/3d/environments/polygon-sampler-pack-207048

---
<div align="center">
  <strong>🏃‍♂️ Keep Running! 🏃‍♂️</strong>
</div>
