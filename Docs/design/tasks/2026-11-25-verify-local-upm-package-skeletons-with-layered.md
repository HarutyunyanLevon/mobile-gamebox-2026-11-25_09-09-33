---
dreamer_task: 1
id: t01M89J4XP523TGC2FCVBAVZ7BM
version: v1
kind: build
cards:
- 01M89C87X6CRQKY7Y036A6Z111
created: 2026-11-25T14:35:55Z
---

## Goal

Verify, and correct where needed, the four embedded local UPM package skeletons (Core, UI, Economy, Cheats) so that they match the card exactly and the layering rule holds at compile time. The card is recorded as built; this task confirms that state against the repository, fixes any drift, and records evidence. It adds no scripts, services, configs or content.

## Context

- `CLAUDE.md` (repo root): project rules, including the Ponytail clean-code rules. Follow them.
- `docs/design/STATUS.md`: build status per card. Update it as described under Report back.

Project decisions that apply (from the user's confirmed project memory):
- Unity 6 LTS, URP, Android first, portrait. One app; a hub screen opens each demo.
- Every game, system and event is a local UPM package in `Packages/com.gamebox.*` with its own assembly definition and, later, its own Addressables group.
- Layers, top to bottom: App shell; Games and Live events; Shared systems (UI, Economy, Meta, Event framework, Cheats); Core. A layer only uses layers below it. Games and events never reference each other.
- Naming follows the Unity C# style guide (Unity 6 edition). No singletons or static state, no god classes, no commented-out code, no magic numbers.
- KISS and YAGNI: do not add anything the card does not ask for.

What the context cards mean for this work:
- The architecture card is the rule these packages enforce: module assemblies may reference only assemblies in lower layers, which for this skeleton means only `Gamebox.Core`.
- The Phase 0 boot scene and hub card depends on these packages. It is not part of this task; keep the skeletons free of hub, boot scene or module-definition code so that card can be built on top of them later.

## What to build

### Local UPM package skeletons with layered assemblies (01M89C87X6CRQKY7Y036A6Z111)

What it is: the Unity 6 project (editor `6000.6.10f1`) with four embedded local packages under `Packages/`:

| Package folder | Assembly root name | May reference |
|---|---|---|
| `com.gamebox.core` | `Gamebox.Core` | nothing (bottom layer) |
| `com.gamebox.ui` | `Gamebox.UI` | `Gamebox.Core` only |
| `com.gamebox.economy` | `Gamebox.Economy` | `Gamebox.Core` only |
| `com.gamebox.cheats` | `Gamebox.Cheats` | `Gamebox.Core` only |

Each package contains:
- `package.json` with version `0.1.0`.
- A README.
- Three assembly definitions: `Runtime`, `Editor` and `Tests/Editor`.
  - The `Tests/Editor` assembly is editor-only, uses NUnit, and is behind the `UNITY_INCLUDE_TESTS` define constraint.
  - The `Editor` assembly is editor-only.

Behaviour: because each module assembly references only Core, a reference from one module to another (for example UI to Economy) or from Core to any module fails to compile. That is the layering enforcement.

Current state to preserve: no scripts, services, configs or content (Addressables) groups exist in any package. The only scene is the template `SampleScene`.

Steps:
1. Inspect each of the four packages and confirm the items above. Correct any deviation (missing file, wrong version, wrong reference, missing define constraint, test assembly not editor-only).
2. Confirm that the Runtime assemblies of UI, Economy and Cheats reference only `Gamebox.Core`, that each package's Editor and Tests/Editor assemblies reference only their own package's assemblies plus `Gamebox.Core` (and the test runner/NUnit for tests), and that Core references no `Gamebox.*` assembly.
3. Confirm the project compiles with no errors and that the Test Runner lists the four editor test assemblies (empty is acceptable).
4. Do not add scripts, placeholder classes, scenes, configs or Addressables groups.

Edge cases:
- If a package contains extra scripts or assets beyond the skeleton, do not delete them silently; record them in STATUS.md notes and raise a question.
- If the exact assembly names for the `Editor` and `Tests/Editor` assemblies are not specified by the card (for example `Gamebox.Core.Editor`, `Gamebox.Core.Tests.Editor`), keep the names already in the repo as long as they follow the Unity C# naming style; do not rename for preference.
- If the installed editor version differs from `6000.6.10f1`, do not upgrade or downgrade; record the version found.

Assets referenced: none.

## Acceptance criteria

Local UPM package skeletons with layered assemblies (01M89C87X6CRQKY7Y036A6Z111):
- `Packages/com.gamebox.core`, `Packages/com.gamebox.ui`, `Packages/com.gamebox.economy` and `Packages/com.gamebox.cheats` exist as embedded packages and load in the Unity Package Manager.
- Each has a `package.json` with `"version": "0.1.0"` and a README.
- Each has exactly three assembly definitions: `Runtime`, `Editor`, `Tests/Editor`.
- `Gamebox.Core` runtime assembly has no reference to any other `Gamebox.*` assembly.
- `Gamebox.UI`, `Gamebox.Economy` and `Gamebox.Cheats` runtime assemblies reference `Gamebox.Core` and no other `Gamebox.*` assembly.
- Every `Tests/Editor` assembly is editor-only, references NUnit/test runner, and has the `UNITY_INCLUDE_TESTS` define constraint.
- Every `Editor` assembly is editor-only.
- The project compiles with zero errors and the Test Runner (Edit Mode) lists the four test assemblies.
- No scripts, services, configs or Addressables groups exist in the packages; `SampleScene` is the only scene.

## Out of scope

- Packages for Meta, Event framework, any game or any live event.
- The boot scene, VContainer composition root, hub screen and module tiles (separate card).
- Addressables groups, tuning JSON, prefabs, scenes and any runtime or editor code.
- Installing or configuring the rest of the stack (VContainer, UniTask, PrimeTween, Addressables, Newtonsoft JSON, Input System, TextMeshPro) beyond what already exists.
- Changing the Unity editor version, render pipeline or player settings.

## Report back

- Update `docs/design/STATUS.md`. Keep its first two lines unchanged. Add or replace one section:
  ```
  ## 01M89C87X6CRQKY7Y036A6Z111 · Local UPM package skeletons with layered assemblies
  status: in-progress|built|partial|blocked
  task: Verify local UPM package skeletons with layered assemblies
  commits: <hashes, or none if no change was needed>
  files: <repo-relative paths touched or verified, e.g. Packages/com.gamebox.core/package.json>
  evidence: <compile result, Test Runner assembly list, reference check per assembly>
  notes: <one line: deviations found and fixed, or none>
  ```
- Append design questions to `docs/design/QUESTIONS.md` (create it if absent) as `- [ ] Q (01M89C87X6CRQKY7Y036A6Z111): question — context`. Take the most conservative option and continue.
- Commit once per logical step; start each commit message with `[dreamer:01M89C87X6CRQKY7Y036A6Z111]`.
- Do not invent design.
