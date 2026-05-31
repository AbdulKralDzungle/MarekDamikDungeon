namespace MarekDamikDungeon.Interfaces.Enemyse;

public class Slime : IEnemy
{
    public string Name { get; set; } = "slime";
    public string Description { get; set; } = "A slow moving acidic blob.";
    public int Hp { get; set; } = 6;
    public int MaxHp { get; set; } = 6;
    public int Defense { get; set; } = 2;
    public int Damage { get; set; } = 2;

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
