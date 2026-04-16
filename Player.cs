using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon
{
    /**
     * handles all player stats
     */
    internal class Player
    {
        private int _health;
        private int _maxHealth;
        private string _name;

        public string Name{ get => _name; set => _name = value; }
    }
}
