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

<!-- unity-gamedev-team:start -->
# Unity Game Dev Team

This project uses a team of game-development agents, skills and commands in `.claude/`.
Target: **Unity**, mobile (Android + iOS) by default, or PC/Steam: see **Platform** below. Copy this block into the game project's own `CLAUDE.md`.

## How the team works

- **The producer drives.** `/build-game` hands the project to the `game-producer` agent, which works one stage at a time and calls specialists.
- **One stage at a time.** The project's current stage lives in `docs/gamedev/STAGE.md`. The `stage-ladder` skill defines the stages and what each one requires. Never do work that belongs to a later stage while the current stage's exit criteria are still unmet.
- **Cheap checks often, one gate per stage.** During a stage, run small focused checks (a skill on one area). At the end of a stage, run one combined `game-review`. Before shipping anything to players, run `release-gate`.
- **Evidence over opinion.** Every finding cites something real: a file and line, a profiler capture, a screenshot, a playtest note, a metric. "Feels slow" is a hypothesis until it is measured.
- **Find the cause before changing code.** Reproduce → isolate → fix → verify. No speculative rewrites.

## Project files the team keeps

```
docs/gamedev/
  STAGE.md          current stage, why, exit criteria status
  CONCEPT.md        one-page concept: fantasy, core verb, core loop, audience, monetization model
  DECISIONS.md      dated log of decisions and the reason for each
  TUNING.md         every tunable number, where it lives, current value, why
  CHEATS.md         every debug cheat, what it does, which game API it uses
  reviews/          game-review and release-gate reports (YYYY-MM-DD-<stage>.md)
  reports/          focused skill reports (YYYY-MM-DD-<skill>.md)
  playtests/        raw playtest notes and triage output
```

Create missing files on first use. Never delete reports; supersede them with newer ones.

## Unity conventions

- Never move, rename or delete assets outside the Unity Editor or without moving the `.meta` file with them. Use the `unity-safe-reorganization` skill.
- Gameplay numbers live in JSON configs (or remote config), never hard-coded in MonoBehaviours. No ScriptableObject configs.
- Keep third-party SDKs (ads, IAP, analytics, remote config) behind game-owned interfaces so they can be swapped and mocked in tests.
- Every system ships with debug cheats (`debug-cheats` skill), compiled out of release builds. A system without cheats isn't done.
- Budgets: see the Platform table (override in `TUNING.md`). On every platform: 0 B GC allocations per frame in gameplay.
- If Unity plugin skills are installed (`unity:*`, `unity-perf:*`), use them for engine-level work and keep these skills for the process around it.
- Keep `com.unity.pipeline` in the project so agents can drive the open Editor (`unity:unity-cli`: tests, recompile). Use batch mode only when the Editor is closed.

## Platform

The game project's `CLAUDE.md` names its platform on a line **outside** this block: `Platform: mobile` or `Platform: pc-steam`. If the line is missing, the platform is `mobile`. Every agent and skill reads it first. Wherever a team file talks about phones, touch, devices or app stores, apply the matching row here instead.

| | `mobile` (default) | `pc-steam` |
|---|---|---|
| Input | Touch first, safe areas, thumb reach | Mouse and keyboard first, gamepad optional, rebindable keys; no safe-area logic |
| Reference hardware | Low-end Android (2–3 GB), mid-tier Android, an older supported iPhone | Current Steam Hardware Survey median (check the survey) plus one minimum-spec PC |
| Budgets | 60 fps on mid-tier, stable 30 on low-end; first interactive frame < 5 s; install < 150 MB | 60 fps minimum on the median PC unless the project sets more; first interactive frame < 10 s; no install cap unless set |
| Profiling | Development build on the device over USB | Development player build on the PC, not the Editor |
| UI and text | Text ≥ 12 pt on the smallest phone; tap targets ≥ 44 pt | Readable at 1080p from 60 cm and scales to 1440p/4K; UI scale setting; hover and focus states |
| Builds | AAB + IPA via `mobile-release-engineer` | Windows x64 IL2CPP player and Steam depots (SteamPipe). `mobile-release-engineer` keeps its pipeline rules (reproducible builds, versioning, symbols, secrets) and uses Steamworks in place of the phone stores |
| Store skills | `store-readiness`, `store-art-aso`, `mobile-device-check`, `ads-mediation-review` | `store-readiness` and `store-art-aso` apply to the Steam page (capsules, screenshots, trailer, tags; check current Steamworks docs). Skip `mobile-device-check` and `ads-mediation-review` unless the owner asks |
| Release checks | `release-gate` as written | `release-gate`: swap the Android/iOS build-identity items for Windows build and Steam depot items; drop ATT/UMP unless the game has ads |

## Who to call

| Problem | Call |
|---|---|
| Build or continue the game | `/build-game` → `game-producer` |
| Design comes from the Dreamer app | `dreamer-bridge` skill |
| Whole-game review | `/review-game` → `game-review` skill |
| Feature code, scenes, prefabs | `unity-developer` |
| Mechanics, loops, economy, difficulty | `game-systems-designer` |
| HUD, menus, onboarding, store screens | `game-ui-ux-designer` |
| Flat, weightless, unresponsive feel | `game-feel-designer` |
| Visual direction, "looks AI-made" | `game-art-director` |
| Frame drops, heat, memory, load time | `game-performance-engineer` |
| Bugs, playtests, regressions | `game-qa-analyst` |
| Reach any game state fast (cheat menu) | `/add-cheats` → `debug-cheats` skill |
| IAP, ads, pricing | `game-monetization-manager` |
| Remote config, flags, analytics, A/B | `game-config-analytics-engineer` |
| Store builds and submission | `mobile-release-engineer` |
| Package or asmdef added or changed | `asmdef-layering-check` skill |
| Before any commit; settings you didn't touch | `settings-drift-check` skill |
<!-- unity-gamedev-team:end -->

