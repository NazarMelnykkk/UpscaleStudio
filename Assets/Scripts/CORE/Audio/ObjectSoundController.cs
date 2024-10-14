using UnityEngine;

public class ObjectSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private SoundType _type; 

    public void PlaySound(SoundConfig soundConfig)
    {
        References.Instance.AudioHandler.PlaySound(soundConfig, _audioSource);
    }

    private void OnEnable()
    {
        References.Instance.AudioHandler.VolumeValueChanged += ChangeSoundVolume;
    }

    private void OnDisable()
    {
        References.Instance.AudioHandler.VolumeValueChanged -= ChangeSoundVolume;
    }

    private void ChangeSoundVolume()
    {
        _audioSource.volume = References.Instance.AudioHandler.GetVolumeByType(_type);
    }
}
