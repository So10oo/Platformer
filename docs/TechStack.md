# Tech stack snapshot

## Engine

- Unity **2022.3.62f1 (LTS)**

## Unity packages (from `Platformer/Packages/manifest.json`)

Not exhaustive in this document; treat `manifest.json` as source of truth.

- Input: `com.unity.inputsystem`
- Rendering: `com.unity.render-pipelines.universal`
- 2D toolchain: animation/spriteshape/tilemap/pixel-perfect
- Camera: `com.unity.cinemachine`
- UI/Text: `com.unity.ugui`, `com.unity.textmeshpro`
- Testing: `com.unity.test-framework`

## Plugins / third-party

Located mostly under `Platformer/Assets/Plugins/`:

- Zenject (dependency injection)
- TextMesh Pro (usually as package, but often includes assets)
- Odin Inspector (editor tooling)
- Art packs (Cainos pixel-art packs, etc.)
