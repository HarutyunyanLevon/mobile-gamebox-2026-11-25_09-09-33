# Coding standards

Clean, small, readable code. A reviewer should understand any file in one read.

## Principles

- **KISS:** the simplest thing that works and reads clearly. Clear beats clever.
- **YAGNI:** build only what the current phase needs. No "just in case" code, options or layers.
- **DRY, by the rule of three:** the same code twice is fine; the third time it becomes shared.
- **Ponytail ladder (full level):** before writing code, stop at the first step that holds:
  1. Does this need to exist?
  2. Is it already in this codebase?
  3. Does the .NET standard library do it?
  4. Does Unity do it natively?
  5. Does an installed package (VContainer, UniTask, PrimeTween, Addressables, Newtonsoft) do it?
  6. Can it be one line?
  7. Only then: the minimum that works, plus a small test if it has logic.
  Input validation, error handling, data safety and tests are never cut.

## No magic numbers, no hardcode

- Every value someone might tweak lives in **JSON**: prices, timers, rewards, level data, event schedules, animation durations and easing, haptic patterns, volumes.
- Only `0`, `1` and `-1` may appear bare in code. Fixed facts become named constants (`const int MatchMinimum = 3;`).
- No hardcoded strings for ids, keys or paths: they come from JSON or a single constants class per module.
- **Layout lives in prefabs**, never in code. Shared looks use one base prefab and prefab variants (base window, base button), so a padding or font size changes in one place.
- When code must compute layout (for example a board's cell size), it uses JSON values plus the screen safe area.

## Configs

- All game data is JSON, read with **Newtonsoft JSON** (`com.unity.nuget.newtonsoft-json`). Saves use the same reader.
- Each module keeps its configs in its own `Content/Configs/` folder, loaded through Addressables.
- Every config is a plain C# class with a `Validate()` step run on load. A bad or missing value throws in the editor and logs plus falls back to safe defaults on device.
- Art is referenced by Addressables key in JSON, never by direct asset link in code.
- One editor test loads every JSON config in the project and fails on any parse or validation error.

## Style

- Naming follows Unity's C# style guide (Unity 6 edition): `PascalCase` types, methods and properties; `_camelCase` private fields; `camelCase` locals and parameters; interfaces start with `I`.
- One class per file; file name matches the class.
- A method does one thing; a class has one job. No god classes ("GameManager").
- Comments explain **why**, not what. No commented-out code.
- No singletons, no static mutable state. Everything is injected by VContainer.
- Async with UniTask; no coroutines in new code.
- Zero per-frame allocations in gameplay loops.

## Errors and tests

- Fail loudly in the editor (exceptions, asserts); fail safe on device (log, fall back, never crash the session).
- Any logic with branches, loops or math gets a small unit test.
- Never skip or disable a test to get green.

## Enforcement

- `.editorconfig` and Roslyn analyzers flag magic numbers, naming and unused code in the IDE.
- Every pull request is reviewed against this file.
- Phase 0: warnings are shown. After Phase 0: warnings fail the build.
- Pull requests are small: one feature each.
