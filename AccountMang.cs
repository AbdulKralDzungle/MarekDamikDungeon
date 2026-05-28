using MarekDamikDungeon.Interfaces;

namespace MarekDamikDungeon;

public class AccountMang
{
    public bool SavePlayer(Player p)
    {
        string path = $"Accounts/{p.Name}.txt";
        StreamReader streamReader = new StreamReader(path);
        string? content = streamReader.ReadLine();
        if (content == null) return false;
        string[] properties = content.Split(";");
        string itemNames = string.Join(";", p.Inv.Select(item => item.Name));
        string data = $"{properties[0]};{p.RoomId};{p.Health};{p.Defense};{p.Attack};{itemNames}"; 
        Directory.CreateDirectory("Accounts");
        StreamWriter streamWriter = new StreamWriter(path);
        streamWriter.Write(data);
        streamWriter.Flush();
        streamWriter.Close();
        return true;
    }

    public bool RegisterPlayer(string name, string password, Player p)
    {
        string path = $"Accounts/{name}.txt";
        Console.WriteLine("lookingForPath");
        if (File.Exists(path)) return false;
        StreamWriter streamWriter = new StreamWriter(path);
        Console.WriteLine("savingPlayer");
        streamWriter.WriteLine(password);
        streamWriter.Flush();
        streamWriter.Close();
        p.Name = name;
        SavePlayer(p);
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
                // tady pls udelej nacitani tech itemu
                p.Inv = new List<IItem>();
            }
            return true;
        }
        return false;
    }
}
