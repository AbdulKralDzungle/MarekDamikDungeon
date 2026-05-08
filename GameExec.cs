using MarekDamikDungeon.Interfaces;
using MarekDamikDungeon.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MarekDamikDungeon
{
    /**
     * this class contains core command design pattern
     * it handles all commands from player and
     */
    public class GameExec
    {
        private List<Client> clients;
        public Map Mapa { get; private set; }

        public GameExec()
        {
            InitializeMap();
            Initialize();
        }

        private void Initialize()
        {
            clients = new List<Client>();
        }

        private void InitializeMap()
        {
            Mapa = new Map();
        }

        public void Brodcast(string message)
        {
            Console.WriteLine("lamo" + clients.Count);
            foreach (Client c in clients)
            {
                c.SendMessage(message);
                Console.WriteLine(c.Id);
            }
        }

        public void AddClient(Client client)
        {
            clients.Add(client);
        }
    }
}