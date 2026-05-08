using MarekDamikDungeon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon
{
    
    /**
     * for now empty
     * will be decided how will work in future
     * marku delej si s tady tim co chces I guess
     * kdo to bude mit na starosti se rozhodne v pondeli
     */
    internal class Room
    {
        public int Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(value)); }

        private int id;
        private string name;
        private List<IItem> items;
        private List<IEnemy> enemes;
        private List<int> canWallkToIds; // tady jsou ids vsech mistnosti do kterych se bude hrac moct dostat s teto mistnosti
        
        public Room(string xml, int id)
        {
            canWallkToIds = new List<int>();
            enemes = new List<IEnemy>();
            items = new List<IItem>();
            Name = "nameIDK"; // nacte se z CSV snad
            this.id = id;
        }

        public bool canWalkTo(int id)
        {
            // return canWallkToIds.Contains(id);
            // az bude vyreseny nacitani mistnosti...
            return true;
        }
        public List<IEnemy> Enemies()
        {
            return enemes;
        }
    }
}
