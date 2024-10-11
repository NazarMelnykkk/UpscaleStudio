using UnityEngine;

[CreateAssetMenu(menuName = "Config/Scene")]
public class SceneConfig : ScriptableObject
{
    public string SceneName;
    public SceneField SceneField;
    public bool LoadingOnInitialization = false;
    public bool IsGameplayScene = false;
    public bool DontUnload = false;

    private void OnValidate()
    {
        if (SceneField != null)
        {
            SceneName = SceneField.SceneName;
        }
    }
}
