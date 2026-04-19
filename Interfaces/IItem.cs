using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon.Interfaces
{
    /**
     * interface witch should be implemented by all items
     */
    internal interface IItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfUses { get; set; }
        public void Effect();
    }
}
