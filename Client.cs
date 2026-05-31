using System.Net;
using System.Net.Sockets;
using System.Text;
using MarekDamikDungeon.Interfaces;
using MarekDamikDungeon.Interfaces.Commands;

namespace MarekDamikDungeon;

public class Client
{
    private Dictionary<string, IGameCommand> commands;
    private Konzole konzole;
    public int Id { get; set; }
    public string Result { get; set; }
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;
    private AccountMang accs;
    private bool clientConnect;
    private GameExec gameExec;
    private Logger log = new Logger();
    public Client(int id, TcpClient client, StreamReader reader, StreamWriter writer, GameExec gameExec)
    {
        accs = new AccountMang();
        this.client = client;
        this.clientConnect = true;
        this.gameExec = gameExec;
        this.gameExec.AddClient(this);
        this.reader = reader;
        this.writer = writer;
        log = new Logger();
        this.Id = id;
        InitializeCommand();
        ExecteLoop();
    }
        
    /**
     * here are initialized all commands for the command design pattern
     */
    private void InitializeCommand()
    {
        konzole = new  Konzole();
        commands = new Dictionary<string, IGameCommand>();
        commands.Add("exit", new Exit());
        commands.Add("pick", new Pick());
        commands.Add("help", new Help());
        commands.Add("walk", new Walk());
        commands.Add("err", new Err());
        commands.Add("attack", new Attack());
        commands.Add("shout", new Shout());
    }
    /**
     * this method handles input from player
     * @param args is pole where [0] is command and all other values are arguments for the given command
     * @return bool true if method ended program false otherwise
     */
    public bool CommandFromClient(string[] args, Map map)
    {
        if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
        {
            args = new[] { "err" };
        }

        if (!commands.ContainsKey(args[0]))
            args[0] = "err";

        Result = "Something went wrong";
        if (args.Length > 1)
        {
            string commandArgument = string.Join(" ", args.Skip(1));
            commands[args[0]].Execute(commandArgument, map, this);
        }
        else
        {
            commands[args[0]].Execute(map);
        }
        Result = konzole.Vypis($"{commands[args[0]].Info()}", map, Id);
        log.Log(commands[args[0]].Log(gameExec.Mapa.GetPlayer(Id)));
        return commands[args[0]].Exit();
    }

    public void login()
    {
        SendMessage("1. to login, 2. to register");
        writer.Flush();
        string result = " ";
        bool registered;
        int i;
        do
        {
            registered = true;
            result = reader.ReadLine();
            try
            {
                i = Int32.Parse(result);
                if (i == 1)
                {
                    SendMessage("Welcome, please tell us your name adventurer: ");
                    writer.Flush();
                    string? name = reader.ReadLine();
                    SendMessage("Please tell us your password: (dramatic pause)");
                    writer.Flush();
                    string? password = reader.ReadLine();
                    if (name == null || password == null)
                    {
                        registered = false;
                        SendMessage("Name or password was empty, try again");
                        writer.Flush();
                    }
                    else
                    {
                        accs.LoadPlayer(name, password,gameExec.Mapa.GetPlayer(Id));
                        SendMessage("Logged in");
                        writer.Flush();
                    }
                }else if (i == 2)
                {
                    SendMessage("Welcome, please tell us your new name: ");
                    writer.Flush();
                    string? name = reader.ReadLine();
                    SendMessage("Please make your password: ");
                    writer.Flush();
                    string? password = reader.ReadLine();
                    if (name == null || password == null)
                    {
                        registered = false;
                        SendMessage("Name or password was empty, try again");
                        writer.Flush();
                    }
                    else
                    {
                        SendMessage("Account created");
                        SendMessage($"Name: {gameExec.Mapa.GetPlayer(Id).Name}");
                        accs.RegisterPlayer(name, password, gameExec.Mapa.GetPlayer(Id));
                        writer.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                registered = false;
            }
        } while (!registered);
    }
    
    public void ExecteLoop()
    {
        writer.WriteLine("Byl jsi pripojen");
        writer.Flush();
        string data = null;
        string[] args = null;
        string dataRecive = null;
        login();
        clientConnect = !CommandFromClient(new []{"help"}, gameExec.Mapa);
        SendMessage(Result);
        while (clientConnect)
        {
            try
            {
                data = reader.ReadLine();
                data = data.ToLower();
                args = data.Split(' ');
                clientConnect = !CommandFromClient(args, gameExec.Mapa);
            }
            catch(Exception e)
            {
                SendMessage("some unexpected error happened\n please try restarting game or contact creator of this game \n error message");
                log.Log("some unexpected error happened\n please try restarting game or contact creator of this game \n error message: " + e.Message);
            }
            SendMessage(Result);
        }
        writer.Flush();
        accs.SavePlayer(gameExec.Mapa.GetPlayer(Id));
        client.Close();
    }

    public void SendMessage(string message)
    {
        try
        {
            writer.WriteLine(message);
            writer.Flush();
            Console.WriteLine("sent");
        }
        catch (Exception e)
        {
            if (client.Connected == false)
            {
                clientConnect = false;
                return;
            }
            log.Log("stream writer error, error message: \n " + e.Message);
        }
    }
    public void Brodcast(string message)
    {
        gameExec.Brodcast(message);
        Console.WriteLine("brodcasted");
    }

}
