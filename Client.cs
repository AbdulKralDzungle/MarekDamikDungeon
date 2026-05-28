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
    private bool clientConnect;
    private GameExec gameExec;
    private Logger log = new Logger();
    public Client(int id, TcpClient client, StreamReader reader, StreamWriter writer, GameExec gameExec)
    {
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
        if (!commands.ContainsKey(args[0]))
            args[0] = "err";
        Result = "Something went wrong";
        if (args.Length == 0) commands["err"].Execute(map);
        if (args.Length > 1) commands[args[0]].Execute(args[1], map, this);
        commands[args[0]].Execute(map);
        Result = konzole.Vypis($"{commands[args[0]].Info()}", map, Id);
        log.Log(commands[args[0]].Log(gameExec.Mapa.GetPlayer(Id)));
        return commands[args[0]].Exit();
    }

    public void login()
    {
        string result = " ";
        SendMessage("Welcome, please tell us your name adventurer: ");
    }
    
    public void ExecteLoop()
    {
        writer.WriteLine("Byl jsi pripojen");
        writer.Flush();
        string data = null;
        string[] args = null;
        string dataRecive = null;
        gameExec.Mapa.RenamePlayer(reader.ReadLine(), Id);
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