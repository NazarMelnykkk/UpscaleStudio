using UnityEngine;

public class EnemyIdleController : ICommandHandler
{
    private CommandHandlerBase _commandHandlerBase;
    private CommandBase _command;

    private GameObject _targetObject;
    private AnimationBaseController _controller;
    private float _triggerRange;

    public void Setup(CommandHandlerBase commandHandlerBase)
    {
        _commandHandlerBase = commandHandlerBase;
        Init();
    }

    private void Init()
    {
        _targetObject = _commandHandlerBase.DataUnitHolder.TargetObject;
        _controller = _commandHandlerBase.DataUnitHolder.AnimationBaseController;
        _triggerRange = _commandHandlerBase.DataUnitHolder.TriggerDistance;
    }

    public void ProcessCommand(CommandBase command)
    {
        _command = command;
        Idle();
    }

    private void Idle()
    {
        _controller.Idle();

        if (_triggerRange >= CalculatedDistance())
        {
            References.Instance.AudioHandler.PlaySound(_commandHandlerBase.DataUnitHolder._soundConfig);
            _command.IsComplete = true;
            _commandHandlerBase.SetCommand(new CommandBase(CommandType.Move));
            return;
        }
    }

    private float CalculatedDistance()
    {
        return Vector3.Distance(_commandHandlerBase.DataUnitHolder.gameObject.transform.position, _targetObject.gameObject.transform.position);
    }
}
