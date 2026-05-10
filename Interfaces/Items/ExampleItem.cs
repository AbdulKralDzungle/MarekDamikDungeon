namespace MarekDamikDungeon.Interfaces.Items;

public class ExampleItem : IItem
{
    public string Name { get; set; } =  "ExampleItem";
    public string Description { get; set; } = "Idk, jestly je tenhle item ve hře tak se stalo něco HOOOOOOOOOOOOOOOODNĚ špatně";
    public int NumberOfUses { get; set; } = 69;
    public string Effect()
    {
        throw new NotImplementedException(); //idk proc tohle vraci string, bych tam dal bool ale netusim jestly jsem to delal ja nebo Marek, a nechce se mi zjistovat myslenkovy pochod ktery k tomudle vedl...
    }
}