namespace MarekDamikDungeon.Interfaces.Items;

public class RustySword : IItem
{
    public string Name { get; set; } = "rustysword";
    public string Description { get; set; } = "An old sword. Not pretty, but still sharp enough.";
    public int NumberOfUses { get; set; } = 25;

    public string Effect()
    {
        return "You swing the rusty sword through the air.";
    }
}
