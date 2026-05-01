using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon.Interfaces
{
    /**
     * interface witch should be implemented by all enemies
     */
    internal interface IEnemy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }

        /**
         * how much damage the enemy deals to a player
         */
        public int Damage { get; set; }

        public string GetDamaged(int dmg);

        public bool DamagePlayer(Player player);

        /**
         * For enemy loading from files - to be written
         */
        public void LoadEnemy();



    }
}
