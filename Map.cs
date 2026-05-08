using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MarekDamikDungeon.Interfaces;
using MarekDamikDungeon.Interfaces.Items;

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
            Initialize("Nacti to tady z CSVcka");
        }

        // potrebujeme vytahnout jednotlivy mistnosti z souboru
        // mistnosti budou fungovat tak ze budes mit mistnost, ta bude mit svoje id, a idecka mistnosti do kterejch se pude z ni dostat
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
            ContainsPlayer(players[idHrace].RoomId, name);
            
            return false;
        }

        /**
         * tohle se bude pouzivat aby to informovalo hrace co se prave ve hre deje
         * takze to bude vracet nejen stav hrace samotneho, ale i mistnosti ve ktere je
         */
        public string[] PlayerStatus(int idHrace)
        {
            // marku tady pls potrebujeme [] stringu, ktery bude obsahovat
            // [zivoty hrace, inventar hrace, nazev mistnosti, popis mistnosti, predmnety v mistnosti, protivnici v mistnosti, hraci v mistnosti]
            // tam kde je vic veci dej 1 string kde je "1. item jedna \n 2. item dva" ...
            return new string[] {"5", "1. item jedna", "idk", "idk", "1. item idk", "1. ban dlazek", "1. pavel"};
        }

        public IItem ExtractItem(string name, int room)
        {
            // tady potrebuju vratit item podle id mistnosti a jmena
            // pokud mistnost item s danym jmenem neobsahuje vrat null
            return new ExampleItem();
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

        public Player GetPlayer(int id)
        {
            return players[id];
        }

    }
}
