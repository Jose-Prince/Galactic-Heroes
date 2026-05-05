using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI lastTimeText;

    public GameManager gameManager;


    private float time = 0f;
    private bool isRunning = true;

    void Awake()
    {
        gameManager = FindObjectsByType<GameManager>(FindObjectsSortMode.None)[0];
    }

    void Start()
    {
        var data = gameManager.GetData();
        //UpdateUI(data.raceTimes[0], lastTimeText);
    }

    void Update()
    {
        if (!isRunning) return;

        time += Time.deltaTime;
        UpdateUI(time, timerText);    
    }

    void UpdateUI(float time, TextMeshProUGUI textUI)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = (int)((time * 100) % 100);

        textUI.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public float GetTime()
    {
        return time;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
