using Randomizer.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winch.Core;

namespace Randomizer.Helpers;

class ItemHelper
{
    public static SpatialItemData GetRandomSpatialItem()
    {
        try
        {
            var allItems = GameManager.Instance.ItemManager.allItems.OfType<SpatialItemData>().ToList();
            return allItems[SeededRng.Rng.Next(0, allItems.Count)];
        }
        catch (Exception e)
        {
            WinchCore.Log.Error("Error generating random item: " + e);
            return null;
        }
    }

    public static FishItemData GetRandomFish()
    {
        try
        {
            return ItemLoader.allFish[SeededRng.Rng.Next(0, ItemLoader.allFish.Count)];
        }
        catch (Exception e)
        {
            WinchCore.Log.Error("Error generating random fish: " + e);
            return null;
        }
    }
}