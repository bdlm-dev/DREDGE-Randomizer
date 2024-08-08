using Randomizer.Config;
using Randomizer.Helpers;
using Randomizer.Loaders;
using Randomizer.Randomizers.Quests;
using Sirenix.Utilities;
using UnityEngine;
using Winch.Core;

namespace Randomizer.Randomizers;

public class QuestRandomizer
{
    // TODO: Add quest randomization

    private static readonly List<Action> individualQuestRandomizers = new List<Action>()
    {
        PackageDelivery.Randomize,
        CaughtToOrder.Randomize
    };

    public static void RandomizeQuests()  // RandomizerConfig config
    {
        WinchCore.Log.Debug("Randomizing quests");

        // can't automatically target quests to randomize 
        // so have to do this all manually -.-
        individualQuestRandomizers.ForEach(randomizer => randomizer());
    }

    public static void RandomizeQuestSteps(Dictionary<string, QuestStepData> steps)
    {
        foreach (var step in steps)
        {
            WinchCore.Log.Debug("Randomizing step" + step.Key);
            foreach (var condition in step.Value.showConditions)
            {
                HandleCondition(condition);
            }
        }
    }

    public static void HandleCondition(QuestStepCondition condition)
    {
        WinchCore.Log.Debug("Handling condition: " + condition);

        switch (condition)
        {
            case ItemInventoryCondition itemInventoryCondition:
                WinchCore.Log.Debug("itemInventoryCondition found");
                ItemData? randomItem = ItemHelper.GetRandomSpatialItem();
                if (randomItem != null)
                {
                    itemInventoryCondition.itemId = randomItem.id;
                    WinchCore.Log.Debug("Deliverable set to " + randomItem.id);
                } else
                {
                    WinchCore.Log.Debug("Failed to randomize deliverable");
                }
                break;

            default:
                WinchCore.Log.Debug("Unhandled condition type: " + condition.GetType());
                break;
        }
    }

    public static void RandomizeQuestGrids()
    {
        WinchCore.Log.Debug("questgrids" + GameManager.Instance.DataLoader.allQuestGrids.Count());
        GameManager.Instance.DataLoader.allQuestGrids.ForEach(grid =>
        {
            if (grid.presetGrid == null) return;
            WinchCore.Log.Debug(grid.gridKey);
            
            SerializableGrid newGrid = grid.presetGrid;
            grid.gridConfiguration.columns = 7;
            grid.gridConfiguration.rows = 7;
            
            int itemCount = grid.presetGrid.spatialItems.Count();
            
            WinchCore.Log.Debug($" dims: {grid.gridConfiguration.rows},{grid.gridConfiguration.columns} {grid.presetGrid}");

            Dictionary<string, SpatialItemData> pairs = new();
            for (int i = 0; i < itemCount; i++)
            {
                if (!pairs.ContainsKey(grid.presetGrid.spatialItems[i].id))
                {
                    pairs.Add(grid.presetGrid.spatialItems[i].id, ItemHelper.GetRandomSpatialItem());
                }
                WinchCore.Log.Debug($"    item: {grid.presetGrid.spatialItems[i].id}");
            }

            for (int i = 0; i < itemCount; i++)
            {
                SpatialItemData newItem = pairs[grid.presetGrid.spatialItems[i].id];
                grid.presetGrid.spatialItems[i] = GameManager.Instance.ItemManager.CreateItem<SpatialItemInstance>(newItem);
                WinchCore.Log.Debug($"new item: {newItem.id}");
            }
            grid.presetGrid = newGrid;
        });
    }

    public static void printAllDetails()
    {
        GameManager.Instance.QuestManager.allQuests.Keys.ForEach(key =>
        {
            QuestData target = GameManager.Instance.QuestManager.allQuests[key];
            WinchCore.Log.Debug($"{target.name}:");
            target.steps.ForEach(step =>
            {
                WinchCore.Log.Debug($"\t{step.name} : {step.shortActiveKey}, {step.longActiveKey}, {step.completedKey}");
            });
        });
    }

    public static void printQuestDetails(string targetQuest)
    {
        GameManager.Instance.QuestManager.allQuests[targetQuest].steps.ForEach(step =>
        {
            try
            {
                WinchCore.Log.Debug($"\t{step.name}:\n  {step.shortActiveKey.GetLocalizedString()}\n  {step.longActiveKey.GetLocalizedString()}\n  {step.completedKey.GetLocalizedString()}");
            } catch (Exception e)
            {
                WinchCore.Log.Error("Error on " + step.name);
            }
        });
    }
}