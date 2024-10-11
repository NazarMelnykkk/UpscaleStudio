public class SettingControllerSound : SettingControllerBase
{
    public void ChangeVolume(SoundType soundType, float value)
    {
        switch (soundType)
        {
            case SoundType.UI:
                SetVolume(soundType, value);
                break;
            case SoundType.Music:
                SetVolume(soundType, value);
                break;
            case SoundType.Ambient:
                SetVolume(soundType, value);
                break;
            case SoundType.SFX:
                SetVolume(soundType, value);
                break;
        }
    }

    private void SetVolume(SoundType soundType, float value) 
    {
        References.Instance.AudioHandler.SetVolumeByType(soundType, value);
        
    }

    public float GetVolume(SoundType soundType)
    {
        return References.Instance.AudioHandler.GetVolumeByType(soundType);
    }
}
