# Hero state machine

## Entry points

- `Hero/Character.cs` — orchestrates the state machine and Unity lifecycle methods.
- `Patterns/StateMachine/*` — generic state machine base classes.
- `Hero/DictionaryCharacterStates.cs` — registers hero states and exposes them by key.

## How it works (current)

- `Character.Start()` initializes the state machine with `states["freeFall"]`.
- Each Unity frame, `Character` delegates to the current state:
  - `HandleInput()` (read input)
  - `LogicUpdate()` (decide transitions, update timers)
  - `FixedUpdate()` (physics)
  - `LateUpdate()` (late adjustments)

## Built-in states (current)

Declared in `Hero/DictionaryCharacterStates.cs`:

- `standing`
- `moving`
- `jumping`
- `freeFall`
- `climbing`

Other states exist under `Hero/States/` (e.g., dash and related lockable/movement mixins).

## Transition examples

- `StandingState` → `MovingState` if abs(x velocity) >= threshold
- `MovingState` → `StandingState` if abs(x velocity) <= threshold
- `JumpingState` → `FreeFallState` when vertical speed <= 0, or ceiling hit, or jump released
- `FreeFallState` → grounded states when `Character.isGround` becomes true

## Extension points

When adding a new hero ability/state:

- Add a new state class under `Hero/States/`
- Register it in `DictionaryCharacterStates`
- Add a short note in this file:
  - state purpose
  - entry/exit conditions
  - key transitions

## Known limitations (documented)

- The state registry uses **string keys**. Consider moving to `enum` or typed references to reduce runtime errors.
