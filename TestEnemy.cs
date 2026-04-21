using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using MarekDamikDungeon.Interfaces;

namespace MarekDamikDungeon
{
    internal class TestEnemy : IEneme
    {
        /**
         * Field values etc. will be most likely loaded from files
         */
        private string _name;
        private string _description;
        private int _hp;
        private int _maxhp;
        private int _damage;
        private bool _dead;
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Hp { get => _hp; set => _hp = value; }
        public int MaxHp { get => _maxhp; set => _maxhp = value; }
        public int Damage { get => _damage; set => _damage = value; }

        public bool DamagePlayer(Player player)
        {
            if(Damage > 0)
            {
                player.GetDamaged(Damage);
                return true;
            } 
            return false;
        }

        public string GetDamaged(int dmg)
        {
            Hp -= dmg;
            _dead = Hp < 0;

            if (!_dead) 
            {
                return Name + " has been damaged! Their current health: " + Hp + "/" + MaxHp;
            }
            else
            {
                return Name + " has been defeated!";
            }
        }

        public void LoadEnemy()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Enemy: {Name}\n{Description}\nCurrent HP: {Hp}/{MaxHp}";
        }
    }
}
