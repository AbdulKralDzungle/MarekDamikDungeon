namespace MarekDamikDungeon.Interfaces.Items;

public class HealthPotion : IItem
{
    public string Name { get; set; } = "healthpotion";
    public string Description { get; set; } = "A red potion that can restore health.";
    public int NumberOfUses { get; set; } = 1;

    public string Effect()
    {
        return "You drink the potion and feel warmer.";
    }
}
