using System;
using System.Collections.Generic;
using UnityEngine;
using static SoundsData;


public class AudioHandler : MonoBehaviour, IDataPersistence
{
    [Header("AUDIO SOURCE")]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _SFXAudioSource;
    [SerializeField] private AudioSource _ambientAudioSource;

    [Header("SOUNDS")]
    [SerializeField] private SoundConfig[] _ui;
    [SerializeField] private SoundConfig[] _musics;
    [SerializeField] private SoundConfig[] _SFX;
    [SerializeField] private SoundConfig[] _ambients;

    [Header("KEYS")]
    private Dictionary<string, SoundConfig> _soundDictionary;


    private void Awake()
    {
        InitializeSoundDictionary();
    }

    private void InitializeSoundDictionary()
    {
        _soundDictionary = new Dictionary<string, SoundConfig>();

        AddSoundsToDictionary(_ui);
        AddSoundsToDictionary(_musics);
        AddSoundsToDictionary(_SFX);
        AddSoundsToDictionary(_ambients);
    }

    private void AddSoundsToDictionary(SoundConfig[] soundConfigs)
    {
        foreach (SoundConfig config in soundConfigs)
        {
            if (!_soundDictionary.ContainsKey(config.Sound.Name))
            {
                _soundDictionary.Add(config.Sound.Name, config);
            }
        }
    }

    public void PlaySound(SoundType type, string soundID)
    {
        if (_soundDictionary.TryGetValue(soundID, out SoundConfig soundConfig) && soundConfig.Sound.Type == type)
        {
            AudioSource audioSource = GetAudioSourceByType(type);
            if (audioSource != null)
            {
                SetRandomPitchValue(audioSource, soundConfig.Sound);
                audioSource.PlayOneShot(GetRandomClip(soundConfig.Sound.AudioClip));
            }
        }
        else
        {
            Debug.LogError($"Sound {soundID} of type {type} not found");
        }
    }

    public void PlaySound(SoundConfig config)
    {
        Sound currentSound = config.Sound;

        if (currentSound == null)
        {
            Debug.LogError($"Config {config.name} not found");
            return;
        }

        AudioSource audioSource = GetAudioSourceByType(currentSound.Type);
        if (audioSource != null)
        {
            SetRandomPitchValue(audioSource, currentSound);
            audioSource.PlayOneShot(GetRandomClip(currentSound.AudioClip));
        }
    }

    private void SetRandomPitchValue(AudioSource audioSource, Sound sound)
    {
        audioSource.pitch = UnityEngine.Random.Range(sound.MinPitch, sound.MaxPitch);
    }

    public float GetVolumeByType(SoundType type)
    {
        switch (type)
        {
            case SoundType.UI:
                return _uiAudioSource.volume;
            case SoundType.Music:
                return _musicAudioSource.volume;
            case SoundType.SFX:
                return _SFXAudioSource.volume;
            case SoundType.Ambient:
                return _ambientAudioSource.volume;
            default:
                Debug.LogError($"Unknown sound type {type}");
                return 0f;
        }
    }

    public void SetVolumeByType(SoundType type, float volume)
    {
        switch (type)
        {
            case SoundType.UI:
                _uiAudioSource.volume = volume;
                break;
            case SoundType.Music:
                _musicAudioSource.volume = volume;
                break;
            case SoundType.SFX:
                _SFXAudioSource.volume = volume;
                break;
            case SoundType.Ambient:
                _ambientAudioSource.volume = volume;
                break;
        }

        SaveByType(References.Instance.DataPersistenceHandlerBase.GameData, type, volume);
    }

    private AudioSource GetAudioSourceByType(SoundType type)
    {
        switch (type)
        {
            case SoundType.UI:
                return _uiAudioSource;
            case SoundType.Music:
                return _musicAudioSource;
            case SoundType.Ambient:
                return _ambientAudioSource;
            case SoundType.SFX:
                return _SFXAudioSource;
            default:
                Debug.LogError($"Unknown sound type {type}");
                return null;
        }
    }

    public AudioClip GetRandomClip(List<AudioClip> clips)
    {
        if (clips == null || clips.Count == 0)
        {
            Debug.LogError("The clip list is null or empty.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, clips.Count);
        return clips[randomIndex];
    }

    public void SaveData(GameData data)
    {
        if (data != null)
        {
            SoundData soundData;

            foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
            {
                string id = type.ToString();

                if (data.SoundsData.SoundVolumeData.ContainsKey(id) == true)
                {
                    data.SoundsData.SoundVolumeData.Remove(id);
                }

                soundData = new SoundData(GetVolumeByType(type));
                data.SoundsData.SoundVolumeData.Add(id, soundData);
            }
        }
    }

    public void SaveByType(GameData data, SoundType type, float volume)
    {
        if (data != null)
        {
            SoundData soundData;

            string id = type.ToString();

            if (data.SoundsData.SoundVolumeData.ContainsKey(id) == true)
            {
                data.SoundsData.SoundVolumeData.Remove(id);
            }

            soundData = new SoundData(volume);
            data.SoundsData.SoundVolumeData.Add(id, soundData);
        }
    }

    public void LoadData(GameData data)
    {
        if (data != null && data.SoundsData != null)
        {
            SoundData soundData;
            foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
            {
                string id = type.ToString();

                if (data.SoundsData.SoundVolumeData.TryGetValue(id, out soundData))
                {
                    SetVolumeByType(type, soundData.Volume);
                }
            }
        }
    }
}
