using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarekDamikDungeon.Interfaces.Commands;

public class Shout : IGameCommand
{
    bool IGameCommand.Execute(string arg, Map map,  Client client)
    {
        client.Brodcast(arg);
        return true;
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
        return "you shouted..."; 
    }

    string IGameCommand.Log(Player player)
    {
        return $"player {player.Name} shouted";
    }
}