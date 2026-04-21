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
    /**
     * this class contains core command design pattern
     * it handles all commands from player and
     */
    internal class GameExec
    {
        private Dictionary<string, IGameCommand> commands;
        private Map map;
        public string Result { get; set; }

        public GameExec()
        {
            InitializeCommand();
        }
        
        /**
         * here are initialized all commands for the command design pattern
         */
        private void InitializeCommand()
        {
            commands = new Dictionary<string, IGameCommand>();
            commands.Add("exit", new Exit());
            commands.Add("help", new Help());
        }

        private void InitializeMap()
        {
            map = new Map();
        }
        
        /**
         * this method handles input from player
         * @param args is pole where [0] is command and all other values are arguments for the given command
         * @return bool true if method ended program false otherwise
         */
        public bool CommandFromClient(string[] args)
        {
            if (commands.ContainsKey(args[0]))
            {
                commands[args[0]].Execute(args[1], map);
                Result = commands[args[0]].Info();
                return commands[args[0]].Exit();
            }
            return false;
        }
    }
}