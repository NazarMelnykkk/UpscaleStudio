using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ScaleLoopAnimation : ScaleAnimationBase
{
    [SerializeField] private float _animationDuration = 1.0f;

    private void OnEnable()
    {
        _animateScaleCoroutine = StartCoroutine(AnimateScale());
    }

    private void OnDisable()
    {
        if (_animateScaleCoroutine != null)
        {
            StopCoroutine(_animateScaleCoroutine);
        }
    }
    private IEnumerator AnimateScale()
    {
        while (true)
        {
            transform.DOScale(originalScale * _scaleFactor, _animationDuration)
                .SetEase(Ease.InOutSine);

            yield return new WaitForSeconds(_animationDuration);

            transform.DOScale(originalScale, _animationDuration)
                .SetEase(Ease.InOutSine);

            yield return new WaitForSeconds(_animationDuration);
        }
    }
}
