using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// basic component for a button
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonCustomBase : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    protected Button Button;

    public event Action OnButtonClickEvent;

    public event Action OnButtonSelectEvent;
    public event Action OnButtonDeselectEvent;

    protected virtual void Awake()
    {
        Button = GetComponent<Button>();
    }

    protected virtual void OnEnable()
    {
        Button.onClick.AddListener(Click);
    }

    protected virtual void OnDisable()
    {
        Button.onClick.RemoveListener(Click);
    }

    public virtual void Click()
    {
        OnButtonClickEvent?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonSelectEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnButtonDeselectEvent?.Invoke();
    }
}
