# Архитектура

2D-платформер: герой на FSM, интерактивы, патрульные, пушка, графовый редактор диалогов, черновик сетки инвентаря.

## Стек

Кратко: Unity **2022.3.62f1**, URP 2D, Input System, Zenject, Odin Inspector, TextMesh Pro, Cainos. Подробности — [TechStack.md](TechStack.md).

Корень Unity-проекта — `Platformer/`. Геймплей: `Platformer/Assets/Scripts/`. Диалоги вынесены в `Assets/DialogueSystem/`. Инвентарь — `Assets/InventorySystem/` (пока не вплетён в сцену `Game`).

## Сцены

- `Assets/Scenes/Game.unity` — основная.
- `Assets/Scenes/Menu.unity`, `test.unity` — меню и песочница.

## Композиция

Точка сборки `Game` — `MonoInstallers/Installer.cs`. Подробности биндингов — [DI-Zenject.md](DI-Zenject.md).

```
Installer
  ├── InputService
  ├── StateMachineEvents<Character>
  ├── Character (prefab → StartPoint)
  ├── HealthPointView  ←  HealthPoint.OnHealthChange
  ├── DialogPanel
  └── IFactory / FactoryWithDiContainer
```

`PlayerInstaller` — второй инсталлер с пересекающимися биндингами (герой уже на сцене). На `Game` используется `Installer`.

## Каркас геймплея

Своя FSM, не Animator Controller как источник логики.

- `Patterns/StateMachine/State<T>` — Enter / HandleInput / LogicUpdate (`bool`: переход уже случился) / FixedUpdate / LateUpdate / Exit.
- `StateMachine<T>` — current/previous, `ChangeState`.
- `StateMachineEvents<T>` — `WhenAttemptingChangeState` (можно отменить вход) и `OnChangeState`.

Герой крутит FSM из `Character`. Старт — `freeFall`. Патрульный держит свою `StateMachine<Patroller>` внутри `Patroller`.

## Модули

| Модуль | Где | Заметка |
|--------|-----|---------|
| Герой | `Hero/` | `Character` + `DictionaryCharacterStates` |
| Ввод | `InputService/` | Generated Input System, карта `GamePlay` |
| Урон | `DamageSystem/` | `IHealth` / `IDamaging` / `IHealthEffect` |
| Оружие | `Weapon/` | Контракт `DealingDamage()` |
| Враги | `NPC/` | Patroller, Cannon |
| Диалоги | `DialogueSystem/` + `CustomDialogSystem/` | Редактор + UI |
| Интерактивы | `Interactives/` | F в зоне `LayerCheck` |
| Check | `Check/` | `LayerCheck`, `ComponentCheck<T>` |
| Инвентарь | `InventorySystem/` | Сетка 4×4, тестовый `EntryPoint` |
| View | `Views/` | HP-бар, поворот, таблички |
| Паттерны | `Patterns/` | FSM, Pool, Factory |

## Ввод

Карта `GamePlay` (`InputService.inputactions`): Move (WASD), Jump (Space), Dash (Left Shift), Interactive (F), Attack (ЛКМ). Схема Keyboard+Mouse. Asset включается/выключается с `Character.OnEnable/OnDisable`.

## Физика и взгляд

Герой — `Rigidbody2D`. Горизонталь: impulse + торможение. Поворот — `RotateView` по оси X (знак `localScale.x`).

Камера: `Hero/Follow.cs` (Lerp в LateUpdate). Пакет Cinemachine в проекте есть, игровой follow на нём не сидит.

## Префабы

- `Prefabs/Hero.prefab`
- `Prefabs/Weapon/` — Sword, MagicStaff, MagicBall
- `Prefabs/NPC/Cannon/`
- `Prefabs/UI/ButtonChoices.prefab`

На герое в Play — IMGUI: имя состояния, velocity, ось Move.X.
