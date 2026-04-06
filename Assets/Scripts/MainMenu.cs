using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject continueButton;
    [SerializeField] PersistanceManager persistence;

    private GameData data;

    void Start()
    {
        data = persistence.LoadData();
        Cursor.lockState = CursorLockMode.None;
        if (persistence.SaveExists() && !data.finishedRace)
        {
            print(data.finishedRace);
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
