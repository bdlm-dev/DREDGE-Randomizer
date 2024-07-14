using Randomizer.Randomizers;
using Winch.Core;
using Winch.Core.API;
using Winch.Core.API.Events.Addressables;

namespace Randomizer;

public static class Loader
{
    public static List<ItemData>? originalAllItems = null;
    public static void Initialize()
    {
        RandomizerConfig.Initialize();

        // use specific seed as set in config
        if (RandomizerConfig.Instance.UseConfigSeed)
        {
            SeededRng.Seed = RandomizerConfig.Instance.Seed ?? SeededRng.Seed;
        }
        // else is set inside WindowsSaveStrategy.GetData: when save is loaded
        DredgeEvent.AddressableEvents.ItemsLoaded.On += OnPostItemLoad;
    }

    public static void OnPostItemLoad(object sender, AddressablesLoadedEventArgs<ItemData> args)
    {
        try
        {
            var allItems = (sender as ItemManager)?.allItems;
            var allFish = allItems?.OfType<FishItemData>().ToList();

            if (allFish == null)
            {
                WinchCore.Log.Error($"Error in {nameof(Loader)}: allFish is null");
                return;
            }

            // reset RNG when visit menu
            SeededRng.Rng = new Random(SeededRng.Seed);

            FishRandomizer.RandomizeAllFish(RandomizerConfig.Instance, allFish);
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in ItemManagerPatcher: {ex}");
        }
    }
}
