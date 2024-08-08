using Randomizer.Helpers;
using Sirenix.Utilities;
using UnityEngine.Localization.Settings;
using Winch.Core;
using Yarn;
using Yarn.Unity;
using static Dialogue.DialogueLoader;

namespace Randomizer.Randomizers.Quests;

internal class CaughtToOrder
{
    // Accompanied by GridUIPatcher

    public static void Randomize()
    {
		QuestRandomizer.printQuestDetails("Quest_CaughtToOrder");

        HandleStep1();
        HandleStep2();
        HandleStep3();
        
    }

    private static void HandleStep1()
    {
        // step1

        // questgrid: GM_FISHMONGER_DELIVERY_1

        QuestGridConfig step1QuestGridConfig = GameManager.Instance.DataLoader.allQuestGrids.Find(grid => grid.gridKey == GridKey.GM_FISHMONGER_DELIVERY_1 && grid.presetGrid.spatialItems.Count() == 2);
        QuestStepData step1 = GameManager.Instance.QuestManager.allQuestSteps["CaughtToOrder_Step1"];

        SpatialItemInstance item1 = step1QuestGridConfig.presetGrid.spatialItems[0]; // flounder replacement
        SpatialItemInstance item2 = step1QuestGridConfig.presetGrid.spatialItems[1]; // eel replacement

        foreach (var condition in step1QuestGridConfig.completeConditions.OfType<ItemCountCondition>())
        {
            switch (condition.item.id)
            {
                case "eel":
                    condition.item = item1.GetItemData<SpatialItemData>();
                    break;
                case "flounder":
                    condition.item = item2.GetItemData<SpatialItemData>();
                    break;
            }
        }

        // Replace flounder check
        ReplaceInstruction(
            "CaughtToOrder_Step1_EvaluateWithReturn",
            CreateInstruction(Instruction.Types.OpCode.PushString,
            "flounder"),
            Instruction.Types.OpCode.PushString,
            item1.id);
        // Replace eel check
        ReplaceInstruction(
            "CaughtToOrder_Step1_EvaluateWithReturn",
            CreateInstruction(Instruction.Types.OpCode.PushString,
            "eel"),
            Instruction.Types.OpCode.PushString,
            item2.id);

        string item1Name = GameManager.Instance.ItemManager.GetItemDataById<ItemData>(item1.id).itemNameKey.GetLocalizedString();
        string item2Name = GameManager.Instance.ItemManager.GetItemDataById<ItemData>(item2.id).itemNameKey.GetLocalizedString();
       
        // Replace pursuit text content
        LocalizationSettings.StringDatabase.GetTable("Quests")[step1.shortActiveKey.TableEntryReference.KeyId].Value = $"1x {item1Name}\n1x{item2Name}"; // shortActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step1.longActiveKey.TableEntryReference.KeyId].Value = $"I'm to catch a {item1Name} and a {item2Name}."; // longActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step1.completedKey.TableEntryReference.KeyId].Value = $"I delivered a {item1Name} and a {item2Name} to the Fishmonger."; // completedKey

        // Replace dialogue text content
        LocalizationSettings.StringDatabase.GetTable("Yarn")[71737740181557250].Value = $"They asked for [C0]one {item1Name}[/C0] and [C0]one {item2Name}[/C0]. Just bring 'em in when you have them.";
    }

