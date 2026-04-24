using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectsByType<GameManager>(FindObjectsSortMode.None)[0];
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void SetActualRace(int RaceID)
    {
        gameManager.setActualRace(RaceID);
    }
}
