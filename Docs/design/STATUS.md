# Build status
<!-- dreamer:status v1 -->

## 01M89C87X6CRQKY7Y036A6Z111 · Local UPM package skeletons with layered assemblies
status: built
task: Rebuild local UPM package skeletons with layered assemblies
commits: 3601d15, 432bf82, 613d0b8, a112f8b, 3d31514, c494f8f
files: Packages/com.gamebox.{core,ui,economy,cheats}/Tests/Editor/<X>LayeringTests.cs (+.meta); removed Assets/Welcome/**, Assets/Settings/Scenes/URP2DSceneTemplate.unity, Assets/Settings/Lit2DSceneTemplate.scenetemplate; verified package.json, README.md, Runtime/Editor/Tests asmdefs of all four packages, ProjectSettings/ProjectVersion.txt; .claude/skills/{dreamer-bridge,testable-by-design,asmdef-layering-check}/SKILL.md
evidence: editor 6000.6.10f1; batch EditMode run 0 compile errors, 4/4 passed in Gamebox.Core.Tests, Gamebox.UI.Tests, Gamebox.Economy.Tests, Gamebox.Cheats.Tests (re-run after probe also 4/4); asmdef refs: Core family -> none, UI/Economy/Cheats Runtime -> Gamebox.Core, Editor/Tests -> own + Gamebox.Core (+TestRunner, nunit, UNITY_INCLUDE_TESTS, Editor-only), checked by the new layering tests; probe UI->Economy failed: "LayeringProbe.cs(5,24): error CS0234 ... 'Economy' does not exist in the namespace 'Gamebox'", probe removed; only scene Assets/Scenes/SampleScene.unity; only .cs in packages are the 4 tests; no AddressableAssetsData
notes: design diff needed no package change; added one layering test per test asmdef (empty asmdefs don't compile) and removed template scene/Welcome assets on owner's call; follow-up: removed unused iet-framework and collab-proxy, added com.unity.pipeline, committed App UI define, layering tests now also prove they reject GUID and sibling refs (batch EditMode 12/12 passed, 0 compile errors)
