# Документация

Снимок ветки **`develop`**. Описывает код как есть, не целевую архитектуру.

## Системы

| Система | Файл | Код |
|---------|------|-----|
| Общая архитектура | [Architecture.md](Architecture.md) | `Assets/Scripts/` |
| DI / Zenject | [DI-Zenject.md](DI-Zenject.md) | `MonoInstallers/` |
| Мувмент героя | [Gameplay/Movement.md](Gameplay/Movement.md) | `Hero/`, `Hero/States/` |
| Диалоги | [Gameplay/Dialogue.md](Gameplay/Dialogue.md) | `DialogueSystem/`, `CustomDialogSystem/` |
| HP, урон, эффекты | [Gameplay/HealthAndDamage.md](Gameplay/HealthAndDamage.md) | `DamageSystem/` |
| Оружие | [Gameplay/Weapons.md](Gameplay/Weapons.md) | `Weapon/` |
| Враги и пушка | [Gameplay/Enemies.md](Gameplay/Enemies.md) | `NPC/` |
| Интерактивы и Check | [Gameplay/Interactives.md](Gameplay/Interactives.md) | `Interactives/`, `Check/` |
| Инвентарь | [Gameplay/Inventory.md](Gameplay/Inventory.md) | `InventorySystem/` |

## Прочее

- [TechStack.md](TechStack.md) — Unity, пакеты, плагины
- [Runbook.md](Runbook.md) — запуск, сборка, типичные поломки
- [Contributing.md](Contributing.md) — как обновлять доки вместе с кодом
- [ADR/](ADR/) — короткие записи архитектурных решений

## Соглашения в коде

- Игровые типы в основном в глобальном namespace.
- Диалоги: `DialogueSystem.Editor` / `DialogueSystem.Realtime`.
- Проверки коллизий: `CustomCheck`.
- Инвентарь: `Inventory`.
- Настройки героя и патрульного: `Assets/Settings/PlayerSettings.asset`, `PatrollerSettings.asset`.
