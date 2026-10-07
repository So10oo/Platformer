# Интерактивы, проверки, окружение

## Check (`CustomCheck`)

```
ILayerCheck                    # InLayer, CountCollision, события смены
 └── LayerCheck                # счётчик входов/выходов по LayerMask

IComponentCheckEnter<T> / Exit
 └── ComponentCheck<T>         # OnTrigger → GetComponent<T>
      ├── HealthPointCheck     # T = IHealth
      ├── PlayerCheck          # T = Character
      └── ComponentInLayerCheck<T>  # слой + компонент (гибрид)
```

`LayerCheck` считает коллайдеры слоя: Enter ++, Exit −−, `InLayer = CountCollision != 0`. Это замена старого `Check.Value` / `ValueChandge`.

`ComponentCheck<T>` события прячет за явную реализацию интерфейса. Подписка: `_hitCheck.SubscribeEnter(handler)` (extension в том же namespace).

`ClimbingDetectionSystem` — не Check, а периодический Raycast вниз. См. [Movement.md](Movement.md).

## Interactive

База объектов на F.

1. На объекте — `LayerCheck`.
2. Вошёл → `character.action = ActionCharacterEvents`, вышел → `null`.
3. Обёртка зовёт `beforeAction` → `Interaction()` → `afterAction`.
4. `GroundedState`: пока зажата Interactive — `character.action?.Interaction()`.

`IActionCharacter` по-прежнему один метод `Interaction()`. Одновременно активен один интерактив.

| Класс | Поведение |
|-------|-----------|
| `Conversation` | Старт диалога, подсказка «Interactive» |
| `SingleContainer` | Один раз выдаёт оружие (подсказка в коде закомментирована) |
| `SingleInteractive` | После действия уничтожает коллайдер, `LayerCheck` и себя |
| `AltarDash` : SingleInteractive | Кладёт `DashState` в словарь, гасит текст и glow |
| `AltarWings` | Переключает на `FlyingState`, «Press F» |

Файл разговора: `Сonversation.cs` (кириллическая «С»).

## Окружение

- `Environment/Trampoline` — любой Rigidbody2D: обнуляет `vy`, импульс от центра (X `/ 5`).
- `Other/Ground` — пустой `OnCollisionEnter2D` (заготовка).
- `Plaque` — `View<bool>`, включает объект.
- Debug IMGUI на герое — [Movement.md](Movement.md).
