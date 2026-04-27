using UnityEngine;

public class PivotFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothSpeed = 5f;

    [Header("Offset")]
    [SerializeField] float distance = 10f;
    [SerializeField] float height = 3f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position
            - target.right * distance
            + Vector3.up * height;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target);
    }
}
