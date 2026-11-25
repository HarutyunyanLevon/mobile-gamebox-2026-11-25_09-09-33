# Questions

- [x] Q (01M89C87X6CRQKY7Y036A6Z111): Card says "no scripts in packages" but also "Test Runner lists the four test assemblies"; Unity skips asmdefs with no .cs — context: owner chose one own-package layering test per Tests/Editor; options: keep tests / empty asmdefs; default used: keep tests
  - A: No scripts exist in any package
- [x] Q (01M89C87X6CRQKY7Y036A6Z111): Template scene URP2DSceneTemplate.unity, its Lit2DSceneTemplate.scenetemplate and Assets/Welcome were removed so SampleScene is the only scene — context: owner's call in chat; confirm the card should say so
  - A: sample scene should be only scene
- [x] Q (01M89C87X6CRQKY7Y036A6Z111): Package com.unity.learn.iet-framework 6.0.7 is now unused (only Welcome used it) — options: remove from manifest / keep; default used: keep (out of scope)
  - A: remove it
- [x] Q (01M89C87X6CRQKY7Y036A6Z111): ProjectSettings has uncommitted changes not made by this task (scripting define SENTIS_ANALYTICS_ENABLED -> APP_UI_EDITOR_ONLY, version control mode Visible Meta Files -> Unity Version Control) — options: commit / revert; default used: left uncommitted
  - A: commit them
- [x] Q (01M89Z7M70P7X2XQVAQPFEFE8J): No hub exists yet (no Assets/_App, no hub scene or code, no module definition type in core), so the tile, tap-to-load and back button cannot be built without inventing the hub — options: add a hub + module definition card first / let this card create them; default used: not built, card partial
  - A: add hub + module card first
- [x] Q (01M89Z7M70P7X2XQVAQPFEFE8J): Addressables, VContainer, UniTask, PrimeTween and Newtonsoft are not in Packages/manifest.json, so no Addressables group can be made — options: add a stack-install card / install them in this card; default used: not installed, no group, no scene
  - A: add a stack-install card
