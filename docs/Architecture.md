# Architecture (high-level)

## What this project is

- **Unity 2D platformer** implemented in **C#**.
- Engine version: **Unity 2022.3.62f1 (LTS)**.
- Main build/play scene: `Platformer/Assets/Scenes/Game.unity`.

## Repository layout

- `README.md` — entry point, links to documentation.
- `Platformer/` — Unity project root
  - `Assets/` — gameplay code, scenes, prefabs, art, plugins
  - `Packages/manifest.json` — Unity packages used by the project
  - `ProjectSettings/` — Unity project configuration (including editor + build settings)
- `docs/` — documentation (this folder)

## Key modules (code)

All scripts are under `Platformer/Assets/Scripts/`.

- **Composition root / DI**
  - `MonoInstallers/Installer.cs` — scene-level installer (Zenject) that binds services and creates/binds the hero.
  - `MonoInstallers/PlayerInstaller.cs` — alternate installer (currently overlaps with `Installer.cs`).
- **Hero**
  - `Hero/Character.cs` — central MonoBehaviour orchestrating the hero state machine + Unity lifecycle hooks.
  - `Hero/DictionaryCharacterStates.cs` — registers hero states by string key.
  - `Hero/States/*` — state machine states (Standing/Moving/Jumping/FreeFall/Climbing/Dash/...).
- **Input**
  - `InputService/InputService.cs` — source-generated wrapper for Unity Input System actions (WASD/Jump/Dash/Interact/Attack).
- **Damage/Health**
  - `DamageSystem/Health/HealthPoint.cs` — basic health implementation with OnDeath / OnHealthChange.
  - `DamageSystem/Damaging/*` — damage payload (value + effects).
- **Weapons**
  - `Weapon/Weapon.cs` — base weapon behavior; hero calls `Weapon.DealingDamage()`.
- **Patterns**
  - `Patterns/StateMachine/*` — generic state machine primitives.
  - `Patterns/Pool/*`, `Patterns/Factory/*` — pooling and factory helpers.

## Runtime architecture (flow)

- Scene boots → Zenject **installer** binds services → hero is instantiated/bound
- `Character.Start()` initializes the state machine at `freeFall`
- Each frame:
  - `Update()` → `CurrentState.HandleInput()` then `CurrentState.LogicUpdate()`
  - `FixedUpdate()` → `CurrentState.FixedUpdate()`
  - `LateUpdate()` → `CurrentState.LateUpdate()`
- Input system is enabled/disabled with hero GameObject enable state (`OnEnable/OnDisable`).

## Known architectural pressure points (current)

These are documented so future changes can target them deliberately:

- **State registry uses strings** (`"freeFall"`, `"moving"`...) which is fragile.
- **States are new’ed manually** in `DictionaryCharacterStates` (limited DI for state dependencies).
- **Installer mixes composition + runtime wiring** (e.g., subscribing UI to health change).

For improvement ideas, see `docs/Contributing.md` (Architecture checklists) and add decisions to `docs/ADR/`.
