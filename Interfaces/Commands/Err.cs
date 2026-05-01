using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarekDamikDungeon.Interfaces.Commands;
    /**
     * Exit is command used in GameLoop as a part of command design pattern
     * It writes a helping text to player, acts like guide
     */
public class Err : IGameCommand
{
    bool IGameCommand.Execute(string arg, Map map)
    {
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
        return "Syntax error, your command is not valid, try again"; 
    }

    string IGameCommand.Log(Player player)
    {
        return $"Player {player.Name} tried wrong command";
    }
}