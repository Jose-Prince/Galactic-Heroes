using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] string startupScene = "MainMenu";

    void Start()
    {
        FindAnyObjectByType<SceneFlowManager>().LoadScene(startupScene);
    }
}
