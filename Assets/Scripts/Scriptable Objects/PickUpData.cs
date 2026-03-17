using UnityEngine;

[CreateAssetMenu(fileName = "PickUpData", menuName = "Game/Pickup")]
public class PickUpData : ScriptableObject
{
    public string pickupName;
    public Sprite icon;
    public string description;
}
