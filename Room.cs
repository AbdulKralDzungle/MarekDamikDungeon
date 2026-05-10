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
        public string Description { get => description; set => description = value; }
        internal List<IEnemy> Enemes { get => enemes; set => enemes = value; }
        public List<IItem> Items { get => items; set => items = value; }
        public List<int> CanWallkToIds { get => canWallkToIds; set => canWallkToIds = value; }

        private int id;
        private string name;
        private List<IItem> items;
        private List<IEnemy> enemes;
        private List<int> canWallkToIds; // tady jsou ids vsech mistnosti do kterych se bude hrac moct dostat s teto mistnosti

        private string description;
        
        public Room(string xml, int id)
        {
            canWallkToIds = new List<int>();
            Enemes = new List<IEnemy>();
            Items = new List<IItem>();
            Name = "nameIDK"; // nacte se z CSV snad
            this.id = id;
        }

        public Room(int id, string name, List<int> canWallkToIds, string description)
        {
            Id = id;
            Name = name;
            this.canWallkToIds = canWallkToIds;
            Description = description;
            Enemes = new List<IEnemy>();
            Items = new List<IItem>();
        }

        public bool CanWalkTo(int id)
        {
            bool canWalkTo = false;
            foreach (int i in canWallkToIds)
            {
                if (id == i)
                {
                    canWalkTo = true;
                    Console.WriteLine("can Walk To");
                }
            }
            return canWalkTo;
        }
        public List<IEnemy> Enemies()
        {
            return Enemes;
        }

        public virtual string ToString()
        {
            string temprooms = "";
            for(int i = 0; i < canWallkToIds.Count; i++)
            {
                temprooms = canWallkToIds[i] + " ";
            }
            return $"{Id}, {Name}, {Description}, {temprooms}";
        }
    }
}
