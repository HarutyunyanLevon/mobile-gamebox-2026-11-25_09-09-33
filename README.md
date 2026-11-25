# Mobile Gamebox

A playable portfolio of modern mobile gaming, built in Unity: small demo games in the main casual genres, the live-ops systems that run them, live events, and the windows that tie it all together. One app, one hub, every piece a separate module.

> Status: Phase 0 (foundation) in progress.

## What's inside

| Area | First wave | Later |
| --- | --- | --- |
| Games | Hidden object, Merge-2, Match-3, Cooking | Tile match, sort puzzle, block puzzle, idle tycoon, bubble shooter, solitaire, word, survivor-like |
| Systems | Currencies, energy and lives, inventory, rewards, boosters, quests, achievements, dialogs, tutorial, cheats panel, haptics | Shop, progression map, analytics hooks, localization, notifications |
| Live events | Season pass, login calendar, win streak, lucky wheel, piggy bank | Race vs bots, milestone track, collection album, treasure dig, limited offer, happy hour |
| Windows | Hub, settings, level start, win, fail, out of lives, reward, inventory, quests, achievements, event intro and end, generic popups, loading | Shop, profile, rate us, privacy consent |

Every live event can be switched on or off at any time from config or the in-game cheats panel.

## Architecture

```
App shell      boot, hub, composition root
   |
Games  |  Live events      never reference each other
   |
Shared systems  UI · Economy · Meta · Event framework
   |
Core           save, DI, configs, time, sound, haptics, cheats, content loading
```

- Each layer may only use the layers below it. Unity assembly definitions enforce this.
- Every module is a local UPM package in `Packages/com.gamebox.*` with `Runtime/`, `Editor/`, `Tests/` and its own Addressables content group.
- Games and events talk through core signals (for example "level won"), never directly.
- Each game and event ships a module definition asset; the hub builds its tiles from them.

## Tech stack

Unity 6 LTS · URP · uGUI (game UI) · UI Toolkit (editor tools, cheats) · VContainer · UniTask · PrimeTween · Addressables · ScriptableObject configs · JSON saves · Input System · TextMeshPro

Android first, portrait only. iOS later.

## Roadmap

0. Foundation: repo, boot and hub, save, sound, haptics, window manager, cheats panel, currencies
1. Hidden object
2. Merge-2
3. Match-3
4. Cooking
5. Live events
6. Polish and second wave

Each game is built with placeholder shapes first, then skinned with AI-generated art in one shared style.

## Built with an AI game dev team

This project is built together with Claude and its Unity skills, and improves those skills along the way: gaps are logged in [`SKILL_GAPS.md`](SKILL_GAPS.md) and turned into new or improved skills after each game.
