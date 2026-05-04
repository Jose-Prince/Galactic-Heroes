using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    string currentScene;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadRoutine(sceneName));
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        if (!string.IsNullOrEmpty(currentScene))
        {
            yield return SceneManager.UnloadSceneAsync(currentScene);
        }

        yield return SceneManager.LoadSceneAsync(
            sceneName,
            LoadSceneMode.Additive
        );

        currentScene = sceneName;

        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }
}
