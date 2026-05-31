using System.Windows.Input;

namespace MarekDamikDungeon.Interfaces.Commands;

internal class Attack : IGameCommand
{
    private bool trefa;
    public bool Execute(string arg, Map map, Client client)
    {
        trefa = map.zautocNa(client.Id, arg);
        return trefa;
    }

    public bool Execute(Map map)
    {
        return false;
    }

    public string Info()
    {
        if (trefa) return "you attacked your target";
        return "you could not find that target";
    }

    public string Log(Player player)
    {
        return $"Player {player.Name} attacks and is succesfull";
    }

    bool IGameCommand.Exit()
    {
        return false;
    }
}
