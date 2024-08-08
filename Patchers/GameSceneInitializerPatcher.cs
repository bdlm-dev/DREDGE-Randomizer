using HarmonyLib;
using Randomizer.Config;
using Randomizer.Helpers;
using UnityEngine;
using Winch.Core;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(GameSceneInitializer))]
[HarmonyPatch("Start")]
public class GameSceneInitializerPatcher
{
    // TODO: Add an event to DredgeEvents to replace the need for this patch. GameSceneInitializer.Start is a good canditate for a new event
    public static void Prefix(GameSceneInitializer __instance)
    {
        if (RandomizerConfig.Instance.RandomizeHarvestPoIs)
        {
            HandlePOIs();
        }

        if (RandomizerConfig.Instance.RandomizeQuestsRequirements)
        {
            HandleQuests();
        }
    }


    public static void HandlePOIs()
    {
        try
        {
            var harvestPoisContainer = GameObject.Find("HarvestPOIs");
            if (harvestPoisContainer == null)
            {
                WinchCore.Log.Error("HarvestPOIs not found");
                return;
            }

            var poiList = PoiHelpers.GetPoiListFromContainer(harvestPoisContainer);
            PoiHelpers.PermuteHarvestPoiLocations(poiList);
        }
        catch (Exception e)
        {
            WinchCore.Log.Error("Error handling POIs in GameSceneInitializerPatcher: " + e);
        }
    }

    public static void HandleQuests()
    {
        try
        {

        } 
        catch(Exception e) 
        {
            WinchCore.Log.Error("Error handling Quests in GameSceneInitializerPatcher: " + e);
        }
    }
}