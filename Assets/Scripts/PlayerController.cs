using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrust = 20f;
    [SerializeField] float boostMultiplier = 2f;
    [SerializeField] float brakeForce = 10f;

    [SerializeField] float pitchSpeed = 80f;
    [SerializeField] float yawSpeed = 80f;
    [SerializeField] float rollSpeed = 120f;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 0;
        rb.angularDamping = 0;

    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float currentThrust = thrust;

        if (Input.GetKey(KeyCode.LeftShift))
            currentThrust *= boostMultiplier;

        rb.AddForce(-transform.right * currentThrust, ForceMode.Acceleration);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, brakeForce * Time.fixedDeltaTime);
        }
    }

    void HandleRotation()
    {
        float pitch = 0f;
        float yaw = 0f;
        float roll = 0f;

                if (Input.GetKey(KeyCode.W)) pitch = 1;
        if (Input.GetKey(KeyCode.S)) pitch = -1;

        if (Input.GetKey(KeyCode.A)) yaw = -1;
        if (Input.GetKey(KeyCode.D)) yaw = 1;

        if (Input.GetKey(KeyCode.Q)) roll = 1;
        if (Input.GetKey(KeyCode.E)) roll = -1;

        Vector3 torque = new Vector3(
            pitch * pitchSpeed,
            yaw * yawSpeed,
            roll * rollSpeed
        );

        rb.AddRelativeTorque(torque * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
