# Runbook (run / build / troubleshoot)

## Requirements

- Unity Hub
- Unity Editor: **2022.3.62f1**

## Open and run (Editor)

- Open project folder: `E:/GitHub/Platformer/Platformer`
- Open scene: `Assets/Scenes/Game.unity`
- Press **Play**

## Build

- Unity: **File → Build Settings**
- Ensure scene list contains `Assets/Scenes/Game.unity`
- Choose platform → **Build**

## Common issues

### Wrong Unity version

Symptoms: import errors, broken packages, unexpected compilation issues.

Fix: install the version from `Platformer/ProjectSettings/ProjectVersion.txt` and reopen the project.

### Input actions broken

`InputService/InputService.cs` is auto-generated from `Assets/Scripts/InputService/InputService.inputactions`.

Fix:
- Reimport the `.inputactions` asset
- Or regenerate via Input System editor UI
- Avoid manual edits in the generated `.cs` file (they will be overwritten)

### Scene does not start correctly

Verify:
- Scene is `Assets/Scenes/Game.unity`
- A Zenject installer exists in the scene and has references assigned (hero prefab, start point, UI views)

## Debug builds

If the project has runtime debug UI (e.g., `OnGUI` state display), prefer enabling it only in:
- `UNITY_EDITOR` or `DEVELOPMENT_BUILD`
