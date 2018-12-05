using UnityEngine;

public class CanvasProxy : MonoBehaviour
{
    public void LoadGame()
    {
        GameSingleton.Singleton.GetSceneLoader().LoadNext();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScore()
    {
        GameSingleton.Singleton.GetSceneLoader().LoadScore();
    }

    public void LoadStart()
    {
        GameSingleton.Singleton.GetSceneLoader().LoadStart();
    }
}
