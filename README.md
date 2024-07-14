# DREDGE Randomizer (W.I.P)

Randomizer mod for the game [DREDGE](https://store.steampowered.com/app/1562430/DREDGE/).

Massive credit to [InsomniacEeper](https://github.com/insomniac-eeper) for the bulk of the work in the initial implementation, initially available [here](https://github.com/SloopyNoSleep/DredgeWinchMods).

Please see the [disclaimer](#disclaimer).

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
- Randomize item rewards from quests
- Randomize HarvestPOI coordinates

## Disclaimer
While this mod _should not_ cause any problems with save data, **there may still be problems** so for your sake, before using this mod, **backup your save files** or play from new ones.

### How to backup
1. Locate your save files at `C:\Users\<Your username>\AppData\LocalLow\Black Salt Games\DREDGE\saves`
2. Create a folder somewhere else, and copy the save files we found previously to this new folder.
3. In the case of any problems, **disable any mods** and replace the files in the prior `saves` folder with those you copied in step 2.
