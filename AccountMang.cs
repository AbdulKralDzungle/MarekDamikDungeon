namespace MarekDamikDungeon;

public class AccountMang
{
    public bool SavePlayer(Player p)
    {
        return true;
    }

    public bool RegisterPlayer(string name, string password)
    {
        return true;
    }

    public bool LoadPlayer(string name, string password, Player p)
    {
        if (File.Exists($"Accounts/{name}.txt"))
        {
            
        }
        return true;
    }
}
