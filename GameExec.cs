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
        public Map Mapa { get; private set; }

        public GameExec()
        {
            InitializeMap();
        }

        private void InitializeMap()
        {
            Mapa = new Map();
        }
    }
}