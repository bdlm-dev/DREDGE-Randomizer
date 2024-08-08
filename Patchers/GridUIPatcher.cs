using HarmonyLib;
using Randomizer.Config;
using Randomizer.Helpers;
using UnityEngine;
using Winch.Core;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(GridUI), nameof(GridUI.GenerateGrid))]
public class GridUIPatcher
{
    public static void Prefix(GridUI __instance)
    {
        switch (__instance.gridConfiguration.name)
        {
            case "Fishmonger_Delivery1":
            case "Fishmonger_Delivery2":
            case "Fishmonger_Delivery3":
                MakeGrid7x7(__instance);
                break;
            default:
                WinchCore.Log.Debug("something else: " + __instance.gridConfiguration.name);
                break;
        }
    }

    // Convert grid into generic 7x7 grid that takes anything
    private static void MakeGrid7x7(GridUI __instance)
    {
        GridCellData[,] newGrid = new GridCellData[7, 7];
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GridCellData cell = new()
                {
                    x = i,
                    y = j,
                    acceptedItemType = ItemType.ALL,
                    acceptedItemSubtype = ItemSubtype.ALL
                };
                newGrid[i, j] = cell;
            }
        }
        __instance.linkedGrid.grid = newGrid;
        
        __instance.gridConfiguration.rows = 7;
        __instance.gridConfiguration.columns = 7;
    }
}