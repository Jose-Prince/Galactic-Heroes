using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float verticalMove; 
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;

    bool isBraking = false;

    [SerializeField] float speedMult = 1;
    [SerializeField] float speedMultAngle = 0.5f;
    [SerializeField] float speedRollMultAngle = 0.05f;

    [Header("Stats")]
    [SerializeField] float acceleration = 20f;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float brakeForce = 40f;
    [SerializeField] float drag = 2f;
    [SerializeField] float orbitSpeed = 50f;

    [SerializeField] Transform pivot;

    float currentSpeed = 0f;

    [SerializeField] float maxRollAngle = 45f;

    [SerializeField] StatsData stats;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<StatsData>();
    }

    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");

        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");
        isBraking = Input.GetKey(KeyCode.Space);;
    }

    void FixedUpdate()
    {
        //Vector3 forwardDir = transform.TransformDirection(-Vector3.right);
        if (pivot != null && Mathf.Abs(horizontalMove) > 0.01f)
        {
            transform.RotateAround(
                pivot.position,
                Vector3.up,
                horizontalMove * orbitSpeed * Time.fixedDeltaTime
            );
        }

        if (pivot != null && Mathf.Abs(verticalMove) > 0.01f)
        {
            transform.RotateAround(
                pivot.position,
                Vector3.right,
                verticalMove * orbitSpeed * Time.fixedDeltaTime
            );
        }

        if (!Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddTorque(transform.forward * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
            rb.AddTorque(transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);
        }

        float currentRoll = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);

        if ((rollInput > 0 && currentRoll < maxRollAngle) ||
            (rollInput < 0 && currentRoll > -maxRollAngle))
        {
            rb.AddTorque(-transform.right * speedRollMultAngle * rollInput, ForceMode.VelocityChange);
        }

        if (Mathf.Abs(currentRoll) >= maxRollAngle)
        {
            Vector3 angVel = rb.angularVelocity;
            angVel -= Vector3.Project(angVel, transform.right);
            rb.angularVelocity = angVel;
        }

        currentSpeed += acceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

        if (isBraking)
        {
            currentSpeed -= brakeForce * Time.fixedDeltaTime;
        }

        currentSpeed = Mathf.Max(currentSpeed, 0);

        currentSpeed -= drag * currentSpeed * Time.fixedDeltaTime;

        //rb.AddForce(forwardDir * currentSpeed, ForceMode.Acceleration);
    }
}