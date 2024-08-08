using HarmonyLib;
using Randomizer.Randomizers;
using Winch.Core;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(DataLoader))]
[HarmonyPatch("OnQuestGridDataAddressablesLoaded")]
class OnQuestGridDataAddressablesLoadedPatcher
{
    public static void Postfix()
    {
        QuestRandomizer.RandomizeQuestGrids();
        GridConfiguration gc = GameManager.Instance.DataLoader.allGridConfigs["Fishmonger_Delivery1"];
        if (gc == null) return;
        
        WinchCore.Log.Debug("found gc");
        WinchCore.Log.Debug(gc.name);
        WinchCore.Log.Debug(gc.columns + "," + gc.rows);
    }
}
