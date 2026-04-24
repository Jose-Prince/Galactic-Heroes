using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PersistanceManager persistance;

    private GameData data;
    private int actualRace;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (persistance != null)
        {
            data = persistance.LoadData();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;   
        }
    }

    public GameData GetData()
    {
        return data;
    }

    public void setActualRace(int id)
    {
        actualRace = id;
    }
}
