# Документация

Снимок текущей кодовой базы в рабочей копии (ветка `main`). Описывает, как системы устроены сейчас, а не как «должны» выглядеть.

## Системы

| Система | Файл | Код |
|---------|------|-----|
| Общая архитектура, DI, сцены | [Architecture.md](Architecture.md) | `Assets/Scripts/` |
| Мувмент героя | [Gameplay/Movement.md](Gameplay/Movement.md) | `Hero/`, `Hero/States/` |
| Диалоги | [Gameplay/Dialogue.md](Gameplay/Dialogue.md) | `DialogueSystem/`, `CustomDialogSystem/`, `Editor/DialogueSystem/` |
| HP и урон | [Gameplay/HealthAndDamage.md](Gameplay/HealthAndDamage.md) | `DamageSystem/` |
| Оружие | [Gameplay/Weapons.md](Gameplay/Weapons.md) | `Weapon/` |
| Враги и пушка | [Gameplay/Enemies.md](Gameplay/Enemies.md) | `NPC/` |
| Интерактивы, Check, камера | [Gameplay/Interactives.md](Gameplay/Interactives.md) | `Interactives/`, `Check/`, `Hero/Follow.cs` |

## Соглашения в коде

- Почти все геймплейные типы — в глобальном namespace (без `Platformer.*`).
- Диалоговый редактор и данные графа — namespace `DS` / `DS.ScriptableObjects`.
- Параметры героя и патрульного — ScriptableObject: `Assets/Settings/PlayerSettings.asset`, `PatrollerSettings.asset`.
