using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public static LoadingUI Instance;

    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI percentageText;

    void Awake()
    {
        Instance = this;
    }

    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);

        progressBar.fillAmount = progress;
        percentageText.text = $"{progress * 100f:0}%";
    }
}