    private static void HandleStep2()
    {
        QuestGridConfig step2QuestGridConfig = GameManager.Instance.DataLoader.allQuestGrids.Find(grid => grid.gridKey == GridKey.GM_FISHMONGER_DELIVERY_2 && grid.presetGrid.spatialItems.Count() == 3);
        QuestStepData step2 = GameManager.Instance.QuestManager.allQuestSteps["CaughtToOrder_Step2"];

        step2QuestGridConfig.presetGrid.spatialItems.ForEach(item => WinchCore.Log.Debug("item: " + item.id));
        SpatialItemInstance item1 = step2QuestGridConfig.presetGrid.spatialItems[0]; // squid replacement
        SpatialItemInstance item2 = step2QuestGridConfig.presetGrid.spatialItems[1]; // black-grouper replacement

        foreach (var condition in step2QuestGridConfig.completeConditions.OfType<ItemCountCondition>())
        {
            switch (condition.item.id)
            {
                case "squid":
                    condition.item = item1.GetItemData<SpatialItemData>();
                    break;
                case "black-grouper":
                    condition.item = item2.GetItemData<SpatialItemData>();
                    break;
            }
        }

        // Replace squid check
        ReplaceInstruction(
            "CaughtToOrder_Step2_EvaluateWithReturn",
            CreateInstruction(Instruction.Types.OpCode.PushString,
            "squid"),
            Instruction.Types.OpCode.PushString,
            item1.id);

        /*  (would feasibly prevent more softlocks but will leave the funny in) 
        
        // This normally requests TWO squid- want to make that ONE of whatever item
        // But instruction that defines that number TWO is not unique in the node
        // So can only replace by instruction index (is the first instance of this repeated instruction, but still unreliable)
        // Replace desired item amount with 1
        ReplaceInstruction(
            "CaughtToOrder_Step2_EvaluateWithReturn",
            4,
            Instruction.Types.OpCode.PushFloat,
            1f
            );
        */

        // Replace black-grouper check
        ReplaceInstruction(
            "CaughtToOrder_Step2_EvaluateWithReturn",
            CreateInstruction(Instruction.Types.OpCode.PushString,
            "black-grouper"),
            Instruction.Types.OpCode.PushString,
            item2.id);

        string item1Name = GameManager.Instance.ItemManager.GetItemDataById<ItemData>(item1.id).itemNameKey.GetLocalizedString();
        string item2Name = GameManager.Instance.ItemManager.GetItemDataById<ItemData>(item2.id).itemNameKey.GetLocalizedString();
        
        // Replace pursuit text content
        LocalizationSettings.StringDatabase.GetTable("Quests")[step2.shortActiveKey.TableEntryReference.KeyId].Value = $"2x {item1Name}\n1x{item2Name}"; // shortActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step2.longActiveKey.TableEntryReference.KeyId].Value = $"I need to catch 2 {item1Name} and 1 {item2Name}. They only bite at night."; // longActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step2.completedKey.TableEntryReference.KeyId].Value = $"I delivered 2 {item1Name} and 1 {item2Name} to the Fishmonger."; // completedKey

        // Replace dialogue text content
        LocalizationSettings.StringDatabase.GetTable("Yarn")[71737740181557257].Value = $"I've got another order here. This one's a little more curious. They want [C0]a couple of {item1Name}[/C0] and [C0]a whole {item2Name}[/C0].";
    }

