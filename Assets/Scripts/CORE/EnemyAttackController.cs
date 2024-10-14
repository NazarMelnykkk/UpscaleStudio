using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttackController : ICommandHandler
{
    private CommandHandlerBase _commandHandlerBase;
    private CommandBase _command;

    private GameObject _targetObject;
    private AnimationBaseController _controller;
    private float _attackCooldown;
    private float _lastAttackTime;
    private bool _isAttacking;

    public void Setup(CommandHandlerBase commandHandlerBase)
    {
        _commandHandlerBase = commandHandlerBase;
        Init();
    }

    private void Init()
    {
        _targetObject = _commandHandlerBase.DataUnitHolder.TargetObject;
        _controller = _commandHandlerBase.DataUnitHolder.AnimationBaseController;
        _attackCooldown = _commandHandlerBase.DataUnitHolder.AttackTime;
    }

    public void ProcessCommand(CommandBase command)
    {
        _command = command;
        AttackAsync();
    }

    private async void AttackAsync()
    {
        if (_isAttacking == true)
        {
            return;
        }

        float distanceToTarget = Vector3.Distance(_commandHandlerBase.DataUnitHolder.gameObject.transform.position, _targetObject.transform.position);

        if (distanceToTarget <= _commandHandlerBase.DataUnitHolder.AttackRange && Time.time >= _lastAttackTime + _attackCooldown)
        {
            if (!_isAttacking)
            {
                _isAttacking = true;

                _controller.Attack();

                References.Instance.AudioHandler.PlaySound(_commandHandlerBase.DataUnitHolder._soundConfig);
                await Task.Delay((int)_attackCooldown); 

                if (Vector3.Distance(_commandHandlerBase.DataUnitHolder.gameObject.transform.position, _targetObject.transform.position) <= _commandHandlerBase.DataUnitHolder.AttackRange)
                {
                    DealDamage();
                }

                _lastAttackTime = Time.time;
                _isAttacking = false;
            }
        }
        else
        {
            _command.IsComplete = true;
            _commandHandlerBase.SetCommand(new CommandBase(CommandType.Idle));
        }
    }

    private void DealDamage()
    {
        //var targetHealth = _targetObject.GetComponent<Health>();
       /* if (targetHealth != null)
        {
            targetHealth.TakeDamage(_commandHandlerBase.DataUnitHolder.AttackDamage);
        }*/
    }
}
