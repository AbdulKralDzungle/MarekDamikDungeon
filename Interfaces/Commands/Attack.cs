using System.Windows.Input;

namespace MarekDamikDungeon.Interfaces.Commands;

internal class Attack : IGameCommand
{
    private int dmg;
    public bool Execute(string arg, Map map, Client client)
    {
        return map.zautocNa(client.Id, arg);
    }

    public bool Execute(Map map)
    {
        return false;
    }

    public string Info()
    {
        return $"you delt {dmg}";
    }

    public string Log(Player player)
    {
        return $"Player {player.Name} attacks dmg: {dmg} target: ";
    }

    bool IGameCommand.Exit()
    {
        return false;
    }
}
