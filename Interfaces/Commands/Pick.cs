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
            return true;
        }
        return false;
    }

    public bool Execute(Map map)
    {
        return false;
    }

    public string Info()
    {
        return $"you picked: {item}";
    }

    public string Log(Player player)
    {
        return $"Player {player.Name} picked: {item} ";
    }

    bool IGameCommand.Exit()
    {
        return false;
    }
}
