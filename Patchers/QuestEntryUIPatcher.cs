using HarmonyLib;
using Winch.Core;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(QuestEntryUI), nameof(QuestEntryUI.OnDisable))]
class QuestEntryUIPatcher
{
    public static void Prefix(QuestEntryUI __instance)
    {
        WinchCore.Log.Debug($"questentry: {__instance.questData.titleKey} | {GameManager.Instance.QuestManager.GetShortActiveStepKeyByQuestId(__instance.questData.name)}");
    }
}
