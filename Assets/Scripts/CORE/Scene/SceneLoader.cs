using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Scene")]
    [SerializeField] private List<SceneConfig> _scenes;   


    [Header("Actions")]
    public Action OnSceneLoadEvent;
    public Action OnSceneUnloadEvent;

    private async void Start()
    {
        foreach (SceneConfig scene in _scenes)
        {
            if (scene.LoadingOnInitialization == true)
            {
                await Add(scene.SceneField);
            }
        }
    }

    public async Task Add(string sceneToLoad)
    {
        SceneField field = GetSceneFieldByString(sceneToLoad);

        await LoadSceneAsync(field.SceneName);
        LoadEvent();
    }

    public async Task Transition(string sceneToLoad, string sceneToUnload)
    {
        UnloadEvent();
        await UnloadSceneAsync(sceneToUnload);

        await LoadSceneAsync(sceneToLoad);
        LoadEvent();
    }

    public async Task RestartGameScene(string sceneToRestart)
    {
        UnloadEvent();
        await UnloadSceneAsync(sceneToRestart);

        await LoadSceneAsync(sceneToRestart);
        LoadEvent();
    }

    private SceneField GetSceneFieldByString(string sceneName)
    {
        foreach (var scene in _scenes)
        {
            if(scene.SceneName == sceneName)
            {
                return scene.SceneField;
            }
        }

        return null;
    }

    private void LoadEvent()
    {
        OnSceneLoadEvent?.Invoke();  
    }

    private void UnloadEvent()
    {
        OnSceneUnloadEvent?.Invoke();
    }

    private async Task LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            await Task.Yield();
        }
    }

    private async Task UnloadSceneAsync(string sceneName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneName);
        while (!asyncUnload.isDone)
        {
            await Task.Yield();
        }
    }

}