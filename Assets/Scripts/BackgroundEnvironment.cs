using UnityEngine;

public class BackgroundEnvironment : MonoBehaviour
{
    [SerializeField] Material skyboxMat;

    void Awake()
    {
        RenderSettings.skybox = skyboxMat;
        DynamicGI.UpdateEnvironment();
    }
}