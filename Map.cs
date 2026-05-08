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
        private Dictionary<int, Player> players;
        private int hracCount;
        
        

        public Map()
        {
            hracCount = 0;
            Initialize("idk tady budou takový ty <> věci");
        }

        private bool Initialize(string xmlData)
        {
            rooms = new List<Room>();
            players = new Dictionary<int, Player>();
            rooms.Add(new Room(xmlData, 0));    // zatim přidání pouze placeholder místnosti
            return true;
        }

        public bool zautocNa(int idHrace, string name) // bool povedlo se - projde hrace a prisery, pokud utocnik lepsi staty, ubere prisere, jenak ubere utocnikovy
        {
            //idhrace je id utocnika
            //name je jmeno na koho se utoci
            
            return false;
        }
        
        public Player GetPlayerByName(string name)
        {
            return null;
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

        public Player ContainsPlayer(int room,  string name)
        {
            return null;
        }
        
        public bool ContainEneme()
        {
            return false;
        }

        public int AddPlayer()
        {
            players.Add(hracCount, new Player());
            hracCount++;
            return hracCount - 1;
        }

    }
}
