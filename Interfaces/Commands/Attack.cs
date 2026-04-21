using System.Windows.Input;

namespace MarekDamikDungeon.Interfaces.Commands;

internal class Attack : IGameCommand
{
    private int dmg;
    public bool Execute(string arg, Map map)
    {
        return true;
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
        return true;
    }
}
