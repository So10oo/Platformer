# HP и урон

Контракты: `IHealth` / `IDeathEvent` / `IDamaging` / `IHealthEffect`. Реализация здоровья — `HealthPoint`.

## HealthPoint

Компонент на герое и врагах, реализует `IHealth`.

- `_maxValue` в инспекторе.
- `Start` → `ResetHealthPoint()` (полное HP, `isDead = false`). Событие `OnHealthChange` при ресете **не** шлётся.
- Сеттер `CurrentValue` клампит в `[0, MaxValue]`, шлёт `OnHealthChange(old, new)`.
- Первое достижение 0 → `OnDeath`, дальше молчит до ресета.
- `TakeDamage(int)` вычитает из `CurrentValue`.

Подписка на смерть героя в `Character` закомментирована.

## IHealth.TakeDamage(IDamaging)

Default-реализация на интерфейсе:

1. `TakeDamage(damaging.Value)`
2. `SetEffects(damaging.Effects)` — у каждого эффекта `SetEffect(this)`

`Damaging` хранит `int Value` и `List<IHealthEffect>`. Конструкторы: только число; число + `params` эффектов; число + `IEnumerable` (без проверки на null).

## Эффекты

| Класс | Поведение |
|-------|-----------|
| `BleedingEffect(timeEffect, timeTick)` | Корутина на `MonoBehaviour` здоровья: каждый тик `TakeDamage(1)` |
| `RepulsiveEffect(force, drummer)` | Импульс `(sign(цель − бьющий), 1) * force` по Rigidbody2D цели |

Эффекты не стакаются явно и не отменяются: кровотечение просто стартует ещё одну корутину.

## Как наносится урон

Два пути:

**Overlap (рука героя).** `Hand` делает `Physics2D.OverlapBox` по `_layerMask`, берёт `IHealth` и `TakeDamage(_damaging)`. Сейчас урон 5 + `RepulsiveEffect(15, 10)` от `transform.parent`.

**Триггер.** `HealthPointCheck` = `ComponentCheck<IHealth>`. Подписка через `SubscribeEnter` / `UnsubscribeEnter` (явная реализация интерфейса `IComponentCheckEnter<T>`). Так работает `Bullet`: попадание в `IHealth` → `TakeDamage(1)` и возврат в пул; касание земли (`LayerCheck.InLayerChange`) тоже `Release()`.

`Sword` включает коллайдер на замах, `IHealth` сам не трогает. `MagicBall` урона не даёт.

## UI

`Installer` после спавна:

```csharp
health.OnHealthChange += (prev, next) =>
    HealthPointView.ViewData(next / (float)health.MaxValue);
```

`HealthPointView` — `fillAmount` картинки.

## Враги

`AttackedStatus` слушает `OnHealthChange` и, если уже не в `pursuing`, переходит туда. В `Exit` подписка снимается (`-=`). Смерть патрульного отдельно не обрабатывается.
