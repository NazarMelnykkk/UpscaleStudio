using DG.Tweening;


public class ScaleAnimation : ScaleAnimationBase
{

    protected float _duration = 0.5f;


    protected virtual void OnEnable()
    {
       
    }

    protected virtual void OnDisable()
    {

    }


    protected virtual void Scale()
    {
        if (transform != null)
        {
            transform.DOScale(originalScale * _scaleFactor, _duration)
                     .SetEase(Ease.InOutSine)
                     .SetUpdate(true);

        }
    }

    protected virtual void UnScale()
    {
        if (transform != null)
        {
            transform.DOScale(originalScale, _duration)
                     .SetEase(Ease.InOutSine)
                     .SetUpdate(true);
        }
    }

}
