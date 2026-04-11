using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarekDamikDungeon
{
    internal class Map
    {
        private List<Room> rooms;
        private Dictionary<int, List<Player>> players;
        public Map() { }

        private bool Initialize(string xmlData)
        {
            rooms = new List<Room>();
            players = new Dictionary<int, List<Player>>();
            players.Add(0, new List<Player>()); // testovaci hrac zatim
            players[0].Add(new Player());
            rooms.Add(new Room(xmlData, 0));    // zatim přidání pouze placeholder místnosti
            return true;
        }
        public bool movePlayer()
        {
            return false;
        }
        public void updatePlayerPositions()
        {
            return;
        }

    }
}
