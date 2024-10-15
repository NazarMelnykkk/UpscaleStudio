using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private DamageType _damageType = DamageType.Fire; 
    [SerializeField] private LayerMask _targetLayer;

    private void OnTriggerEnter(Collider other)
    {

        if ((_targetLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            BaseHealthController healthController = other.GetComponent<BaseHealthController>();
            if (healthController != null)
            {
                healthController.TakeDamage(_damageType);
            }
        }
    }
}
