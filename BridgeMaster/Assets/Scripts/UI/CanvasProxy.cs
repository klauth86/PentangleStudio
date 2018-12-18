using UnityEngine;

public class CanvasProxy : MonoBehaviour {
    public void LoadNext() {
        GameSingleton.Singleton.GetSceneLoader.LoadNext();
    }

    public void LoadPrev() {
        GameSingleton.Singleton.GetSceneLoader.LoadPrev();
    }

    public void Quit() {
        Application.Quit();
    }

    public void LoadScore() {
        GameSingleton.Singleton.GetSceneLoader.LoadScore();
    }

    public void LoadStart() {
        GameSingleton.Singleton.GetSceneLoader.LoadStart();
    }
}
