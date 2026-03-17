using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text nameText;

    public void Setup(PickUpData data)
    {
        icon.sprite = data.icon;
        nameText.text = data.pickupName;
    }
}
