using UnityEngine;

public class MenuUI : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        {
            TogglePause();
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneFlowManager.Instance.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void LoadSceneWait(string sceneName)
    {
        SceneFlowManager.Instance.LoadSceneWithLoading(sceneName);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = isPaused;
    }
}
