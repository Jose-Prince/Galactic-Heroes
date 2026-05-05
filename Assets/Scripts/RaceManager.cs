using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    [SerializeField] RaceTimer timer;

    public PersistanceManager persistence;
    public static RaceManager Instance;

    public List<Ring> rings = new List<Ring>();
    private int ringsPassed = 0;

    void Awake()
    {
        Instance = this;
        Ring[] allRings = FindObjectsByType<Ring>(FindObjectsSortMode.None);
        rings.AddRange(allRings);    
    }

    void Start()
    {
        GameData data = persistence.LoadData();

        if (data != null)
        {
            LoadRace(data);
        }
    }

    public void RingPassed()
    {
        ringsPassed++;

        if (ringsPassed >= rings.Count)
        {
            FinishRace();
        }
    }

    void FinishRace()
    {
        var actualID = 0;
        timer.StopTimer();
        persistence.SaveData(Vector3.zero, true, timer, actualID);
        SceneManager.LoadScene("Main Menu");
    }

    public void ResetRace()
    {
        ringsPassed = 0;

        foreach (Ring ring in rings)
        {
            ring.ResetRing();
        }
    }

    public void SaveRaceProgress(Vector3 playerPosition, bool finished)
    {
        GameData data = new GameData();

        data.posX = playerPosition.x;
        data.posY = playerPosition.y;
        data.posZ = playerPosition.z;

        data.finishedRace = finished;

        data.ringsPassed = new List<bool>();

        data.raceTimes[0] = timer.GetTime();

        foreach (Ring ring in rings)
        {
            data.ringsPassed.Add(ring.passed);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText("/player.save", json);;
    }

    void LoadRace(GameData data)
    {
        for (int i = 0; i < rings.Count; i++)
        {
            if (i < data.ringsPassed.Count && data.ringsPassed[i])
            {
                rings[i].passed = true;
                rings[i].gameObject.SetActive(false);
                ringsPassed++;
            }
        }
    }
}