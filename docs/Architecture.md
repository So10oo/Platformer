# Архитектура

2D-платформер: герой с машиной состояний, интерактивы на уровне, патрульные враги, пушка, графовый редактор диалогов.

## Стек

| Слой | Что |
|------|-----|
| Unity | 2022.3.10f1, URP 2D |
| Ввод | Input System 1.7 (`InputService.inputactions`) |
| DI | Extenject / Zenject (`Assets/Plugins/Zenject`) |
| UI текста | TextMesh Pro |
| Арт | Cainos Pixel Art (персонаж, пропсы деревни) |

Корень Unity-проекта — папка `Platformer/`. Игровые скрипты: `Platformer/Assets/Scripts/`.

## Сцены

- `Assets/Scenes/Game.unity` — основная игровая сцена.
- `Assets/Scenes/Menu.unity`, `test.unity` — меню и песочница.

## Композиция (Zenject)

Точка сборки сцены — `MonoInstallers/Installer.cs` (`MonoInstaller`).

При старте он:

1. Создаёт и биндит `InputService` (New Input System).
2. Создаёт и биндит `StateMachineEvents<Character>` — общая машина состояний героя.
3. Спаунит префаб героя в `StartPoint`, биндит `Character` как singleton.
4. Подписывает `HealthPoint` героя на UI-бар `HealthPointView`.
5. Биндит `DialogPanel` (панель диалога со сцены).
6. Биндит фабрику `FactoryWithDiContainer` как `IFactory`.

`PlayerInstaller` — второй инсталлер с пересекающимися биндингами (герой уже на сцене, без спавна). На основной сцене используется `Installer`.

Зависимости в компоненты попадают через `[Inject] void Construct(...)`.

```
Installer
  ├── InputService
  ├── StateMachineEvents<Character>
  ├── Character (prefab → StartPoint)
  ├── HealthPointView  ←  HealthPoint.OnHealthChange
  ├── DialogPanel
  └── IFactory / FactoryWithDiContainer
```

## Каркас геймплея

Общий каркас — своя FSM, не Animator Controller.

- `Patterns/StateMachine/State<T>` — Enter / HandleInput / LogicUpdate / FixedUpdate / LateUpdate / Exit.
- `StateMachine<T>` — хранит current/previous, `ChangeState`.
- `StateMachineEvents<T>` — перехват смены: `WhenAttemptingChangeState` (можно отменить) и `OnChangeState`.

Герой крутит FSM из `Character`: `Update` → input+logic, `FixedUpdate` → физика. Стартовое состояние — `freeFall`.

Патрульный крутит отдельную `StateMachine<Patroller>` внутри `Patroller`, без Zenject-биндинга самой машины.

## Ввод

Карта `GamePlay` в `Assets/Scripts/InputService/InputService.inputactions`:

- `Move` — Vector2, WASD
- `Jump` — Space
- `Dash` — Left Shift
- `Interactive` — F
- `Attack` — ЛКМ

Схема устройств: Keyboard+Mouse. `Character` включает/выключает asset вместе с `OnEnable` / `OnDisable`.

## Вспомогательные паттерны

| Паттерн | Где | Зачем |
|---------|-----|--------|
| Pool | `Patterns/Pool/` | Снаряды пушки (`Bullet` : `ElementPool`) |
| Factory | `Patterns/Factory/` | Спавн через DiContainer |
| View | `Views/` | UI/визуал без логики (`HealthPointView`, `RotateView`, `Plaque`) |
| ScriptableObject settings | `Settings/` | Твикнутые числа движения и ИИ |
| ReactiveProperty | `ReactiveProperty.cs` | Простой observable-контейнер, почти не используется |

`RotateView` зеркалит `localScale` по оси (у героя режим Z) и шлёт событие `Rotate` — речь врага (`SpeechWindow`) не переворачивается вместе с телом.

## Физика и слой проверок

Герой — `Rigidbody2D`. Горизонталь — `AddForce` impulse + торможение `MoveTowards`. Прыжок/dash — импульс и кривая скорости.

Перекрытия с миром идут через триггеры `Check` / `LayerCheck` / `ComponentCheck<T>` (земля, потолок, зона игрока, хитбокс оружия). Подробнее: [Interactives.md](Gameplay/Interactives.md).

## Префабы

- `Prefabs/Hero.prefab`
- `Prefabs/Weapon/` — Sword, MagicStaff, MagicBall
- `Prefabs/NPC/Cannon/` — пушка и `Bullet`
- `Prefabs/UI/ButtonChoices.prefab` — кнопка варианта в диалоге

На герое в Play в левом верхнем углу рисуется IMGUI-оверлей: текущее состояние FSM, velocity, горизонтальный ввод.
