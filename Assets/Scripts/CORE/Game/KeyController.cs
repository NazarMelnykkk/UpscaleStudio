using System;
using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private DoorController _doorController;

    public static event Action OnKeyCollected;

    public void Interact()
    {
        References.Instance.AudioHandler.PlaySound(_soundConfig);
        OnKeyCollected?.Invoke();
        _doorController.OpenGrate();
        gameObject.SetActive(false);
    }
}
