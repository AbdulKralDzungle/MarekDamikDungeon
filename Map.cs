using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MarekDamikDungeon.Interfaces;
using MarekDamikDungeon.Interfaces.Enemyse;
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

        internal List<Room> Rooms { get => rooms;}

        public Map()
        {
            hracCount = 0;
            //soubor je v bin/Debug/net8.0/Resources/MapTest.txt
            if (Initialize("Resources/MapTest.txt")) Console.WriteLine("Map loaded properly");
            foreach (Room r in rooms)
            {
                r.Items.Add(new ExampleItem());
                r.Enemes.Add(new ExampleEnemy());
            }
        }

        // potrebujeme vytahnout jednotlivy mistnosti z souboru
        // mistnosti budou fungovat tak ze budes mit mistnost, ta bude mit svoje id, a idecka mistnosti do kterejch se pude z ni dostat
        private bool Initialize(string data)
        {
            rooms = new List<Room>();
            players = new Dictionary<int, Player>();
            //rooms.Add(new Room(data, 0));    // zatim přidání pouze placeholder místnosti

            string line;
            try
            {
                StreamReader sr = new StreamReader(data);
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
                    //Console.WriteLine(lines[2]);
                    for (int i = 0; i < conRoomsFile.Length; i++)
                    {
                        connectedRooms.Add(int.Parse(conRoomsFile[i]));
                    }
                    string description = lines[3];
                    Room room = new Room(id, name, connectedRooms, description);
                    Rooms.Add(room);
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
            Player attackedPlayer = ContainsPlayer(players[idHrace].RoomId, name);

            if (attackedPlayer != null)
            {
                if(GetPlayer(idHrace).Defense > attackedPlayer.Defense)
                {
                    attackedPlayer.GetDamaged(GetPlayer(idHrace).Attack);
                    return true;
                }
                else
                {
                    GetPlayer(idHrace).GetDamaged(attackedPlayer.Attack);
                    return true;
                }
            }
            else if(ContainEneme(players[idHrace].RoomId, name))
            {
                IEnemy enemy = null;

                for (int i = 0; i < Rooms[players[idHrace].RoomId].Enemes.Count; i++)
                {
                    if (Rooms[players[idHrace].RoomId].Enemes[i].Name == name)
                    {
                        enemy = Rooms[players[idHrace].RoomId].Enemes[i];
                    }
                }
                if(enemy != null)
                {
                    if (GetPlayer(idHrace).Defense > enemy.Defense)
                    {
                        enemy.ChangeHelth(GetPlayer(idHrace).Attack);
                        return true;
                    }
                    else
                    {
                        GetPlayer(idHrace).GetDamaged(enemy.Damage);
                        return true;
                    }
                }
            }
            return false;
        }

        /**
         * tohle se bude pouzivat aby to informovalo hrace co se prave ve hre deje
         * takze to bude vracet nejen stav hrace samotneho, ale i mistnosti ve ktere je
         */
        public string[] PlayerStatus(int idHrace)
        {
            if (players.ContainsKey(idHrace))
            {
                string[] status = new string[8];
                status[0] = "You, mighty knight of name " + GetPlayer(idHrace).Name + " are under these circumstances:" + "\n";
                status[0] += "Your helth: " + GetPlayer(idHrace).Health + "\n";
                status[0] += "Tour defense: " + GetPlayer(idHrace).Defense + "\n";
                status[0] += "Tour attack: " + GetPlayer(idHrace).Attack+ "\n";
                int idInv = 1;

                status[3] = "Items in your inventory: ";
                if(GetPlayer(idHrace).Inv.Count == 0) 
                    status[3] += "no items in your inventory";
                foreach(IItem item in GetPlayer(idHrace).Inv)
                {
                    status[1] += idInv + " " + item.Name + "\n";
                    idInv++;
                }

                status[2] = Rooms[GetPlayer(idHrace).RoomId].Name;
                status[3] = Rooms[GetPlayer(idHrace).RoomId].Description;
                
                int idItems = 1;

                status[4] = "Items in room: ";
                if(Rooms[GetPlayer(idHrace).RoomId].Items.Count == 0)
                    status[4] += "no items in this room";
                foreach (IItem item in Rooms[GetPlayer(idHrace).RoomId].Items)
                {
                    status[4] += idItems + " " + item.Name + "\n";
                    idItems++;
                }

                int idEnemies = 1;
                status[5] = "Monsters in this room: ";
                if(Rooms[GetPlayer(idHrace).RoomId].Enemes.Count == 0) 
                    status[5] += "No monsters in this room";
                foreach (IEnemy enemy in Rooms[GetPlayer(idHrace).RoomId].Enemes)
                {
                    status[5] += idEnemies + " " + enemy.Name + "\n";
                    idEnemies++;
                }

                int idPlayers = 1;

                status[6] = "Players in this room: ";
                if(players.Count == 0) 
                    status[6] += "No players in this room";
                for(int i = 0; i < players.Count; i++)
                {
                    if(GetPlayer(i).RoomId == GetPlayer(idHrace).RoomId && GetPlayer(i) != GetPlayer(idHrace)) 
                        status[6] += idPlayers + " " + GetPlayer(i).Name + "\n";
                    idPlayers++;
                }

                status[7] = "You can walk to: ";
                if( rooms[GetPlayer(idHrace).RoomId].CanWallkToIds.Count == 0) 
                    status[7] += "Somehow you got softlocked...";
                foreach (int i in rooms[GetPlayer(idHrace).RoomId].CanWallkToIds)
                {
                    status[7] += rooms[i].Name + " ";
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
            for (int i = 0; i < Rooms[room].Enemies().Count; i++)
            {
                output += $"1. {Rooms[room].Enemies()[i].Name} \n";
            }
            return output;
        }

        public Player ContainsPlayer(int room,  string name)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Name.ToLower() == name.ToLower() && players[i].RoomId == room)
                {
                    return players[i];
                }
            }
            return null;
        }
        
        public bool ContainEneme(int room, string name)
        {
            for (int i = 0; i < Rooms[room].Enemes.Count; i++)
            {
                if (Rooms[room].Enemes[i].Name.ToLower() == name.ToLower())
                {
                    return true;
                }
            }
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
            for (int i = 0; i < rooms.Count; i ++)
            {
                if (rooms[i].Name.ToLower() == room.ToLower())
                {
                    Console.WriteLine($"room name {rooms[i].Name}");
                    if (rooms[players[playerId].RoomId].CanWalkTo(i))
                    {
                        Console.WriteLine("hrac dosel");
                        players[playerId].RoomId = i;
                        return true;
                    }
                }
            }
            return false;
        }

        public void RenamePlayer(string? name, int id)
        {
            try
            {
                if (name != null)
                {
                    players[id].Name = name;
                    return;
                }
                players[id].Name = "SirSpatneSePojmenoval" + id ;
            }
            catch (Exception e)
            {
                // idk, tohle by nemělo nastat, a pokud jo tak se bude prostě ignorovat
            }
        }
    }
}
