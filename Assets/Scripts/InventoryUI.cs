using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] GameObject slotPrefab;

    public void AddItem(PickUpData data)
    {
        GameObject slot = Instantiate(slotPrefab, container);

        InventorySlot ui = slot.GetComponent<InventorySlot>();
        ui.Setup(data);
    }
}
