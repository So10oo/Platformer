# DI / Zenject

## Goals

- Keep Unity scene/prefab wiring minimal and explicit
- Centralize runtime object graph creation in installers
- Make “who creates what” easy to find

## Current composition root(s)

Scripts:

- `Platformer/Assets/Scripts/MonoInstallers/Installer.cs`
- `Platformer/Assets/Scripts/MonoInstallers/PlayerInstaller.cs` (overlaps with `Installer.cs`)

### Installer responsibilities (today)

`Installer.cs` currently:

- binds `InputService` as a singleton instance
- binds `StateMachineEvents<Character>` as a singleton instance
- instantiates `HeroPrefab` at `StartPoint`
- binds the created `Character` instance
- binds UI view instances (e.g. `HealthPointView`, `DialogPanel`)
- does a piece of runtime wiring (UI subscribing to `HealthPoint` change)

## Guidelines (to keep DI maintainable)

- Prefer **one** scene installer per scene (avoid overlapping installers with similar bindings).
- Bind “pure services” as interfaces (`IInputService`, `IFactory`, etc.) to reduce coupling.
- Avoid putting gameplay logic into installers (installers should wire dependencies, not implement mechanics).
- If an object implements `IDisposable`, decide explicitly who disposes it and when.

## When adding a new system

Add/extend docs in the same PR:

- Update `docs/Architecture.md` module list
- Add a short section to `docs/DI-Zenject.md` describing bindings and lifecycle
- If you made a structural decision, add an ADR entry in `docs/ADR/`
