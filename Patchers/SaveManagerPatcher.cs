using HarmonyLib;
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
                string filePath = __instance.saveStrategy.GetFilePath(slot);
                DateTime fileCreationTime = File.GetCreationTime(filePath);
                WinchCore.Log.Debug("Slot " + slot + " created at " + fileCreationTime.ToString() + " has hash " + fileCreationTime.GetHashCode().ToString());
                if (!RandomizerConfig.Instance.UseConfigSeed)
                {
                    SeededRng.Seed = fileCreationTime.GetHashCode();
                }
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
            try
            {
                string filePath = __instance.saveStrategy.GetFilePath(slot);
                DateTime now = DateTime.Now;
                File.SetCreationTime(filePath, now);
                if (!RandomizerConfig.Instance.UseConfigSeed)
                {
                    SeededRng.Seed = now.GetHashCode();
                }
                WinchCore.Log.Debug("Updated slot " + slot + " creation time to " + now);
            }
            catch (Exception e)
            {
                WinchCore.Log.Debug("Error setting slot " + slot + " creation time: " + e);
            }
        }
    }
}
