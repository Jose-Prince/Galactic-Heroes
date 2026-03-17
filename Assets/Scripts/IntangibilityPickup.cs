using UnityEngine;

public class IntangibilityPickup : PickUp
{
    [SerializeField] float duration = 5f;
    protected override void ApplyEffect(GameObject player)
    {
        ShipSettings ship = player.GetComponent<ShipSettings>();

        if (ship != null)
        {
            ship.ActivateIntangibility(duration);
        }
    }
}