
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
            if (Initialize("MapTest.txt")) Console.WriteLine("Map loaded properly");
        }

        // potrebujeme vytahnout jednotlivy mistnosti z souboru
        // mistnosti budou fungovat tak ze budes mit mistnost, ta bude mit svoje id, a idecka mistnosti do kterejch se pude z ni dostat
        private bool Initialize(string data)
        {
            rooms = new List<Room>();
            players = new Dictionary<int, Player>();
            string mapPath = FindResourcePath(data);

            try
            {
                using StreamReader sr = new StreamReader(mapPath);
                string? line = sr.ReadLine();

                while (line != null)
                {
                    //file ve formatu:
                    //id;name;idKamMuze1-idKamMuze2-idKamMuze3;popisMistnosti;item1,item2;enemy1,enemy2;

                    string[] lines = line.Split(";");
                    if (lines.Length < 4)
                    {
                        line = sr.ReadLine();
                        continue;
                    }

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
                    LoadItemsToRoom(room, lines.Length > 4 ? lines[4] : "");
                    LoadEnemiesToRoom(room, lines.Length > 5 ? lines[5] : "");
                    Rooms.Add(room);
                    line = sr.ReadLine();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Map loading failed: " + e.Message);
                return false;
            }
        }

        private string FindResourcePath(string fileName)
        {
            string? projectResourcePath = FindProjectResourcePath(fileName);
            if (projectResourcePath != null)
            {
                return projectResourcePath;
            }

            string[] pathsToTry =
            {
                Path.Combine("Resources", fileName),
                Path.Combine(Directory.GetCurrentDirectory(), "Resources", fileName),
                Path.Combine(AppContext.BaseDirectory, "Resources", fileName),
            };

            foreach (string path in pathsToTry)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null)
            {
                string path = Path.Combine(directory.FullName, "Resources", fileName);
                if (File.Exists(path))
                {
                    return path;
                }
                directory = directory.Parent;
            }

            return Path.Combine("Resources", fileName);
        }

        private string? FindProjectResourcePath(string fileName)
        {
            List<string> startingPaths = new List<string>
            {
                Directory.GetCurrentDirectory(),
                AppContext.BaseDirectory
            };

            foreach (string startingPath in startingPaths)
            {
                DirectoryInfo? directory = new DirectoryInfo(startingPath);
                while (directory != null)
                {
                    if (File.Exists(Path.Combine(directory.FullName, "MarekDamikDungeon.csproj")))
                    {
                        string path = Path.Combine(directory.FullName, "Resources", fileName);
                        if (File.Exists(path))
                        {
                            return path;
                        }
                    }
                    directory = directory.Parent;
                }
            }

            return null;
        }

        private void LoadItemsToRoom(Room room, string data)
        {
            foreach (string itemName in SplitResourceList(data))
            {
                IItem? item = CreateItem(itemName);
                if (item != null)
                {
                    room.Items.Add(item);
                }
            }
        }

        private void LoadEnemiesToRoom(Room room, string data)
        {
            foreach (string enemyName in SplitResourceList(data))
            {
                IEnemy? enemy = CreateEnemy(enemyName);
                if (enemy != null)
                {
                    room.Enemes.Add(enemy);
                }
            }
        }

        private List<string> SplitResourceList(string data)
        {
            return data.Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }

        private IItem? CreateItem(string name)
        {
            switch (name.ToLower())
            {
                case "healthpotion":
                case "health potion":
                    return new HealthPotion();
                case "rustysword":
                case "rusty sword":
                    return new RustySword();
                case "armor":
                    return new Armor();
                default:
                    return null;
            }
        }

        private IEnemy? CreateEnemy(string name)
        {
            switch (name.ToLower())
            {
                case "orc":
                    return new Orc();
                case "skeleton":
                    return new Skeleton();
                case "slime":
                    return new Slime();
                default:
                    return null;
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
                        bool enemyStillAlive = enemy.ChangeHelth(GetPlayer(idHrace).Attack);
                        if (!enemyStillAlive)
                        {
                            Rooms[players[idHrace].RoomId].Enemes.Remove(enemy);
                        }
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

                status[1] = "Items in your inventory: ";
                if(GetPlayer(idHrace).Inv.Count == 0) 
                    status[1] += "no items in your inventory";
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
            for (int i = 0; i < Rooms[room].Items.Count; i++)
            {
                if (Rooms[room].Items[i].Name.ToLower() == name.ToLower())
                {
                    IItem item = Rooms[room].Items[i];
                    Rooms[room].Items.RemoveAt(i);
                    return item;
                }
            }
            return null;
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
