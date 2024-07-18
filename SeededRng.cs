using Randomizer.Config;

namespace Randomizer;

public static class SeededRng
{
    // Supposedly not good to use .getHashCode() by itself, but don't think it matters for this use-case
    // https://stackoverflow.com/a/9084760
    public static int Seed = Guid.NewGuid().GetHashCode();

    private static Random? _rng;
    public static Random? Rng
    {
        get => _rng ?? new(Seed);
        set => _rng = value;
    }

    public static void ResetRng() => _rng = new(Seed);

    public static void UpdateSeed(int seed)
    {
        if (!RandomizerConfig.Instance.UseConfigSeed)
        {
            Seed = seed;
            ResetRng();
        }
    }

    public static List<T> FisherYatesShuffle<T>(List<T> originalList)
    {
        List<T> list = new(originalList);
        if (Rng == null) return originalList;

        var n = list.Count();
        for (int i = 0; i < (n - 1); i++)
        {
            var swapIdx = i + SeededRng.Rng.Next(n - i);
            (list[i], list[swapIdx]) = (list[swapIdx], list[i]);
        }

        return list;
    }
}