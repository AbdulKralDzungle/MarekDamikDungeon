using MarekDamikDungeon.Interfaces;
using MarekDamikDungeon.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarekDamikDungeon
{
    internal class GameLoop
    {
        private Dictionary<string, IGameCommand> commands;

        public GameLoop()
        {
            Initialize();
        }

        private void Initialize()
        {
            commands = new Dictionary<string, IGameCommand>();
            commands.Add("exit", new Exit());
        }
        
    }
}
