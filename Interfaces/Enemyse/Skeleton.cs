namespace MarekDamikDungeon.Interfaces.Enemyse;

public class Skeleton : IEnemy
{
    public string Name { get; set; } = "skeleton";
    public string Description { get; set; } = "Old bones held together by stubborn dark magic.";
    public int Hp { get; set; } = 12;
    public int MaxHp { get; set; } = 12;
    public int Defense { get; set; } = 7;
    public int Damage { get; set; } = 4;

    public bool ChangeHelth(int dmg)
    {
        Hp -= dmg;
        return Hp > 0;
    }

    public bool IsAttacking()
    {
        return Hp > 0;
    }

    public void LoadEnemy(string data)
    {
        string[] values = data.Split(",", StringSplitOptions.TrimEntries);
        if (values.Length > 0) Name = values[0];
        if (values.Length > 1) Description = values[1];
        if (values.Length > 2 && int.TryParse(values[2], out int hp))
        {
            Hp = hp;
            MaxHp = hp;
        }
        if (values.Length > 3 && int.TryParse(values[3], out int defense)) Defense = defense;
        if (values.Length > 4 && int.TryParse(values[4], out int damage)) Damage = damage;
    }
}
