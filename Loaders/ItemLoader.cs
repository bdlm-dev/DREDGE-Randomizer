using Randomizer.Config;
using Randomizer.Randomizers;
using Winch.Core.API.Events.Addressables;
using Winch.Core;

namespace Randomizer.Loaders;

class ItemLoader
{
    public static List<FishItemData>? allFish;

    public static void PostItemLoad(object sender, AddressablesLoadedEventArgs<ItemData> args)
    {
        try
        {
            var allItems = (sender as ItemManager)!.allItems;

            allItems.OfType<SpatialItemData>().ToList().ForEach(item =>
            {
                if (!item.canBeDiscardedByPlayer)
                {
                    item.canBeDiscardedByPlayer = true;
                    item.showAlertOnDiscardHold = true;
                }
            });

            allFish = allItems.OfType<FishItemData>().ToList();

            if (allFish == null)
            {
                WinchCore.Log.Error($"Error in {nameof(Loader)}: allFish is null");
                return;
            }

            // reset RNG when visit menu
            SeededRng.ResetRng();

            FishRandomizer.RandomizeAllFish(RandomizerConfig.Instance, allFish);
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in ItemManagerPatcher: {ex}");
        }
    }
}