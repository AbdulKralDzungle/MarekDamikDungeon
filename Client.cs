using MarekDamikDungeon.Interfaces;
using MarekDamikDungeon.Interfaces.Commands;

namespace MarekDamikDungeon;

public class Client
{
    private Dictionary<string, IGameCommand> commands;
    private Konzole konzole;
    public int Id { get; set; }
    public string Result { get; set; }
    
    public Client(int id)
    {
        this.Id = id;
        InitializeCommand();
    }
        
    /**
     * here are initialized all commands for the command design pattern
     */
    private void InitializeCommand()
    {
        konzole = new  Konzole();
        commands = new Dictionary<string, IGameCommand>();
        commands.Add("exit", new Exit());
        commands.Add("help", new Help());
        commands.Add("err", new Err());
        commands.Add("attack", new Attack());
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
        Result = konzole.Vypis($"{commands[args[0]].Info()}", map);
        return commands[args[0]].Exit();
    }
}