# Интерактивы, проверки, окружение

## Check

Триггерные датчики, без физики движения.

```
Check                          # Value + ValueChandge, Enter/Exit
 └── LayerCheck                # слой в LayerMask + OverlapCollider

ComponentCheck<T>              # отдельная ветка, не Check
 ├── HealthPointCheck          # T = HealthPoint
 └── PlayerCheck               # T = Character
```

`Check.Value` меняется только при смене и шлёт `ValueChandge`. На Enter достаточно одного подходящего коллайдера; на Exit `Value` пересчитывается через `CheckAllObject()`.

Используется как: земля/потолок героя, зона интерактива, край платформы патрульного, зона пушки.

`ComponentCheck<T>` ищет компонент на вошедшем объекте и шлёт `EnterComponent` / `ExitComponent`. Нужен для урона, не для булева «внутри/снаружи».

## Interactive

База объектов, с которыми герой жмёт F.

1. На том же объекте — `LayerCheck` (слой игрока).
2. Вошёл → `character.action = this`, вышел → `null`.
3. `GroundedState`: пока зажата Interactive — `character.action?.Interaction()`.
4. Наследник рисует подсказку в `View()` и реализует `Interaction()`.

| Класс | Эффект | Подсказка |
|-------|--------|-----------|
| `Conversation` | Стартует диалог | «Interactive» |
| `SingleContainer` | Один раз выдаёт оружие | «get weapon» |
| `AltarDash` | Один раз кладёт `DashState` в словарь | «Get dash» |
| `Wings` | Переключает на `FlyingState` | «Press F» |

`Wings` дополнительно слушает `OnChangeState`, чтобы прятать подсказку, когда полёт уже активен.

Контракт действия: `IActionCharacter.Interaction()`. Одновременно активен только один интерактив (последний, в чью зону вошли).

Файл разговора называется `Сonversation.cs` (кириллическая «С»).

## Прочее окружение

- `Trampoline` — любой `Rigidbody2D` в триггере: обнуляет `vy`, импульс от центра батута (X ослаблен `/ 5`).
- `Plaque` — `View<bool>`: включает/выключает объект (табличка).
- Debug-оверлей героя — см. [Movement.md](Movement.md).
