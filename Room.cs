using MarekDamikDungeon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon
{
    internal class Room
    {
        private int id;
        private List<IItem> items;
        private List<IEneme> enemes;

        public Room(string xml, int id) 
        {
            this.id = id;
        }
    }
}
