using UnityEngine;
public class EnemyMoveController : ICommandHandler
{
    private CommandHandlerBase _commandHandlerBase;
    private CommandBase _command;

    private Vector3 _defaultPos;
    private GameObject _targetObject;
    private AnimationBaseController _controller;
    private float _triggerRange;
    private float _speed;

    private Transform _currentTransform { get { return _commandHandlerBase.DataUnitHolder.gameObject.transform; } }

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
        _defaultPos = _commandHandlerBase.DataUnitHolder.gameObject.transform.position;
        _speed = _commandHandlerBase.DataUnitHolder.Speed;
    }

    public void ProcessCommand(CommandBase command)
    {
        _command = command;
        Move();
    }

    private void Move()
    {
        float distanceToTarget = Vector3.Distance(_currentTransform.position, _targetObject.transform.position);

        _controller.Move();

        if (distanceToTarget <= _triggerRange)
        {

            _currentTransform.position = Vector3.MoveTowards(_currentTransform.position, _targetObject.transform.position, Time.deltaTime * _speed);

            if (distanceToTarget <= _commandHandlerBase.DataUnitHolder.AttackRange)
            {

                _command.IsComplete = true;
                _commandHandlerBase.SetCommand(new CommandBase(CommandType.Attack));
                return;
            }
        }
        else
        {
            _currentTransform.position = Vector3.MoveTowards(_currentTransform.position, _defaultPos, Time.deltaTime * _speed);

            if (Vector3.Distance(_currentTransform.position, _defaultPos) <= 0.1f)
            {
                _command.IsComplete = true;
                _commandHandlerBase.SetCommand(new CommandBase(CommandType.Idle));
                return;
            }
        }

        Vector3 directionToTarget = (_targetObject.transform.position - _currentTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        _currentTransform.rotation = Quaternion.RotateTowards(_currentTransform.rotation, lookRotation, Time.deltaTime * _commandHandlerBase.DataUnitHolder.RotateSpeed);
    }
}
