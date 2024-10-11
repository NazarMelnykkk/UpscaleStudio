using UnityEngine;

public class ScaleAnimationBase : MonoBehaviour
{
    [SerializeField] protected float _scaleFactor = 1.2f;

    [SerializeField] protected Vector3 originalScale = new Vector3(1, 1, 1);
    protected Coroutine _animateScaleCoroutine;
}
