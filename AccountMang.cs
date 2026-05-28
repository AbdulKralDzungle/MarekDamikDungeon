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
        string path = $"Accounts/{name}.txt";
        if (File.Exists(path))
        {
            StreamReader streamReader = new StreamReader(path);
            string? content = streamReader.ReadLine();
            if (content == null) return false;
            string[] properties = content.Split(";");
            if (password == properties[0])
            {
                p.Name = name;
                p.RoomId = Int32.Parse(properties[1]); 
                p.Health = Int32.Parse(properties[2]);
                p.Defense = Int32.Parse(properties[3]);
                p.Attack = Int32.Parse(properties[4]);
                string items 
    private List<IItem> _inv;
            }
            return true;
        }
        return false;
    }
}
