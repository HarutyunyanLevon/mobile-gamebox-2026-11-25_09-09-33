---
dreamer_task: 1
id: t01M8A0KAABVHW9C79PGC25J72J
version: v1
kind: build
cards:
- 01M89Z7M70P7X2XQVAQPFEFE8J
created: 2026-11-25T18:48:27Z
---

# Create the Hidden object module package, Addressables group and hub tile

## Goal
Create the local UPM package `com.gamebox.hiddenobject` with runtime, editor and test assembly definitions, its own Addressables group, and a module definition, so the existing hub shows a Hidden object tile. Tapping the tile opens an empty game scene, and the back button returns to the hub. This is the first card of the Hidden object build order. Build nothing beyond this.

## Context
- Read the repo's `CLAUDE.md` first. It holds the Ponytail rules (full level) and project conventions, and they apply to all work here.
- `docs/design/STATUS.md`: build status per card. Update it as described in Report back.
- `docs/design/QUESTIONS.md`: open design questions. Append to it as described in Report back.

Project rules that bind this task (confirmed by the user):
- Unity 6 LTS, URP, Android first, portrait. One app; the hub screen opens each demo.
- Each game, system and event is a local UPM package in `Packages/com.gamebox.*`, with its own assembly definition and Addressables group.
- Layers, top to bottom: App shell; Games and Live events; Shared systems (UI, Economy, Meta, Event framework, Cheats); Core. A layer uses only layers below it.
- Stack: uGUI for game UI; UI Toolkit for editor tools and cheats; VContainer (DI); UniTask; PrimeTween; Addressables; Newtonsoft JSON; Input System; TextMeshPro.
- No singletons or static state, and no god classes. VContainer injects everything. Keep methods and classes small. Write no commented-out code. Comments say why, not what.
- Use no magic numbers. Only 0, 1 and -1 may appear bare. Fixed facts are named constants. Tweakable values live in JSON, read with Newtonsoft, loaded through Addressables, and checked by `Validate()`.
- Sizes and positions live in prefabs. Shared looks use a base prefab and prefab variants. Code never sets layout numbers.
- Naming follows the Unity C# style guide (Unity 6 edition). KISS and YAGNI apply. DRY follows the rule of three.
- Logic with branches or math gets a unit test.

## What to build

### HO: module package, Addressables group, hub tile (01M89Z7M70P7X2XQVAQPFEFE8J)
**What it is.** The Hidden object game becomes a new module in the Games layer, delivered as the local package `Packages/com.gamebox.hiddenobject`.

**Package contents:**
- `package.json` for `com.gamebox.hiddenobject`, following the format of the existing `com.gamebox.*` packages.
- Three assembly definitions:
  - Runtime.
  - Editor (Editor platform only).
  - Tests (references the runtime assembly).
  Name them to match the existing packages.
- A dedicated Addressables group for this package. The group holds the package's game scene and any module assets.
- A module definition: whatever asset or registration the hub already uses to discover modules and show tiles. Use the same mechanism the existing modules use. The tile's title is "Hidden object".
- An empty game scene. It contains only what the existing module or scene convention requires to load and to return to the hub with the back button.

**Behaviour:**
- On launch, the hub shows a Hidden object tile among the other tiles. Hub code must not change for this to happen; the tile comes only from the new package's module definition.
- Tapping the tile loads the Hidden object scene through Addressables.
- The back button returns to the hub, using the same flow other modules use.

**Dependencies and layering:**
- The runtime assembly references only the Core, UI and Economy layers (plus the third-party stack listed above, as needed). It must not reference the App shell, other games, live events, Meta, the Event framework or Cheats.
- The project's layer test must pass with the new package included. If the layer test lists packages explicitly, register the new one there.

**Edge cases:**
- If the hub has no data-driven module discovery, and showing a tile would mean editing hub code, do not change the hub ad hoc. Log a question in `docs/design/QUESTIONS.md`, mark the card `blocked` or `partial` in STATUS.md, and stop at the conservative option.
- If no layer test exists yet, log a question. Do not invent a new layer framework. Add only the minimal check the existing test setup allows, and report it.
- Tile art, labels and layout come from the module definition and prefabs, not from code. No art exists for this card yet, so use the existing tile base prefab and placeholder look.

**What the context cards mean for this card:**
- This card starts the Hidden object build order. Later cards (level config, scene, pan and zoom, tap, find list, level flow, save, cheats, then hints, systems and windows) build on this package. Do not start any of them here.
- The next card adds level JSON files in `Content/Configs/Levels/`, plus a levels list and `Validate()`. Do not create level configs, a level schema or sample levels now. Shape the package so those can be added later without restructuring it, but add no placeholder code for them (YAGNI).
- Shared systems must never reference the Hidden object game. The dependency points only from this package down to the shared layers.

**Assets referenced:** none.

## Acceptance criteria
**01M89Z7M70P7X2XQVAQPFEFE8J: HO: module package, Addressables group, hub tile**
- [ ] `Packages/com.gamebox.hiddenobject` exists with a valid `package.json` and resolves in the Unity project without errors.
- [ ] The package has runtime, editor and test assembly definitions. The editor assembly is Editor-only, and the test assembly references the runtime assembly.
- [ ] A dedicated Addressables group for the package exists and contains the Hidden object game scene.
- [ ] A module definition for Hidden object exists in the package, using the hub's existing module mechanism.
- [ ] The hub shows a Hidden object tile, and the diff contains no change to hub code.
- [ ] Tapping the tile opens the empty Hidden object game scene.
- [ ] The back button in that scene returns to the hub.
- [ ] The runtime assembly references only the Core, UI and Economy layers among the project's own assemblies. The layer test passes.
- [ ] The new code has no singletons, no static state, no bare numbers other than 0, 1 and -1, and no layout numbers. Any services use VContainer.
- [ ] The project compiles for Android, and all existing tests still pass.

## Out of scope
- Level JSON schema, `Validate()` for levels, the levels list and sample gray-box levels (next card).
- Building the scene from a level config, pan and zoom, tap handling, the find list, level flow, save, cheats.
- Hints, rewards, tutorial, dialogs, achievements, and all windows (level start, win, reward, achievements, pause).
- Final art (the SpriteCook art pass comes after the gray-box playtest).
- Any change to hub code or to other modules.

## Report back
- **STATUS.md:** update `docs/design/STATUS.md`. Keep its first two lines unchanged. Add or update one section:
  ```
  ## 01M89Z7M70P7X2XQVAQPFEFE8J · HO: module package, Addressables group, hub tile
  status: in-progress|built|partial|blocked
  task: Create the Hidden object module package, Addressables group and hub tile
  commits: <hashes>
  files: <repo-relative paths>
  evidence: <test names and results, how the tile, the scene load and the back button were checked>
  notes: <one line>
  ```
- **QUESTIONS.md:** append each design question to `docs/design/QUESTIONS.md` as `- [ ] Q (01M89Z7M70P7X2XQVAQPFEFE8J): question — context`. Then take the most conservative option and continue. Do not invent design.
- **Commits:** commit once per logical step, for example package and asmdefs, Addressables group, module definition and scene, layer test. Start each message with `[dreamer:01M89Z7M70P7X2XQVAQPFEFE8J]`.
