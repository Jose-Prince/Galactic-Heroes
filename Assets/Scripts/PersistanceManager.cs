using System.IO;
using TMPro;
using UnityEngine;

public class PersistanceManager : MonoBehaviour
{   
    string path;

    void Awake()
    {
        path = Application.persistentDataPath + "/player.save";
    }

    public void SaveData(Vector3 position, bool finishedRace, RaceTimer timer, int raceID)
    {
        GameData data = new GameData();

        data.posX = position.x;
        data.posY = position.y;
        data.posZ = position.z;

        data.finishedRace = finishedRace;

        if (finishedRace)
            data.raceTimes[raceID] = timer.GetTime();

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

        Debug.Log("Game saved in: " + path);
    }

    public GameData LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            Debug.Log("Data loaded");
            return data;
        }
        else
        {
            Debug.LogWarning("File does not exists");
            return null;
        }
    }

    public bool SaveExists()
    {
        return File.Exists(path);
    }
}
