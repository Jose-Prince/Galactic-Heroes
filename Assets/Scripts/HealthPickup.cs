using UnityEngine;

public class HealthPickup : PickUp
{
    protected override void ApplyEffect(GameObject player)
    {
        ShipSettings ship = player.GetComponent<ShipSettings>();

        if (ship != null)
            ship.Heal(30f);
    }
}
