using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarekDamikDungeon.Interfaces.Commands;

public class Walk: IGameCommand
{
    private bool succesfull;
    bool IGameCommand.Execute(string arg, Map map,  Client client)
    {
        succesfull = map.WalkPlayer(client.Id, arg);
        return succesfull;
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
        if(succesfull)return " You walked in there \n \n";
        return "you cannot walk there";
    }

    string IGameCommand.Log(Player player)
    {
        return $"player {player.Name} walked into: ";
    }
}