using UnityEngine;

public class AnimationBaseController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Idle()
    {
        _animator.SetBool("IsIdle", true);
        _animator.SetBool("IsMoving", false);
    }

    public void Move()
    {
        _animator.SetBool("IsMoving", true);
        _animator.SetBool("IsIdle", false);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
