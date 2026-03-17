using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] protected PickUpData data;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory != null)
                inventory.AddPickup(data);
                
            ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(GameObject player);
}
