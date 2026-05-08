using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarekDamikDungeon.Interfaces.Commands;

public class Walk: IGameCommand
{
    bool IGameCommand.Execute(string arg, Map map,  Client client)
    {
        return map.WalkPlayer(client.Id, arg);
    }

    bool IGameCommand.Execute(Map map)
    {
        return true;
    }

    public bool Exit()
    {
        return false;
    }

    public string Info()
    {
        return "you are now in: ";
    }

    string IGameCommand.Log(Player player)
    {
        return $"player {player.Name} walked into: ";
    }
}