using UnityEngine;

public class References : MonoBehaviour
{
    public static References Instance;

    [field: SerializeField] public DataPersistenceHandlerBase DataPersistenceHandlerBase { get; private set; }
    [field: SerializeField] public AudioHandler AudioHandler { get; private set; }
    [field: SerializeField] public InputController InputController { get; private set; }
    [field: SerializeField] public SceneLoader SceneLoader { get; private set; }


    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
