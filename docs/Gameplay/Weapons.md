# Оружие

Иерархия:

```
IWeapon.Attack()
 └── Weapon (MonoBehaviour)
      └── CoolDownWeapon          # таймер _timeCoolDown, BeforeAttack()
           ├── Hand               # актуальная ближняя атака
           ├── Sword              # Weapon/Legasy — замах коллайдером
           └── Staff              # Weapon/Legasy — спавн MagicBall
```

`Weapon` шлёт `OnAttack` / `OnUpdate`. КД подписывается на эти события в `OnStart`. `BeforeAttack()`: если кд активен — вернуть `true` и ничего не делать; иначе вызвать `base.Attack()` (взвод кд) и вернуть `false`.

## Герой

Поля `Character`: текущий `Weapon` и слот `_transformWeapon`.

- `Attack()` — из `AttackableState` по ЛКМ: `_weapon?.Attack()`.
- `GetWeapon(Weapon)` — уничтожает старое, парентит новое в слот (без Instantiate: объект уже должен существовать).

Подбор: `SingleContainer` (интерактив). По F инстанцирует `Object`, берёт `Weapon`, отдаёт в `GetWeapon`. Повторно не подбирается (`Object = null`).

## Hand

Сериализованный `HealthPointCheck` + `Animator`. Атака: `anim.SetTrigger("Attack")`. Попадание триггера в `HealthPoint` → `Value -= 1`.

## Legacy

Папка `Weapon/Legasy/` — старые прототипы, префабы ещё есть.

### Sword

`Prefabs/Weapon/Sword.prefab`. На `_attackTime` включает коллайдер и крутит euler Z на 60°. Урон через `HealthPoint` не начисляет.

### Staff + MagicBall

`Prefabs/Weapon/MagicStaff.prefab`, `Prefabs/Weapon/Projectile/MagicBall.prefab`.

`Staff.Attack` спавнит шар и `Fire(±1)` по `lossyScale.x`. Шар летит по X со `_speed`, через `_timeDestroy` `Destroy`. Хитбокса/урона нет. Пул не используется (в отличие от пуль пушки).

## Слот врага

`WeaponSlot` — на патрульном. `Awake` берёт `Weapon` из детей. `SetWeapon` уничтожает старое и `Instantiate` новое. В преследовании при дистанции &lt; 1 вызывается `WeaponSlot.weapon.Attack()`.
