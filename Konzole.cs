namespace MarekDamikDungeon;

public class Konzole
{
    public string Vypis(string input, Map map)
    {
        return $"---------------//{input}//---------------" +
            $"Jis v MISTNOST" +
            $"jsu zde protivníci{map.EnemeNames(0)}" +
            $"";
    }
    
}