using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float distance = 80f;
    [SerializeField] float orbitSpeed = 100f;
    [SerializeField] float recenterSpeed = 3f;

    float yaw;
    float pitch;

    float initialYaw;
    float initialPitch;

    void Start()
    {
        Vector3 direction = transform.position - target.position;
        distance = -direction.magnitude;

        yaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        pitch = Mathf.Asin(direction.y / distance) * Mathf.Rad2Deg;

        initialYaw = yaw;
        initialPitch = pitch;
    }

    void LateUpdate()
    {
        bool orbiting = Input.GetKey(KeyCode.LeftControl);

        if (orbiting)
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, -40f, 80f);
        }
        else
        {
            yaw = Mathf.LerpAngle(yaw, initialYaw, recenterSpeed * Time.deltaTime);
            pitch = Mathf.LerpAngle(pitch, initialPitch, recenterSpeed * Time.deltaTime);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 offset = rotation * new Vector3(0, 0, -distance);

        transform.position = target.position + offset;

        transform.LookAt(target);
    }
}
