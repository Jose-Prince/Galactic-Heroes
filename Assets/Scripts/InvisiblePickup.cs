using UnityEngine;

public class InvisiblePickup : PickUp
{
    [SerializeField] float duration = 5f;

    protected override void ApplyEffect(GameObject player)
    {
        ShipSettings ship = player.GetComponent<ShipSettings>();

        if (ship != null)
        {
            ship.ActivateInvisibility(duration);
        }
    }
}
