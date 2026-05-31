namespace MarekDamikDungeon.Interfaces.Items;

public class Armor : IItem
{
    public string Name { get; set; } = "armor";
    public string Description { get; set; } = "A sturdy piece of armor that helps block incoming attacks.";
    public int NumberOfUses { get; set; } = 20;

    public string Effect()
    {
        return "You adjust the armor and feel better protected.";
    }
}
