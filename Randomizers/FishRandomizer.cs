﻿using Randomizer.Config;
using Winch.Core;

namespace Randomizer.Randomizers;

public class FishRandomizer
{
    private static readonly Array MiniGameEnums = Enum.GetValues(typeof(HarvestMinigameType));
    private static readonly Array DifficultyEnums = Enum.GetValues(typeof(HarvestDifficulty));
    private static readonly Array HarvestableTypeEnums;
    private static readonly List<HarvestableType> InvalidHarvestableTypeEnums = new() {
        HarvestableType.NONE,
        HarvestableType.ICE, // requires DLC
        HarvestableType.COUNT, // i genuinely don't know what this is
        HarvestableType.CRAB // haven't implemented 
    };

    static FishRandomizer()
    {
        //  = Enum.GetValues(typeof(HarvestableType))
        HarvestableTypeEnums = Enum.GetValues(typeof(HarvestableType)).Cast<HarvestableType>().Where(item => !InvalidHarvestableTypeEnums.Contains(item)).ToArray();
    }

    // TODO: Remove possibleSizes here. Either separate out single fish randomization of process all fish at once
    public static void RandomizeFish(RandomizerConfig config, FishItemData fish, List<(float, float)>? possibleSizes = null)
    {
        if (config.RandomizeSizes)
        {
            fish.minSizeCentimeters = possibleSizes[SeededRng.Rng.Next(0, possibleSizes.Count)].Item1;
            fish.maxSizeCentimeters = possibleSizes[SeededRng.Rng.Next(0, possibleSizes.Count)].Item2;
        }

        if (config.RandomizeHarvestMinigamesTypes)
            fish.harvestMinigameType = (HarvestMinigameType)SeededRng.Rng?.Next(MiniGameEnums.Length);
        if (config.RandomizeDifficulty)
            fish.harvestDifficulty = (HarvestDifficulty)SeededRng.Rng?.Next(DifficultyEnums.Length);
        if (config.RandomizeHarvestableType)
        {
            if (fish.harvestableType != HarvestableType.CRAB)
            {
                fish.harvestableType = (HarvestableType)HarvestableTypeEnums.GetValue(SeededRng.Rng.Next(HarvestableTypeEnums.Length));
            }
        }
    }

    // TODO: Remove possibleSizes here. Either separate out single fish randomization of process all fish at once
    public static void RandomizeAllFish(RandomizerConfig config, List<FishItemData> fishList)
    {
        WinchCore.Log.Debug($"There are {fishList.Count} fish to randomize");

        List<(float, float)> possibleSizes = (fishList ?? throw new InvalidOperationException())
            .Select(fish => (fish.minSizeCentimeters,
                fish.maxSizeCentimeters)).ToList();

        foreach (var fish in fishList)
        {
            RandomizeFish(config, fish, possibleSizes);
        }
    }
}