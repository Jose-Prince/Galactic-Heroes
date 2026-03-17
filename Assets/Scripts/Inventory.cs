using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<PickUpData> pickups = new List<PickUpData>();

    [SerializeField] InventoryUI inventoryUI;

    public void AddPickup(PickUpData pickup)
    {
        pickups.Add(pickup);

        if (inventoryUI != null)
            inventoryUI.AddItem(pickup);   
    }
}
