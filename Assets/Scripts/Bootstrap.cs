using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrap 
{
    const string BootstrapScene = "Bootstrap";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        if (!SceneManager.GetSceneByName(BootstrapScene).isLoaded)
        {
            SceneManager.LoadScene(BootstrapScene, LoadSceneMode.Additive);
        }
    }
}
