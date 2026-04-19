using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarekDamikDungeon.Interfaces;

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
        private bool _alive;

        private List<IItem> _inv;

        public string Name{ get => _name; set => _name = value; }

        public int Health{ get => _health; set => _health = value; }
        public bool Alive{ get => _alive; set => _alive = value; }
        public int MaxHealth { get => _maxHealth;}
        public List<IItem> Inv { get => _inv; set => _inv = value; }

        public Player()
        {
            Inv = new List<IItem>();
        } 

        public string Damaged(int dmg)
        {
            Health -= dmg;
            Alive = Health > 0;

            if (Alive)
            {
                return "You were damaged, your health is now " + Health + "/" + MaxHealth;
            }
            else
            {
                return "You have died.";
            }
        }


    }
}
