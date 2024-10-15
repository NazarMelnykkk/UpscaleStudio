using UnityEngine;

public class AnimationBaseController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void Idle()
    {
        _animator.SetBool("IsMoving", false);
        _animator.SetBool("IsIdle", true);
    }

    public void Move()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsMoving", true);
    }

    public void Attack()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsMoving", false);
        _animator.SetTrigger("Attack");
    }
}