    private static void HandleStep3()
    {
        QuestGridConfig step3QuestGridConfig = GameManager.Instance.DataLoader.allQuestGrids.Find(grid => grid.gridKey == GridKey.GM_FISHMONGER_DELIVERY_3 && grid.presetGrid.spatialItems.Count() == 2);
        QuestStepData step3 = GameManager.Instance.QuestManager.allQuestSteps["CaughtToOrder_Step3"];

        step3QuestGridConfig.presetGrid.spatialItems.ForEach(item => WinchCore.Log.Debug("item: " + item.id));
        SpatialItemInstance item1 = step3QuestGridConfig.presetGrid.spatialItems[0]; // crab replacement
        SpatialItemInstance item2 = step3QuestGridConfig.presetGrid.spatialItems[1]; // fiddler-crab replacement

        foreach (var condition in step3QuestGridConfig.completeConditions.OfType<ItemCountCondition>())
        {
            switch (condition.item.id)
            {
                case "crab":
                    condition.item = item1.GetItemData<SpatialItemData>();
                    break;
                case "fiddler-crab":
                    condition.item = item2.GetItemData<SpatialItemData>();
                    break;
            }
        }

        // Replace squid check
        ReplaceInstruction(
            "CaughtToOrder_Step3_EvaluateWithReturn",
            CreateInstruction(Instruction.Types.OpCode.PushString,
            "crab"),
            Instruction.Types.OpCode.PushString,
            item1.id);

        /*  (would feasibly prevent more softlocks but will leave the funny in) 
        
        // This normally requests TWO squid- want to make that ONE of whatever item
        // But instruction that defines that number TWO is not unique in the node
        // So can only replace by instruction index (is the first instance of this repeated instruction, but still unreliable)
        // Replace desired item amount with 1
        ReplaceInstruction(
            "CaughtToOrder_Step2_EvaluateWithReturn",
            4,
            Instruction.Types.OpCode.PushFloat,
            1f
            );
        */

        // Replace black-grouper check
        ReplaceInstruction(
            "CaughtToOrder_Step3_EvaluateWithReturn",
            CreateInstruction(Instruction.Types.OpCode.PushString,
            "fiddler-crab"),
            Instruction.Types.OpCode.PushString,
            item2.id);

        string item1Name = GameManager.Instance.ItemManager.GetItemDataById<ItemData>(item1.id).itemNameKey.GetLocalizedString();
        string item2Name = GameManager.Instance.ItemManager.GetItemDataById<ItemData>(item2.id).itemNameKey.GetLocalizedString();

        // Replace pursuit text content
        LocalizationSettings.StringDatabase.GetTable("Quests")[step3.shortActiveKey.TableEntryReference.KeyId].Value = $"1x {item1Name}\n1x{item2Name}"; // shortActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step3.longActiveKey.TableEntryReference.KeyId].Value = $"The Fishmonger wants a {item1Name} and a {item2Name}."; // longActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step3.completedKey.TableEntryReference.KeyId].Value = $"I delivered a {item1Name} and a {item2Name} to the Fishmonger."; // completedKey

        // Replace dialogue text content
        LocalizationSettings.StringDatabase.GetTable("Yarn")[128706042417827844].Value = $"I've got a customer after [C0]a {item1Name}[/C0] and [C0]a {item2Name}[/C0]. Both species can be found around here.";
    }
}

/*
 * Quest_CaughtToOrder:
	CaughtToOrder_Step1Pre
	CaughtToOrder_Step1
	CaughtToOrder_Step2
	CaughtToOrder_Step3Pre
	CaughtToOrder_Step3
	CaughtToOrder_Step4Pre
	CaughtToOrder_Step4

[Randomizer.<>c.<printQuestDetails>b__6_0] :    CaughtToOrder_Step1:
                                               1x Flounder
                                             1x Eel
                                               I'm to catch a Gulf Flounder and a Grey Eel. I'll need a rod that can handle Shallow water.
                                               I delivered a Flounder and an Eel to the Fishmonger.
[Randomizer.<>c.<printQuestDetails>b__6_0] :    CaughtToOrder_Step2:
                                               2x Arrow Squid
                                             1x Black Grouper
                                               I need to catch 2 Squid and 1 Black Grouper. They only bite at night.
                                               I delivered the Squid and Black Grouper to the Fishmonger.
[Randomizer.<>c.<printQuestDetails>b__6_0] : Error on CaughtToOrder_Step3Pre
[Randomizer.<>c.<printQuestDetails>b__6_0] :    CaughtToOrder_Step3:
                                               1x Common Crab
                                             1x Fiddler Crab
                                               The Fishmonger wants a Common Crab and a Fiddler Crab.
                                               I delivered a Common Crab and a Fiddler Crab to the Fishmonger.
[Randomizer.<>c.<printQuestDetails>b__6_0] : Error on CaughtToOrder_Step4Pre
[Randomizer.<>c.<printQuestDetails>b__6_0] :    CaughtToOrder_Step4:
                                               1x Aberrant Fish
                                               The Fishmonger wants me to bring him an aberrant fish - a mutated version of a regular fish.
                                               I delivered an aberrated fish to the Fishmonger.
 
 */