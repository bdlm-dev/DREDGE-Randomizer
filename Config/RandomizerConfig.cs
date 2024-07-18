using Winch.Core;

namespace Randomizer.Config;

public class RandomizerConfig
{
    private static RandomizerConfig? _instance;
    public static RandomizerConfig Instance => _instance ??= new();

    public bool UseConfigSeed { get; set; } = false;
    public bool RandomizeHarvestPoIs { get; set; } = true;
    public bool RandomizePoiCoordinates { get; set; } = true;
    public bool RandomizeSizes { get; set; } = true;
    public bool RandomizeQuestsRequirements { get; set; } = true;
    public bool RandomizeHarvestMinigamesTypes { get; set; } = true;
    public bool RandomizeHarvestableType { get; set; } = true;
    public bool RandomizeDifficulty { get; set; } = true;
    public int? Seed { get; set; } = null;

    public static void Initialize()
    {
        ReadConfig();
    }

    private static void ReadConfig()
    {
        try
        {
            Instance.Seed = Convert.ToInt32(ModConfig.GetProperty<long>("Randomizer", "Seed", SeededRng.Seed)); // integer appears to get read as 64, then fails to be converted to int32
            Instance.UseConfigSeed = ModConfig.GetProperty("Randomizer", "UseConfigSeed", false);
            Instance.RandomizeDifficulty = ModConfig.GetProperty("Randomizer", "RandomizeDifficulty", true);
            Instance.RandomizeHarvestPoIs = ModConfig.GetProperty("Randomizer", "RandomizeHarvestPoIs", true);
            Instance.RandomizePoiCoordinates = ModConfig.GetProperty("Randomizer", "RandomizePoiCoordinates", true);
            Instance.RandomizeSizes = ModConfig.GetProperty("Randomizer", "RandomizeSizes", true);
            Instance.RandomizeQuestsRequirements = ModConfig.GetProperty("Randomizer", "RandomizeQuestsRequirements", true);
            Instance.RandomizeHarvestMinigamesTypes = ModConfig.GetProperty("Randomizer", "RandomizeHarvestMinigamesTypes", true);
            Instance.RandomizeHarvestableType = ModConfig.GetProperty("Randomizer", "RandomizeHarvestableType", true);
            Instance.RandomizeDifficulty = ModConfig.GetProperty("Randomizer", "RandomizeDifficulty", true);
        }
        catch (Exception e)
        {
            WinchCore.Log.Debug("Error reading config: " + e);
        }
    }
}