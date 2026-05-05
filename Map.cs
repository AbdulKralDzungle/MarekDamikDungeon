using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MarekDamikDungeon.Interfaces;

namespace MarekDamikDungeon
{
    /**
     * contains all information about the game world in form of room list
     * handles movement of players
     */
    public class Map
    {
        private List<Room> rooms;
        private Dictionary<int, List<Player>> players;

        public Map()
        {
            Initialize("idk tady budou takový ty <> věci");
        }

        private bool Initialize(string xmlData)
        {
            rooms = new List<Room>();
            players = new Dictionary<int, List<Player>>();
            players.Add(0, new List<Player>()); // testovaci hrac zatim
            players[0].Add(new Player());
            rooms.Add(new Room(xmlData, 0));    // zatim přidání pouze placeholder místnosti
            return true;
        }

        public Player GetPlayerByName(string name)
        {
            return null;
        }
        
        public bool MovePlayer()
        {
            return false;
        }
        public void UpdatePlayerPositions()
        {
            return;
        }

        public string EnemeNames(int room)
        {
            string output = "";
            foreach (IEnemy e in rooms[room].Enemies())
            {
                output += $"{e.Name} ";
            }
            return output;
        }

        public bool ContainEneme()
        {
            return false;
        }

    }
}
