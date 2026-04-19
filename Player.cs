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
        private int _invMax;

        public string Name{ get => _name; set => _name = value; }

        public int Health{ get => _health; set => _health = value; }
        public bool Alive{ get => _alive; set => _alive = value; }
        public int MaxHealth { get => _maxHealth;}
        public List<IItem> Inv { get => _inv; set => _inv = value; }

        public Player()
        {
            Inv = new List<IItem>();
        } 

        public string GetDamaged(int dmg)
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

        public bool AddItem(IItem item)
        {
            if(item == null) return false;
            if(Inv.Count < _invMax)
            {
                Inv.Add(item);
                return true;
            }
            return false;
        }

        public string GetDescOfItem(string name)
        {
            foreach (var item in Inv)
            {
                if (name.Equals(item.Name))
                {
                    return item.Description;
                }
            }

            return "Item not found";
        }

        public string InvToString()
        {
            string text = $"Inventory ({Inv.Count}/{_invMax}):\n";
            foreach(IItem item in Inv)
            {
                text += $"[{item.Name}] ";
            }
            return text;
        }


    }
}
