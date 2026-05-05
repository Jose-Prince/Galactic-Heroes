using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneFlowManager.Instance.LoadScene(sceneName);
    }

    public void LoadSceneWait(string sceneName)
    {
        SceneFlowManager.Instance.LoadSceneWithLoading(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
