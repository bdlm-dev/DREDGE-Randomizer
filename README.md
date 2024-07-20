# DREDGE Randomizer (W.I.P)
[![Create Release](https://github.com/bdlm-dev/DREDGE-Randomizer/actions/workflows/release.yml/badge.svg)](https://github.com/bdlm-dev/DREDGE-Randomizer/actions/workflows/release.yml)
![GitHub License](https://img.shields.io/github/license/bdlm-dev/DREDGE-Randomizer)

![GitHub Downloads (all assets, all releases)](https://img.shields.io/github/downloads/bdlm-dev/DREDGE-Randomizer/total)

Randomizer mod for the game [DREDGE](https://store.steampowered.com/app/1562430/DREDGE/).

Massive credit to [InsomniacEeper](https://github.com/insomniac-eeper) for the bulk of the work in the initial implementation, initially available [here](https://github.com/SloopyNoSleep/DredgeWinchMods).

Please see the [disclaimer](#disclaimer).

## Usage

By default, the mod will create save data for each save slot, which is inside `save.json` in the mod folder `/mods/mmbluey.randomizer/`. The seed used for randomization is randomized when you create a new save file, but if you wish to change the seed without resetting progress, the `seed` attribute can be altered in `save.json` for the desired slot (index beginning 0). If you wish to use the same seed across all runs and resets, you can set `UseConfigSeed` to `true` in the config. Specifics of randomization can be set in the config, such as whether fishing size, harvest type, difficulty, and more will be randomized.

## Config
* Seed
   * Seed used for RNG: can be used to create repeatable experiences
* UseConfigSeed (Default false)
   * Override per-save seed with seed set in config.
* RandomizeSizes
   * Randomize Fish sizes.
* RandomizeHarvestMinigamesTypes
   * Randomize minigame associated to fish.
* RandomizeHarvestableType
   * Randomize HarvestableType (Volcanic, Mangrove, Oceanic) etc for each fish.
* RandomizeDifficulty
   * Randomize difficulty for all fish.

## Features
### Implemented
- Fish randomization
  - Random fish size ranges
  - Random fish catch minigame
  - Random HarvestableType (Volcanic, Mangrove, Oceanic, etc.,)
  - Random difficulty
  - Random location (Not randomly generated coordinates, is the same set of locations as base game but with different fish)

### Planned
- Randomize fish required for quests (will need to be careful of the exotic fish)
- Include crabs in randomization: Crabs can randomize to be fishable from HarvestPOIs, and fish can randomize to be caught via crab pots.
- Randomize item rewards from quests
- Randomize HarvestPOI coordinates

## Disclaimer
While this mod _should not_ cause any problems with save data, **there may still be problems** so for your sake, before using this mod, **backup your save files** or play from new ones.

### How to backup
1. Locate your save files at `C:\Users\<Your username>\AppData\LocalLow\Black Salt Games\DREDGE\saves`
2. Create a folder somewhere else, and copy the save files we found previously to this new folder.
3. In the case of any problems, **disable any mods** and replace the files in the prior `saves` folder with those you copied in step 2.
