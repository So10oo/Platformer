# Инвентарь

Черновик сетки предметов. Живёт в `Assets/InventorySystem/`, **не** биндится в `Installer` и не связан с героем, оружием или лутом на уровне.

Namespace: `Inventory`.

## Модель

| Тип | Роль |
|-----|------|
| `ItemData` | ScriptableObject: Id, Icon, CapacitySlot |
| `InventorySlot` / `InventorySlotData` | Ячейка: предмет + количество |
| `InventoryGrid` / `InventoryGridData` | Словарь слотов `Vector2Int` → слот, размер сетки |
| `AddItemsToInventoryGridResult` / `RemoveItems...` | Сколько добавили / удалось ли снять |

`InventoryGrid`:

- `AddItems(item, amount)` — сначала в стопки того же предмета, потом в пустые слоты, пока не упрётся в `CapacitySlot`.
- `RemoveItems` — снимает по Id.
- `GetAmount` / `Has`.
- События `ItemsAdded` / `ItemsRemoved`.
- `SwitchSlots` закомментирован.

## UI и вход

- `InventoryGridController` / `InventorySlotController` / `InventorySlotView` — слоты на сцене.
- `DragUIItem`, `DropUISlot`, `PutOnSlotController` — drag-and-drop UI.
- `EntryPoint` — тестовый бутстрап: сетка 4×4, `Initialize` контроллера. В `Update`: зажатая **A** кладёт случайный `ItemData` из массива, **R** пытается снять.

Инвентарь рассчитан на отдельную UI-сцену/объект с `EntryPoint`, не на геймплейный цикл героя.
