using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance;

    public List<Ring> rings = new List<Ring>();
    private int ringsPassed = 0;

    void Awake()
    {
        Instance = this;
        Ring[] allRings = FindObjectsByType<Ring>(FindObjectsSortMode.None);
        rings.AddRange(allRings);    
    }

    public void RingPassed(Ring ring)
    {
        ringsPassed++;

        if (ringsPassed >= rings.Count)
        {
            FinishRace();
        }
    }

    void FinishRace()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
