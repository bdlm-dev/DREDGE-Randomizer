using Dialogue;
using HarmonyLib;
using Randomizer.Config;
using Randomizer.Data;
using Randomizer.Loaders;
using UnityEngine;
using Winch.Core;
using Winch.Core.API;

namespace Randomizer;

public static class Loader
{
    public static List<ItemData>? originalAllItems = null;
    public static GameObject gameObject;
    public static void Initialize()
    {
        new Harmony("mmbluey.randomizer").PatchAll();

        gameObject = new GameObject("Randomizer");
        gameObject.DontDestroyOnLoad();

        RandomizerConfig.Initialize();
        ModSaveData.Initialize();
        DialogueAPI.Initialize();

        // use specific seed as set in config
        // else is set inside WindowsSaveStrategy.GetData: when save is loaded
        if (RandomizerConfig.Instance.UseConfigSeed)
        {
            SeededRng.Seed = RandomizerConfig.Instance.Seed ?? SeededRng.Seed;
        }
        
        // attach event handling to winch events
        DredgeEvent.AddressableEvents.ItemsLoaded.On += ItemLoader.PostItemLoad;
        DredgeEvent.AddressableEvents.QuestsLoaded.On += QuestLoader.PostQuestLoad;

        
    }
}

/*
 * what have?
 *  - yarn DialogueRunner
 *  - questStepData -> yarnRootNode
 *  - questStepData -> QuestStepCondition
 *  - QuestStepCondition : ItemInventoryCondition ?! CompletedGridCondition : ItemCountCondition
 *  test by checing for ItemInventoryCondition & changing it, see if pursuit details update
 */