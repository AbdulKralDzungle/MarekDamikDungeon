using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon.Interfaces.Commands
{
    /**
     * Exit is command used in GameLoop as a part of command design pattern
     * its purpose is to be a way for player to exit the game
     */
    internal class Exit : IGameCommand
    {
        public bool Execute()
        {
            return true;
        }

        public string Info()
        {
            return "You exited the game";
        }

        public string Log(Player player)
        {
            return $"Player {player.Name} exited the game";
        }

        bool IGameCommand.Exit()
        {
            return true;
        }
    }
}
