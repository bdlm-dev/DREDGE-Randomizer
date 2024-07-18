using HarmonyLib;
using Randomizer.Data;
using Winch.Core;

namespace Randomizer.Patchers;

public class SaveManagerPatcher
{
    // Using save file creation time as persistent factor to generate seed from;
    // DREDGE doesn't actually ever delete/recreate these files, so setting that time here.

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

    // When a save file is 'created', actually update the save file creation time to current time.
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