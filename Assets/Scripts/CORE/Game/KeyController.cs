using UnityEngine;

public class KeyController : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private DoorController _doorController;

    public void Interact()
    {
        References.Instance.AudioHandler.PlaySound(_soundConfig);
        _doorController.OpenGrate();
        gameObject.SetActive(false);
    }
}
