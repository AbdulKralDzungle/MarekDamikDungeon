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
        private int id;
        private List<IItem> items;
        private List<IEneme> enemes;
        
        public Room(string xml, int id) 
        {
            this.id = id;
        }
    }
}
