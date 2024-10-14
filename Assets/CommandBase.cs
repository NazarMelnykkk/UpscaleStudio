using UnityEngine;

public class CommandBase
{
    public CommandType CommandType;

    public Vector3 Direction;

    public bool IsComplete;

    public CommandBase(CommandType commandType, Vector3 direction)
    {
        CommandType = commandType;
        Direction = direction;
    }

    public CommandBase(CommandType commandType)
    {
        CommandType = commandType;
    }

}
