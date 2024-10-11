
using DG.Tweening;

/// <summary>
/// USE WITH CUSTOM BUTTON SCRIPT
/// </summary>
public class ButtonScaleAnimation : ScaleAnimation
{
    protected ButtonCustomBase _customBase;

    protected void Awake()
    {
        _customBase = GetComponent<ButtonCustomBase>();
    }

    protected override void OnEnable()
    {
        _customBase.OnButtonSelectEvent += Scale;
        _customBase.OnButtonDeselectEvent += UnScale;
    }

    protected override void OnDisable()
    {
        _customBase.OnButtonSelectEvent -= Scale;
        _customBase.OnButtonDeselectEvent -= UnScale;

        if (transform != null)
        {
            transform.DOKill();
        }
    }
}
