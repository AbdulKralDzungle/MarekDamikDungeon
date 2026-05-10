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
    bool IGameCommand.Execute(string arg, Map map,  Client client)
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
        return "This is your guidline to this game: \n" +
               "You play using commands: Walk Attack Pick Exit Help Shout \n" +
               "You use command by writing it to the terminal, usually, the command requires argument, to specify what you are doing\n" +
               "Commands Exit and Help can be used without argument, other will need it. Example: Shout Hello! \n" +
               "Walk [room name] \n" +
               "Attack [eneme/player]\n" +
               "Pick [item]\n" +
               "Exit\n" +
               "Help\n" +
               "Shout [message]\n";
    }

    string IGameCommand.Log(Player player)
    {
        return $"Player {player.Name} listed Help";
    }
}