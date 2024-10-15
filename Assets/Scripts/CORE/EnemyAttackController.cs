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

        float timeSinceLastAttack = Time.time - _lastAttackTime;
        if (timeSinceLastAttack < _attackCooldown)
        {
            _command.IsComplete = true;
            _commandHandlerBase.SetCommand(new CommandBase(CommandType.Move));
            return;
        }

        _isAttacking = true;
        _controller.Attack();
        References.Instance.AudioHandler.PlaySound(_commandHandlerBase.DataUnitHolder._soundConfig);

        await Task.Delay(1500); 

        if (Vector3.Distance(_commandHandlerBase.DataUnitHolder.gameObject.transform.position, _targetObject.transform.position) <= _commandHandlerBase.DataUnitHolder.AttackRange)
        {
            DealDamage();
        }

        _lastAttackTime = Time.time;

        await Task.Delay(2000);

        _isAttacking = false;

        if (Vector3.Distance(_commandHandlerBase.DataUnitHolder.gameObject.transform.position, _targetObject.transform.position) <= _commandHandlerBase.DataUnitHolder.AttackRange)
        {
            return;
        }

        _command.IsComplete = true;
        _commandHandlerBase.SetCommand(new CommandBase(CommandType.Move));
    }

    private void DealDamage()
    {
        var targetHealth = _targetObject.GetComponent<IDamageable>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(DamageType.Enemy);
        }
    }
}
