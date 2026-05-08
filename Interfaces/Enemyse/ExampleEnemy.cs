namespace MarekDamikDungeon.Interfaces.Enemyse;

public class ExampleEnemy : IEnemy
{
    public string Name { get; set; } = "Example Enemy";
    public string Description { get; set; } = "Jak jsem se dostal do hry... tady nemam co delat, POMOOOOOOOOOOC";
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Damage { get; set; }
    public bool ChangeHelth(int dmg)
    {
        throw new NotImplementedException();
    }

    public bool IsAttacking()
    {
        throw new NotImplementedException();
    }

    public void LoadEnemy(string data)
    {
        throw new NotImplementedException();
    }
}