namespace MarekDamikDungeon;

public class Konzole
{
    public string Vypis(string input, Map map, int id)
    {
        foreach (string s in map.PlayerStatus(id))
        {
            input += "\n " + s;
        }
        return input;
    }
}