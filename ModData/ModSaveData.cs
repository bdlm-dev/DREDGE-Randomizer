using Newtonsoft.Json;
using System.Reflection;
using Winch.Core;

namespace Randomizer.Data;

public class ModSaveData
{
    private static readonly string _saveDataFile = "save.json";
    private static string _savePath = "";


    private static ModSaveData? _instance;
    public static ModSaveData Instance => _instance ??= new();

    private static readonly int slotCount = 4; // 4 save slots :) I don't imagine this will ever change but doesn't hurt to have it here
    public SlotData[] data = CreateNewSaveData();

    public static void Initialize()
    {
        WinchCore.Log.Debug("hereeeeee");
        try
        {
            // try read from save.json
            // if find data, load it
            // else: generate new base data per slot

            WinchCore.Log.Debug("here?");
            string _rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            WinchCore.Log.Debug(_rootPath);
            _savePath = Path.Combine(_rootPath, _saveDataFile);
            WinchCore.Log.Debug(_savePath);

            if (File.Exists(_savePath))
            {
                WinchCore.Log.Debug("a");
                string JSON = File.ReadAllText(_savePath);
                WinchCore.Log.Debug($"Found {JSON} at {_savePath}");
                try
                {
                    Instance.data = JsonConvert.DeserializeObject<SlotData[]>(JSON) ?? throw new JsonSerializationException("Error deserializing " + _savePath);
                    // After loading, rewrite with same data to populate JSON with any missing attributes
                    File.WriteAllText(_savePath, JsonConvert.SerializeObject(Instance.data, Formatting.Indented));
                }
                catch (Exception e)
                {
                    WinchCore.Log.Debug($"Error deserializing JSON: {JSON}\nWith error: {e}");
                }
            }
            else
            {
                WinchCore.Log.Debug("b");
                try
                {
                    File.WriteAllText(_savePath, JsonConvert.SerializeObject(Instance.data, Formatting.Indented));
                }
                catch (Exception e)
                {
                    WinchCore.Log.Debug($"Error serializing default save data: {e}");
                }
            }
        }
        catch (Exception e)
        {
            WinchCore.Log.Error("Error loading save data: " + e);
        }
    }

    private static SlotData[] CreateNewSaveData()
    {
        WinchCore.Log.Debug("hello");
        SlotData[] newData = new SlotData[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            SlotData slotData = new(i);
            WinchCore.Log.Debug($"Created new data {slotData}, {i}, {slotData.slot}");
            newData[i] = slotData;
        }
        return newData;
    }

    private static void Save()
    {
        try
        {
            File.WriteAllText(_savePath, JsonConvert.SerializeObject(Instance.data, Formatting.Indented));
            WinchCore.Log.Debug("Successfully saved to " + _saveDataFile);
        } 
        catch (Exception e)
        {
            WinchCore.Log.Error($"Error serializing save data: {e}");
        }
    }

    public static void ResetSlot(int slot)
    {
        Instance.data[slot] = new SlotData(slot);
        WinchCore.Log.Debug("Reset data in slot " + slot);
        Save();
    }

    public static SlotData GetSlot(int slot)
    {
        WinchCore.Log.Debug("Fetching slot " + slot);
        return Instance.data[slot];
    }
}
