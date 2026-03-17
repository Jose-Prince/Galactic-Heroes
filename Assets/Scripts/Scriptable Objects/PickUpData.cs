using UnityEngine;

[CreateAssetMenu(fileName = "PickUpData", menuName = "Game/Pickup")]
public class PickUpData : ScriptableObject
{
    [SerializeField] string pickupName;
    [SerializeField] Sprite icon;
    [SerializeField] string description;
}
