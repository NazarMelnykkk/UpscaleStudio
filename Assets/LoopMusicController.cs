using UnityEngine;

public class LoopMusicController : MonoBehaviour
{
    [SerializeField] private SoundConfig _soundConfig;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        PlaySound(_soundConfig);
    }

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
        _audioSource.volume = References.Instance.AudioHandler.GetVolumeByType(_soundConfig.Sound.Type);
    }
}
