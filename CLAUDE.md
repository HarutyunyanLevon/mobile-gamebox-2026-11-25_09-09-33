# Mobile Gamebox: instructions for Claude

Portfolio app of small mobile demo games, live-ops systems, live events and windows. Owner: Levon (mobile live-ops game developer). Explain technical choices in plain words and offer numbered choices.

## Architecture rules (do not break)

- One Unity 6 LTS project, URP, Android first, portrait only.
- Every module is a local package in `Packages/com.gamebox.<area>[.<name>]/` with `package.json`, `Runtime/`, `Editor/`, `Tests/` and an assembly definition per folder.
- Layers, top to bottom: App shell (`Assets/_App`) -> Games (`com.gamebox.game.*`) and Live events (`com.gamebox.event.*`) -> Shared systems (`ui`, `economy`, `meta`, `liveops`, `cheats`) -> `core`.
  A module may only reference modules in layers below it. Games never reference other games or events; events never reference games. They communicate through signals defined in core.
- Assembly names: `Gamebox.<Area>[.<Name>]`, editor `...Editor`, tests `...Tests`. Root namespace matches the assembly name.
- Each game and event has a module definition ScriptableObject (id, title, icon, entry scene address, enabled flag). The hub builds tiles from these; never hard-code a module in the hub.
- Each module has its own Addressables group and a standalone test scene.
- Dependency injection via VContainer; async via UniTask (no coroutines for new code); tweens via PrimeTween.
- Game UI uses uGUI; UI Toolkit only for editor tools and the cheats panel.
- All game data and tuning is JSON (Newtonsoft JSON), validated on load; saves are JSON behind `ISaveService`. No ScriptableObject configs.
- Layout lives in prefabs and prefab variants; code never sets layout numbers.
- Zero per-frame allocations in gameplay loops.

## Clean code (read `Docs/CODING_STANDARDS.md` before writing code)

- KISS, YAGNI, DRY by the rule of three.
- Ponytail ladder at full level: does it need to exist, is it already here, stdlib, Unity, installed package, one line, then the minimum that works plus a test.
- No magic numbers: only 0, 1, -1 bare; tweakable values in JSON; fixed facts as named constants; no hardcoded ids, keys or paths.
- Naming per Unity's C# style guide (Unity 6 edition). No singletons or static mutable state.
- Comments say why, not what. Small pull requests, one feature each.

## Workflow

- Build each game with placeholder shapes first; skin later with SpriteCook art using the shared gamebox style preset.
- When a Unity skill gives wrong or missing advice, add a row to `SKILL_GAPS.md`.
- Never skip or disable tests to get green.
