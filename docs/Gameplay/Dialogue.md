# Диалоговая система

Две половины: **редактор графа** (Editor) и **рантайм** (ScriptableObject + UI). Данные диалога не хранятся в сцене — граф сохраняется в ассеты, на NPC вешается ссылка на стартовую реплику.

## Поток

```
Window/DS/Dialogue Graph  →  Save
        │
        ├── Editor-граф:  Assets/Editor/DialogueSystem/Graphs/<Name>Graph.asset
        └── Игровые SO:   Assets/DialogueSystem/Dialogues/<Name>/
                              ├── <Name>.asset          (контейнер)
                              ├── Groups/<Group>/...
                              └── Global/Dialogues/*.asset
        │
NPC: DSDialogue (выбирает контейнер/группу/стартовый DSDialogueSO)
        │
Игрок в триггере + F  →  Conversation.Interaction()
        │
DialogPanel.StartDialog(first)  →  DialogView рисует текст и кнопки
        │
клик Choices  →  NextDialogue  →  пока NextDialogue != null
        │
null  →  EndDialog()  (панель выключается)
```

## Редактор

Меню: **Window → DS → Dialogue Graph**.

`DSEditorWindow` + `DSGraphView` (Unity GraphView):

- Ноды: single choice (`DSSingleChoiceNode`) и multiple choice (`DSMultipleChoiceNode`).
- Группы (`DSGroup`) — папки внутри контейнера.
- Toolbar: имя файла, Save / Load / Clear / Reset, minimap.
- Save блокируется, если у нод пустые/битые имена (`NameErrorsAmount`).
- Load берёт `.asset` из `Assets/Editor/DialogueSystem/Graphs`.

Типы реплик (`DSDialogueType`): `SingleChoice`, `MultipleChoice`.

Каждая нода: имя, текст, список выборов `{ Text, NextDialogue }`, флаг стартовой реплики.

## Данные рантайма

| Тип | Роль |
|-----|------|
| `DSDialogueContainerSO` | Файл диалога: группы → списки реплик, плюс ungrouped |
| `DSDialogueGroupSO` | Имя группы |
| `DSDialogueSO` | Одна реплика: текст, choices, тип, `IsStartingDialogue` |
| `DSDialogueChoiceData` | Текст кнопки + ссылка на следующую `DSDialogueSO` |

На объекте в мире: `DSDialogue`. Кастомный инспектор `DSInspector` даёт выпадающие списки контейнера / группы / реплики (фильтры «только grouped» и «только starting»). Игровой код читает только `FirstDialogue`.

## Рантайм UI

| Класс | Роль |
|-------|------|
| `Conversation` | Interactive: в зоне пишет «Interactive», по F стартует диалог |
| `DialogPanel` | Включает панель, гоняет цепочку реплик, на `null` выключает |
| `DialogView` | Текст в TMP + спавн кнопок в контейнер |
| `Choices` | Кнопка варианта, по клику вызывает callback со следующим SO |

Префаб кнопки: `Prefabs/UI/ButtonChoices.prefab`. Панель биндится в `Installer` со сцены.

`DialogPanel.Construct(Character, StateMachineEvents<Character>)` задуман как инъекция, но атрибута `[Inject]` нет — персонаж в диалог не переводится (комментарий в `StartDialog` об этом). Вход не глушит `InputService`, герой может ходить во время разговора.

Цепочка кончается, когда у выбора `NextDialogue == null` (в тестовом графе так сделаны реплики 3 и 4).

## Тестовый граф

`Assets/DialogueSystem/Dialogues/DialoguesFileName/`:

1. «Привет!» (starting, single) → 2
2. «Как дела?» (multiple) → «норм» → 3, «не норм» → 4
3. «пон» → конец
4. «хорош» → конец
