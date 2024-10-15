using System;
using UnityEngine;

public class BaseHealthController : MonoBehaviour, IDamageable
{

    public event Action<DamageType> OnDieEvent;

    private bool _isDie = false;

    public void TakeDamage(DamageType type)
    {
        if (_isDie == false)
        {
            _isDie = true;
            OnDieEvent?.Invoke(type);
        }
    }
}
