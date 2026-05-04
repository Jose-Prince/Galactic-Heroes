using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneFlowManager.Instance.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
