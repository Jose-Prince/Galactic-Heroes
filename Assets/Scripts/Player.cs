using UnityEngine;

public class Player : MonoBehaviour
{
    public PersistanceManager persistence;

    void Start()
    {
        GameData data = persistence.LoadData();

        if (data != null)
        {
            transform.position = new Vector3(data.posX, data.posY, data.posZ);
        }        
    }
}