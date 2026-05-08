namespace MarekDamikDungeon;

public class Konzole
{
    public string Vypis(string input, Map map, int id)
    {
        /*
        return $"---------------//{input}//---------------" +
            $"Jis v MISTNOST" +
            $"jsu zde protivníci{map.EnemeNames(0)}" +
            $"";
        */
        foreach (string s in map.PlayerStatus(id))
        {
            input += "\n " + s;
        }
        return input;
    }
}