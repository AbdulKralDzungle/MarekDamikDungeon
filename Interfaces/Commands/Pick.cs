using System.Windows.Input;

namespace MarekDamikDungeon.Interfaces.Commands;

internal class Pick : IGameCommand
{
    public IItem? item;
    public bool Execute(string arg, Map map, Client client)
    {
        item = map.ExtractItem(arg, map.GetPlayer(client.Id).RoomId);
        if(item != null)
        {
            map.GetPlayer(client.Id).AddItem(item);
            Console.WriteLine($"item picked {arg}");
            return true;
        }
        item = null;
        return false;
    }

    public bool Execute(Map map)
    {
        item = null;
        return false;
    }

    public string Info()
    {
       if(item == null) return "Nothing picked"; 
       return $"you picked: {item.Name}, this item {item.Description}";
    }

    public string Log(Player player)
    {
        if(item == null) return $"Player {player.Name} tried wrong command";
        return $"Player {player.Name} picked: {item} ";
    }

    bool IGameCommand.Exit()
    {
        return false;
    }
}
