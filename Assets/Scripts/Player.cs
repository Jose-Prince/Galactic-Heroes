using UnityEngine;

public class Player : MonoBehaviour
{
    public PersistanceManager persistence;

    void Start()
    {
        if (GameManager.isContinue)
        {
            GameData data = persistence.LoadData();

            if (data != null)
            {
                transform.position = new Vector3(data.posX, data.posY, data.posZ);
            }        
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            persistence.SaveData(transform.position);
        }        
    }
}
