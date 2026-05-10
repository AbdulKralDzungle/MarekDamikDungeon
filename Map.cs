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
            //rooms.Add(new Room(xmlData, 0));    // zatim přidání pouze placeholder místnosti

            string line;
            try
            {
                StreamReader sr = new StreamReader(xmlData);
                line = sr.ReadLine();

                while (line != null)
                {
                    //file ve formatu:
                    //id;name;idKamMuze1-idKamMuze2-idKamMuze3;popisMistnosti;

                    string[] lines = line.Split(";");
                    int id = int.Parse(lines[0]);
                    string name = lines[1];
                    List<int> connectedRooms = new List<int>();

                    string[] conRoomsFile = lines[2].Split("-");
                    for (int i = 0; i < connectedRooms.Count; i++)
                    {
                        connectedRooms.Add(int.Parse(conRoomsFile[i]));
                    }

                    string description = lines[3];

                    Room room = new Room(id, name, connectedRooms, description);
                    rooms.Add(room);

                    line = sr.ReadLine();
                }

                sr.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool zautocNa(int idHrace, string name) // bool povedlo se - projde hrace a prisery, pokud utocnik lepsi staty, ubere prisere, jenak ubere utocnikovy
        {
            //idhrace je id utocnika
            //name je jmeno na koho se utoci
            ContainsPlayer(players[idHrace].RoomId, name);
            // pokud ne tak
            ContainEneme(); // <- jo ta metoda taky zatim neni...
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

            if (players.ContainsKey(idHrace))
            {
                string[] status = new string[7];
                status[0] = GetPlayer(idHrace).Health.ToString();
                int idInv = 1;

                foreach(IItem item in GetPlayer(idHrace).Inv)
                {
                    status[1] = idInv + item.Name + "\n";
                    idInv++;
                }

                status[2] = rooms[GetPlayer(idHrace).RoomId].Name;
                status[3] = rooms[GetPlayer(idHrace).RoomId].Description;

                int idItems = 1;

                foreach (IItem item in rooms[GetPlayer(idHrace).RoomId].Items)
                {
                    status[4] = idItems + item.Name + "\n";
                    idItems++;
                }

                int idEnemies = 1;

                foreach (IEnemy enemy in rooms[GetPlayer(idHrace).RoomId].Enemes)
                {
                    status[5] = idEnemies + enemy.Name + "\n";
                    idEnemies++;
                }

                int idPlayers = 1;

                for(int i = 0; i < players.Count; i++)
                {
                    if(GetPlayer(i).RoomId == rooms[GetPlayer(idHrace).RoomId].Id && GetPlayer(i) != GetPlayer(idHrace))
                    status[6] = idPlayers + GetPlayer(i).Name + "\n";
                    idPlayers++;
                }

                EnemeNames(players[idHrace].RoomId); // <- pro enemaky to tady jakstaks je
                return status;
            }
            return null;
        }

        public IItem ExtractItem(string name, int room)
        {
            // tady potrebuju vratit item podle id mistnosti a jmena
            // pokud mistnost item s danym jmenem neobsahuje vrat null
            return new ExampleItem();
        }

        public string EnemeNames(int room)
        {
            string output = "";
            for (int i = 0; i < rooms[room].Enemies().Count; i++)
            {
                output += $"1. {rooms[room].Enemies()[i].Name} \n";
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

        public bool WalkPlayer(int playerId, string room)
        {
            foreach (Room r in rooms)
            {
                if (r.Name == room)
                {
                    if (rooms[players[playerId].RoomId].canWalkTo(r.Id))
                    {
                        players[playerId].RoomId = r.Id;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
