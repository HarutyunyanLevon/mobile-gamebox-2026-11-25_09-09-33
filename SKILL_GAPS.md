# Skill gap log

Every time a Unity skill gives wrong, outdated or missing advice while building the gamebox, add one row. After each game, the retro turns these rows into skill fixes or new skills.

| Date | Skill | Phase / module | What happened | What would have been right | Status |
| --- | --- | --- | --- | --- | --- |
| 2026-11-25 | testable-by-design | Phase 0 / package skeletons | No warning that Unity skips asmdefs without a .cs file, so empty test assemblies never show in the Test Runner | Give each test assembly one real test from day one | Fixed in skill |
| 2026-11-25 | dreamer-bridge | Phase 0 / package skeletons | Knew only design packs; no task files, link.json, STATUS.md format or outbox report cards | "Running a Dreamer task" section | Fixed in skill |
| 2026-11-25 | (none) | Phase 0 / package skeletons | No skill to check asmdef references against the layer rule and prove it with a compile probe | New asmdef-layering-check skill | Added |
| 2026-11-25 | asmdef-layering-check | Phase 0 / package skeletons | Suggested layering tests had no negative case, so the GUID-rejection branch never ran and a broken check would pass | Each layering test also feeds forbidden references and expects rejection | Fixed in skill |
| 2026-11-25 | (none) | Phase 0 / package skeletons | ProjectSettings drift (define swap, version-control mode flipped by collab-proxy) sat uncommitted with no way to tell its cause | New settings-drift-check skill, run before each commit | Added |
| 2026-11-25 | unity:unity-cli | Phase 0 / package skeletons | Open Editor blocked batch tests twice; project had no Pipeline package, so the open Editor couldn't be driven either | Add com.unity.pipeline at project setup | Fixed in project |
| 2026-11-25 | unity-cli (playmode-verification-loop.md) | Phase 1 / hidden object hub | Revert examples use `-- --enable false` for `editor set_auto_tick` and `editor.playmode set_run_in_background`; Pipeline 1.0.1 rejects it (exit 2) | `-- --enabled false` | Open |
