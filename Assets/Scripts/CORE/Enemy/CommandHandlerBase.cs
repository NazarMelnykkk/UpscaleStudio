using UnityEngine;

public class CommandHandlerBase : MonoBehaviour
{
    [field: SerializeField] public DataUnitHolder DataUnitHolder { get; private set; }
    public CommandBase CurrentCommand;

    private EnemyIdleController _enemyIdleController;
    private EnemyMoveController _enemyMoveController;
    private EnemyAttackController _enemyAttackController;

    private void Start()
    {
        SetupCommands();
    }

    private void SetupCommands()
    {
        _enemyIdleController = new EnemyIdleController();
        _enemyIdleController.Setup(this);

        _enemyMoveController = new EnemyMoveController();
        _enemyMoveController.Setup(this);

        _enemyAttackController = new EnemyAttackController();
        _enemyAttackController.Setup(this);

        SetCommand(new CommandBase(CommandType.Idle));
    }

    public void SetCommand(CommandBase newCommand)
    {
        CurrentCommand = newCommand;
    }

    private void FixedUpdate()
    {
        if (CurrentCommand != null)
        {
            ProcessCommand(CurrentCommand);
        }
    }

    private void ProcessCommand(CommandBase CommandBase)
    {
        switch (CommandBase.CommandType)
        {
            case CommandType.Idle:
                ProcessIdleCommand();
                break;
            case CommandType.Move:
                ProcessMoveCommand();
                break;
            case CommandType.Attack:
                ProcessAttackCommand();
                break;
        }

        if (CurrentCommand.IsComplete == true)
        {
            CompleteCommand();
        }
    }

    private void CompleteCommand()
    {
        CurrentCommand = null;
    }
    private void ProcessIdleCommand()
    {
        _enemyIdleController.ProcessCommand(CurrentCommand);
    }

    private void ProcessMoveCommand()
    {
        _enemyMoveController.ProcessCommand(CurrentCommand);
    }

    private void ProcessAttackCommand()
    {
        _enemyAttackController.ProcessCommand(CurrentCommand);
    }


    public CommandType GetCurrentCommandType()
    {
        if (CurrentCommand == null)
        {
            return CommandType.None;
        }

        return CurrentCommand.CommandType;
    }
}
