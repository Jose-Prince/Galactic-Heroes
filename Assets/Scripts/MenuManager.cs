using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }
}
