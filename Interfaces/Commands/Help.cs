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
public class Help : IGameCommand
{
    bool IGameCommand.Execute(string arg, Map map)
    {
        return true;
    }

    public bool Exit()
    {
        return false;
    }

    public string Info()
    {
        return "Help: Walk Attack Grab Exit Help Interact Shout"; // will have to change the help text
    }

    string IGameCommand.Log(Player player)
    {
        return $"Player {player.Name} listed Help";
    }
}