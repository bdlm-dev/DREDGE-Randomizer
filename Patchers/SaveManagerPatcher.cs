using HarmonyLib;
using Randomizer.Data;
using Winch.Core;

namespace Randomizer.Patchers;

public class SaveManagerPatcher
{
    // Called when a save slot is loaded (not when new save is created)
    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.Load))]
    public class LoadPatch
    {
        public static void Prefix(int slot, SaveManager __instance)
        {
            try
            {
                SeededRng.UpdateSeed(ModSaveData.GetSlot(slot).seed);
            }
            catch (Exception e)
            {
                WinchCore.Log.Error($"Error in {nameof(SaveManagerPatcher)}: exception {e}");
            }
        }
    }

    // Called when a new save is created
    [HarmonyPatch(typeof(SaveManager), nameof(SaveManager.CreateSaveData))]
    public class CreateSaveDataPatch
    {
        public static void Postfix(int slot, SaveManager __instance)
        {
            ModSaveData.ResetSlot(slot);
            SeededRng.UpdateSeed(ModSaveData.GetSlot(slot).seed);
        }
    }
}