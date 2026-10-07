# DI / Zenject

Сцена собирается через Extenject. Зависимости в компоненты приходят `[Inject] void Construct(...)`.

## Composition root

| Инсталлер | Роль |
|-----------|------|
| `MonoInstallers/Installer.cs` | Сцена `Game`: спавн героя, сервисы, UI |
| `MonoInstallers/PlayerInstaller.cs` | Альтернатива: биндит уже лежащий на сцене `Character`. Пересекается с `Installer` |

На основной сцене нужен один из них. Сейчас канон — `Installer`.

## Что биндит `Installer`

1. `InputService` — `new InputService()`, `AsSingle()`.
2. `StateMachineEvents<Character>` — общая FSM героя, `AsSingle()`.
3. Префаб героя инстанцируется в `StartPoint`, биндится `Character`.
4. `HealthPoint` героя подписывается на `HealthPointView.ViewData(next / MaxValue)`.
5. `DialogPanel` со сцены — `AsSingle()`.
6. `FactoryWithDiContainer` как `IFactory` (маркер Zenject, не проектный `IFactory<T>`).

`FactoryWithDiContainer.Create<T>` — обёртка над `DiContainer.InstantiatePrefabForComponent`.

## Кто ещё инжектится

| Потребитель | Что просит |
|-------------|------------|
| `Character` | `InputService`, `StateMachineEvents<Character>` |
| `DialogPanel` | `Character`, `StateMachineEvents<Character>` |
| `Conversation` | `DialogPanel` (+ из `Construct` Interactive — `Character`) |
| `AltarDash` / `AltarWings` | `InputService`, `StateMachineEvents<Character>` |
| `EnemyEyes`, `Cannon`, `Follow` | `Character` (цель / follow) |
| `Interactive` | `Character` |

Инсталлер не должен содержать игровую логику. Сейчас исключение — подписка HP → UI прямо в `InstallerHero()`.
