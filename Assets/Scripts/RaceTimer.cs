using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RaceTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    private float time = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        time += Time.deltaTime;
        UpdateUI();    
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
