using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI lastTimeText;

    public PersistanceManager persistence;


    private float time = 0f;
    private bool isRunning = true;

    void Start()
    {
        GameData data = persistence.LoadData();

        UpdateUI(data.raceTime, lastTimeText);
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
