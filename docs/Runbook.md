# Запуск и сборка

## Требования

- Unity Hub
- Unity Editor **2022.3.62f1** (как в `ProjectSettings/ProjectVersion.txt`)

## Play

1. Открыть папку `Platformer/` (не корень git, если открываешь именно Unity-проект).
2. Сцена `Assets/Scenes/Game.unity`.
3. На сцене должен быть Zenject `SceneContext` + `Installer` с заполненными ссылками: StartPoint, HeroPrefab, HealthPointView, DialogPanel.

## Сборка

File → Build Settings: в списке сцен есть `Game.unity` → платформа → Build.

## Типичные поломки

**Не та версия Unity.** Ставь ту, что в `ProjectVersion.txt`.

**Сломался ввод.** `InputService.cs` генерируется из `InputService.inputactions`. Не править `.cs` руками: reimport ассета или regenerate в окне Input System.

**Сцена не стартует.** Нет инсталлера, пустые ссылки, или на сцене одновременно `Installer` и `PlayerInstaller` с одними и теми же биндингами.

**Диалог не открывается.** На NPC нужен `DSInspectorInitialDialogue` с выбранным `FirstDialogue` и `Conversation`. `DialogPanel` должен быть забинжен в инсталлере.

**Dash не работает.** Способность появляется только после `AltarDash`. Пока `character["dash"] == null`, Shift игнорируется.

IMGUI состояния героя рисуется всегда в Play. Для билдов имеет смысл обернуть в `UNITY_EDITOR` / `DEVELOPMENT_BUILD`.
