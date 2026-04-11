using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarekDamikDungeon.Interfaces
{
    internal interface IGameCommand
    {
        public bool Execute();
        public bool Exit();
        public string Info();
        public string Log(Player player);
    }
}
