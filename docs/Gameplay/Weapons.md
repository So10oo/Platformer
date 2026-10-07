# Оружие

```
IWeapon.DealingDamage()
 └── Weapon
      └── CoolDownWeapon                 # _timeCooldown, BeforeDealingDamage()
           ├── Hand                      # актуальная ближняя атака
           ├── Sword                     # Weapon/Legasy
           └── Staff                     # Weapon/Legasy
```

`Weapon.DealingDamage()` шлёт `OnDealingDamage`. КД подписывается в `OnStartMonoBehaviour`. `BeforeDealingDamage()`: на кд — `true` и выход; иначе `base.DealingDamage()` (взвод кд) и `false`.

Герой вызывает `_weapon?.DealingDamage()` из `AttackableState` по ЛКМ (`Character.Attack`).

## Герой

- `GetWeapon(Weapon)` уничтожает старое и парентит переданный объект в `_transformWeapon` (без Instantiate).
- Подбор: `SingleContainer` инстанцирует `Object`, берёт `Weapon`, отдаёт в `GetWeapon`.

## Hand

`OverlapBox` в позиции/масштабе transform по `_layerMask`. На старте собирает `Damaging(5, RepulsiveEffect(...))`. Аниматор не трогает.

## Legacy

### Sword

`Prefabs/Weapon/Sword.prefab`. На `_attackTime` включает коллайдер и крутит euler Z. `IHealth` не вызывает.

### Staff + MagicBall

Спавн шара, `Fire(±1)` по `lossyScale.x`. Полёт по X, затем `Destroy`. Хитбокса нет. Пул не используется.

## Слот врага

`WeaponSlot`: в `Awake` берёт `Weapon` из детей. Патрульный в `AttackingStatus` через 0.3 с после триггера атаки зовёт `WeaponSlot.weapon.DealingDamage()`.
