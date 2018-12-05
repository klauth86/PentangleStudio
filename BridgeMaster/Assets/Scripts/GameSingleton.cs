using UnityEngine;

public class GameSingleton : MonoBehaviour
{
    public static GameSingleton Singleton;

    [SerializeField] private SceneLoader SceneLoader;

    public SceneLoader GetSceneLoader()
    {
        return SceneLoader;
    }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
