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
    private GameExec gameExec;
    private Logger log = new Logger();
    public Client(int id, TcpClient client, StreamReader reader, StreamWriter writer, GameExec gameExec)
    {
        this.client = client;
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
        if (args.Length > 1) commands[args[0]].Execute(args[1], map, this);
        commands[args[0]].Execute(map);
        Result = konzole.Vypis($"{commands[args[0]].Info()}", map, Id);
        log.Log(commands[args[0]].Log(gameExec.Mapa.GetPlayer(Id)));
        return commands[args[0]].Exit();
    }

    public void ExecteLoop()
    {
        writer.WriteLine("Byl jsi pripojen");
        writer.Flush();
        bool clientConnect = true;
        string data = null;
        string[] args = null;
        string dataRecive = null;
        while (clientConnect)
        {
            data = reader.ReadLine();
            data = data.ToLower();
            args = data.Split(' ');
            clientConnect = !CommandFromClient(args, gameExec.Mapa);
            SendMessage(Result);
        }
        writer.Flush();
        client.Close();
    }

    public void SendMessage(string message)
    {
        writer.WriteLine(message);
        writer.Flush();
        Console.WriteLine("sent");
    }
    public void Brodcast(string message)
    {
        gameExec.Brodcast(message);
        Console.WriteLine("brodcasted");
    }

}