using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon.Interfaces
{
    /**
     * Game loop is interface that makes possible the command design pattern in Gameloop
     * the individual commands can be found in the 'Commands directory'
     */
    internal interface IGameCommand
    {
        public bool Execute();
        public bool Exit();
        public string Info();
        public string Log(Player player);
    }
}
