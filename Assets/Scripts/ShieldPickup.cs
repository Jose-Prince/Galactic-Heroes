using UnityEngine;

public class ShieldPickup : PickUp
{
    protected override void ApplyEffect(GameObject player)
    {
        ShipSettings ship = player.GetComponent<ShipSettings>();

        if (ship != null)
            ship.HealShield(30f);
    }
}
