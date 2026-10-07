# Диалоговая система

Редактор графа (Editor + Odin) и рантайм (ScriptableObject + UI). Граф и реплики пишутся **в один ассет-контейнер** (sub-assets), на NPC вешается стартовая реплика.

## Поток

```
Window/DS/Dialogue Graph  →  Save / Save As
        │
        └── <Name>DialogueContainer.asset
              ├── DSDialogueContainerSO          (группы, ungrouped, персонажи)
              ├── DSGraphSaveDataSO              (раскладка графа, nested)
              ├── CharacterDataSO × N            (nested)
              └── DSDialogueSO × N               (nested)
        │
NPC: DSInspectorInitialDialogue.FirstDialogue
        │
Игрок в зоне + F  →  Conversation.Interaction()
        │
DialogPanel.StartDialog  →  DialogView (текст, имя, иконка, кнопки)
        │
клик Choices  →  NextDialogue  →  null → EndDialog()
```

## Редактор

Меню: **Window → DS → Dialogue Graph**. Код: `Assets/DialogueSystem/Editor/`.

- `DSEditorWindow` + `DSGraphView`: ноды single/multiple choice, группы, blackboard персонажей (`DSCharacterBlackboard` / `CharacterField`: имя + иконка Texture).
- Toolbar: имя файла, **Save**, **Save As**, Load, Clear, Reset, Minimap.
- Save/Save As блокируются `ErrorController`, пока в графе ошибки имён.
- `SaveLoadService` кладёт контейнер по выбранному пути; граф, персонажи и реплики — `AddObjectToAsset` внутрь контейнера.
- С контейнера кнопка Odin `OpenGraph` снова открывает окно и грузит nested `DSGraphSaveDataSO`.

Namespace редактора: `DialogueSystem.Editor`. Рантайм-данные: `DialogueSystem.Realtime`.

## Данные рантайма

| Тип | Роль |
|-----|------|
| `DSDialogueContainerSO` | SerializedScriptableObject: группы → списки реплик, ungrouped, список `CharacterDataSO` |
| `DSDialogueGroupSO` | Имя группы |
| `DSDialogueSO` | Реплика: имя, текст, choices, стартовый флаг, **персонаж** |
| `DSDialogueChoiceData` | Текст кнопки + `NextDialogue` |
| `CharacterDataSO` | Имя и `Texture2D` иконки |

Отдельного enum `DSDialogueType` в рантайм-SO больше нет: ветвление задаётся числом choices на ноде.

На объекте в мире: `DSInspectorInitialDialogue` (Odin ValueDropdown по контейнеру, фильтры grouped / starting). Игровой код читает `FirstDialogue`.

## Рантайм UI

| Класс | Роль |
|-------|------|
| `Conversation` | Interactive: подсказка «Interactive», по F стартует диалог |
| `DialogPanel` | Включает панель, гоняет цепочку, на `null` выключает. `[Inject]` есть |
| `DialogView` | Текст, имя персонажа, спрайт из `Character.Icon`, спавн кнопок |
| `Choices` | Кнопка варианта |

Префаб кнопки: `Prefabs/UI/ButtonChoices.prefab`. Панель биндится в `Installer`.

`StartDialog` по-прежнему не переводит героя в отдельное состояние (комментарий в коде). Ввод геймплея не глушится: можно ходить во время реплик.

Цепочка кончается, когда `NextDialogue == null`.
