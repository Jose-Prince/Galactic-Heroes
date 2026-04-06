using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject continueButton;
    [SerializeField] PersistanceManager persistence;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if (persistence.SaveExists())
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    public void NewGame()
    {
        GameManager.isContinue = false;
        SceneManager.LoadScene("SampleScene");
    }

    public void ContinueGame()
    {
        GameManager.isContinue = true;
        SceneManager.LoadScene("SampleScene");
    }
}
