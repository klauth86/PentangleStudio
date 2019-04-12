using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNext()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index < SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPrev()
    {
        var index = SceneManager.GetActiveScene().buildIndex;
        if (index > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadStart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadScore()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }
}
