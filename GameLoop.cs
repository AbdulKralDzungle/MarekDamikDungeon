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
        public string Result { get; set; }

        public GameLoop()
        {
            Initialize();
        }

        private void Initialize()
        {
            commands = new Dictionary<string, IGameCommand>();
            commands.Add("exit", new Exit());
            commands.Add("help", new Help());
        }

        public bool CommandFromClient(string[] args)
        {
            if (commands.ContainsKey(args[0]))
            {
                commands[args[0]].Execute();
                Result = commands[args[0]].Info();
                return commands[args[0]].Exit();
            }
            return false;
        }
    }
}