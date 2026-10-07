# HP и урон

Система маленькая: здоровье на объекте, урон — число, попадание — триггер с `HealthPointCheck`.

## HealthPoint

Компонент на герое и на врагах.

- `_maxValue` в инспекторе.
- `Start` → `ResetHealthPoint()` (полное HP, `_isDead = false`).
- Сеттер `Value` клампит в `[0, MaxValue]`, шлёт `OnHealthChange(old, new)`.
- При первом достижении 0 — `OnDeath`, дальше повторно не зовётся, пока не `ResetHealthPoint()`.

Подписки на смерть героя в `Character` закомментированы: объект при 0 HP сам не выключается.

## Damaging

Обычный класс (не MonoBehaviour): только `int Value`, задаётся в конструкторе. Живёт внутри оружия и пуль (`new Damaging(1)`). Отдельного «источника урона» с эффектами нет.

## Как наносится урон

1. У атакующего на триггере висит `HealthPointCheck` (`ComponentCheck<HealthPoint>`).
2. `OnTriggerEnter2D` → если на другом объекте есть `HealthPoint`, событие `EnterComponent`.
3. Подписчик делает `healthPoint.Value -= damaging.Value`.

Так работают:

- `Hand` — ближняя атака героя (хитбокс + анимационный триггер `Attack`).
- `Bullet` — снаряд пушки: попадание снимает 1 HP и возвращает пулю в пул; по таймеру `_lifeTime` тоже `Release()`.

`Sword` (legacy) включает `Collider2D` клинка на время замаха, но сам `HealthPoint` не трогает — урона нет, пока на клинке нет `HealthPointCheck` с подпиской.

`MagicBall` двигается по X и уничтожается по таймеру, урона нет.

## UI

`Installer` после спавна героя:

```csharp
health.OnHealthChange += (prev, next) =>
    HealthPointView.ViewData(next / (float)health.MaxValue);
```

`HealthPointView` — fillAmount картинки-бара (`View<float>`). Стартовое заполнение бара зависит от того, был ли уже `OnHealthChange` (при `ResetHealthPoint` событие не шлётся).

## Враги и HP

Базовые состояния патрульного наследуют `AttackedStatus`: на `OnHealthChange` сразу переход в `pursuing` (агро с урона). В `Exit` подписка снова `+=`, а не `-=` — слушатель копится.

Отдельной логики смерти врага нет: `OnDeath` у патрульного никто не обрабатывает.
