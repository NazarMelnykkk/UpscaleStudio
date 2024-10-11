using UnityEngine;

public class ButtonSceneTransition : ButtonCustomBase
{
    [SerializeField] private SceneConfig _sceneToTransition;

    public override async void Click()
    {
        base.Click();

        await References.Instance.SceneLoader.Transition(_sceneToTransition.SceneName, gameObject.scene.name);
        PlaySound();
    }

    private void PlaySound()
    {
        References.Instance.AudioHandler.PlaySound(SoundConstants.UICLICK_TYPE, SoundConstants.UICLICK);
    }
}
