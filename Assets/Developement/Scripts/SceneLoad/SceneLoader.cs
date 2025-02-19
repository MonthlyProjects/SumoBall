using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public UnityEvent OnMainMenuLoaded;
    public UnityEvent OnGameSceneLoaded;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        OnMainMenuLoaded?.Invoke();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        OnGameSceneLoaded?.Invoke();
    }
}
