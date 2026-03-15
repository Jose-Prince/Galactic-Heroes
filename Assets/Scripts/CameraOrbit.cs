using Unity.VisualScripting;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance = 80f;
    [SerializeField] float orbitSpeed = 100f;
    [SerializeField] float recenterSpeed = 3f;

    float yaw;
    float pitch = 20f;

    void Start()
    {
        Vector3 direction = transform.position - target.position;
        distance = -direction.magnitude;

        yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        pitch = Mathf.Asin(direction.y / distance) * Mathf.Rad2Deg;
    }

    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, -40f, 80f);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + offset;

        transform.LookAt(target);
    }
}
