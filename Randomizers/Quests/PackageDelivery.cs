using Randomizer.Helpers;
using UnityEngine.Localization.Settings;
using Yarn;
using Yarn.Unity;
using static Dialogue.DialogueLoader;

namespace Randomizer.Randomizers.Quests;

internal class PackageDelivery
{
    public static void Randomize()
    {
        /*  What aspects are there to randomize?
         *  - Mayor gives item to deliver (this is randomized via allQuestGrids)
         *  - Item to be delivered (& text displaying it in pursuit screen)
         *  
         *  No continuity between what mayor gives you, and what you deliver >:)
         */

        DredgeDialogueRunner runner = GameManager.Instance.DialogueRunner;
        DredgeLocalizedLineProvider lineProvider = runner.lineProvider as DredgeLocalizedLineProvider;
        
        ItemData item = ItemHelper.GetRandomSpatialItem();

        // Replace check for whether you have arrived at little marrow with deliverable
        ReplaceInstruction(
            "LittleMarrow_Root", 
            CreateInstruction(Instruction.Types.OpCode.PushString, 
            "quest-package"), 
            Instruction.Types.OpCode.PushString, 
            item.id);

        // Replace removal of quest deliverable in both nodes
        Instruction oldRemoveItemInstruction = CreateInstruction(Instruction.Types.OpCode.RunCommand, "RemoveItemById quest-package 1", 0f);
        
        ReplaceInstruction(
            "PackageDelivery_1_Normal",
            oldRemoveItemInstruction,
            Instruction.Types.OpCode.RunCommand,
            "RemoveItemById " + item.id + " 1",
            0f);
        ReplaceInstruction(
            "PackageDelivery_1_Infected",
            oldRemoveItemInstruction,
            Instruction.Types.OpCode.RunCommand,
            "RemoveItemById " + item.id + " 1",
            0f);

        QuestStepData step = GameManager.Instance.QuestManager.allQuestSteps["PackageDelivery_Deliver"];

        // Replace "Deliver the package" active quest step pursuit text with deliverable name
        // step: PackageDelivery_Deliver
        
        string itemName = item.itemNameKey.GetLocalizedString();
        LocalizationSettings.StringDatabase.GetTable("Quests")[step.shortActiveKey.TableEntryReference.KeyId].Value = $"Deliver a {itemName}"; // shortActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step.longActiveKey.TableEntryReference.KeyId].Value = $"Deliver a {itemName} to the Dockworker at Little Marrow"; // longActiveKey
        LocalizationSettings.StringDatabase.GetTable("Quests")[step.completedKey.TableEntryReference.KeyId].Value = $"I delivered the {itemName}"; // longActiveKey
    }

    public static void UpdateSize()
    {

    }
}
