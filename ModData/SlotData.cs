namespace Randomizer.Data;

public class SlotData
{
    public readonly int slot;

    public int seed = Guid.NewGuid().GetHashCode();


    public SlotData(int slot)
    {
        this.slot = slot;
    }
}
