using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowManager : MonoBehaviour
{
    public static SceneFlowManager Instance { get; private set; }
    string currentScene;
    const string BootstrapScene = "Bootstrap";
    const string BackgroundScene = "Background";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DetectCurrentScene();
    }

    void DetectCurrentScene()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != BootstrapScene)
            {
                currentScene = scene.name;
                SceneManager.SetActiveScene(scene);
                break;
            }
        }
        Debug.Log("Scene: " + currentScene);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadRoutine(sceneName));
    }

    public void LoadSceneWithLoading(string sceneName)
    {
        StartCoroutine(LoadWithLoadingRoutine(sceneName));
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        yield return UnloadBackgroundIfNeeded(currentScene);

        if (!string.IsNullOrEmpty(currentScene))
        {
            Scene sceneToUnload = SceneManager.GetSceneByName(currentScene);

            if (sceneToUnload.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        yield return SceneManager.LoadSceneAsync(
            sceneName,
            LoadSceneMode.Additive
        );

        yield return LoadBackgroundIfNeeded(sceneName);

        currentScene = sceneName;

        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }

    IEnumerator LoadWithLoadingRoutine(string sceneName)
    {
        yield return UnloadBackgroundIfNeeded(currentScene);

        yield return SceneManager.LoadSceneAsync(
            "LoadingScene",
            LoadSceneMode.Additive
        );
        yield return null;

        if (!string.IsNullOrEmpty(currentScene))
        {
            Scene sceneToUnload = SceneManager.GetSceneByName(currentScene);

            if (sceneToUnload.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(
            sceneName,
            LoadSceneMode.Additive
        );

        loadOp.allowSceneActivation = false;

        float timer = 0f;

        while (timer < 5f || loadOp.progress < 0.9f)
        {
            timer += Time.deltaTime;

            float sceneProgress = loadOp.progress / 0.9f;
            float timeProgress = timer / 5f;

            float progress = Mathf.Min(sceneProgress, timeProgress);

            if (LoadingUI.Instance != null)
                LoadingUI.Instance.SetProgress(progress);

            yield return null;
        }

        loadOp.allowSceneActivation = true;

        while (!loadOp.isDone)
        {
            yield return null;
        }

        yield return LoadBackgroundIfNeeded(sceneName);

        currentScene = sceneName;

        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);

        yield return SceneManager.UnloadSceneAsync("LoadingScene");
    }

    bool IsRaceScene(string sceneName)
    {
        return sceneName == "RaceScene1" ||
            sceneName == "RaceScene2" ||
            sceneName == "RaceScene3";
    }

    IEnumerator LoadBackgroundIfNeeded(string sceneName)
    {
        if (IsRaceScene(sceneName))
        {
            Scene bg = SceneManager.GetSceneByName(BackgroundScene);

            if (!bg.isLoaded)
            {
                yield return SceneManager.LoadSceneAsync(
                    BackgroundScene,
                    LoadSceneMode.Additive
                );
            }
        }
    }

    IEnumerator UnloadBackgroundIfNeeded(string sceneName)
    {
        if (IsRaceScene(sceneName))
        {
            Scene bg = SceneManager.GetSceneByName(BackgroundScene);

            if (bg.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(BackgroundScene);
            }
        }
    }
}
