# Overkill-Protocol

Unity проект на версии 2022.3.62f2.

## Быстрый старт

- Откройте проект через Unity Hub (Unity 2022.3.62f2).
- Откройте сцену `Assets/Scenes/Game.unity`.
- Нажмите Play в Editor.

## Геймплей

- Управление персонажем: WASD на ПК или свайпы/тач на мобильных (см. `Assets/Scripts/Input/InputSystemPC.cs` и `Assets/Scripts/Input/MobileInputSystem.cs`).
- Наведение и стрельба по клику/тапу: поворот игрока к точке на земле и выстрел пистолета по лучу (см. `Assets/Scripts/Character/Rotate.cs` и `Assets/Scripts/Weapon/WeaponType/Pistol.cs`).
- Противники спаунятся вне видимой области камеры и идут к игроку по NavMesh (см. `Assets/Scripts/Services/Spawners/EnemySpawner.cs` и `Assets/Scripts/Enemy/Walk/EnemyWalk.cs`).
- За убийство врагов начисляются очки прокачки, которые тратятся в окне улучшений (см. `Assets/Scripts/Enemy/Factory/EnemyFactory.cs` и `Assets/Scripts/CharactersStats/UpgradeStats/UpgradeStatsService.cs`).

## Фичи

- Две UI‑панели: игровой HUD и окно апгрейдов со связкой Model‑View‑Presenter (см. `Assets/Scripts/Ui/GameplayWindowPresenter.cs` и `Assets/Scripts/Ui/UpgradeWindowPresenter.cs`).
- Прокачка статов игрока: здоровье/скорость/урон с ограниченными максимальными бафами (см. `Assets/Scripts/CharactersStats/Stats/HealthCharacter.cs`, `Assets/Scripts/CharactersStats/Stats/SpeedCharacter.cs`, `Assets/Scripts/CharactersStats/Stats/DamageStat.cs`).
- Пуллинг врагов и VFX попаданий для экономии инстанцирования (см. `Assets/Scripts/Enemy/Pooling/CustomObjectPool.cs`).
- Конфигурация через ScriptableObject‑ассеты (см. `Assets/Scripts/Di/GameSettingsInstaller.cs`).

## Архитектура и паттерны

- DI и composition root на Zenject: `Assets/Scripts/Di/GameBindInstaller.cs`, `Assets/Scripts/Di/GameSettingsInstaller.cs`, `Assets/Scripts/Di/GameUiPrefabInstaller.cs`.
- Strategy для переключения ввода по платформе: `Assets/Scripts/Services/StrategyInstaller/StrategyInitializer.cs` + `Assets/Scripts/Bootstrap/GameBootstrapper.cs`.
- MVP‑подобная схема для UI: `Assets/Scripts/Ui/PresenterBase.cs`, `Assets/Scripts/Ui/GameplayWindowModel.cs`, `Assets/Scripts/Ui/UpgradeWindowModel.cs`.
- Реактивные события и таймеры через R3: клики, апдейты статов, спавн по таймеру (см. `Assets/Scripts/Services/Spawners/EnemySpawner.cs` и `Assets/Scripts/Ui/GameplayWindowModel.cs`).
- Паттерн Builder для сборки наборов статов (см. `Assets/Scripts/CharactersStats/Builder/CharacterStatsBuilder.cs`).

## Конфиги и данные

- Инпут‑карта с группами `PC` и `Mobile`: `Assets/Scripts/Input/NewInputSystem.inputactions`.
- Параметры игрока (скорость поворота, слои для рейкаста): `Assets/Scripts/Character/Config/PlayerParameters.cs`.
- Оружие и VFX попаданий: `Assets/Scripts/Weapon/Configs/WeaponConfig.cs`.
- Параметры врагов и частота спавна: `Assets/Scripts/Enemy/Config/EnemyParameters.cs`, `Assets/Scripts/Services/Config/EnemySpawnParameters.cs`.
- Таблицы статов через Odin‑сериализованный словарь: `Assets/Scripts/CharactersStats/Impl/StatConfigCollection.cs`.

## Структура проекта

- `Assets/` игровые ассеты и код.
  - `Assets/Scripts/` runtime C# код.
  - `Assets/Scenes/` сцены Unity.
  - `Assets/Settings/` URP и проектные настройки.
  - `Assets/Plugins/` сторонние пакеты (Zenject, UniTask, Odin, R3 и др.).
- `Packages/` манифест пакетов Unity.
- `ProjectSettings/` конфигурация проекта.
