using UnityEngine;

public class BoostPickup : PickUp
{
    protected override void ApplyEffect(GameObject player)
    {
        ShipSettings ship = player.GetComponent<ShipSettings>();

        if (ship != null)
            ship.HealBoost(30f);
    }
}
