using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float verticalMove; 
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;

    bool isBraking = false;
    float pitchInput;
    float currentPitch = 0f;

    float mouseVelX;
    float mouseVelY;

    [SerializeField] float speedMult = 1;
    [SerializeField] float speedMultAngle = 0.5f;
    [SerializeField] float speedRollMultAngle = 0.05f;

    [Header("Stats")]
    [SerializeField] float acceleration = 20f;
    [SerializeField] float maxSpeed = 50f;
    [SerializeField] float brakeForce = 40f;
    [SerializeField] float drag = 2f;
    [SerializeField] float orbitSpeed = 50f;

    [SerializeField] float pitchSpeed = 120f;
    [SerializeField] float maxPitchAngle = 45f;
    [SerializeField] float pitchReturnSpeed = 2f;

    [SerializeField] Transform pivot;

    [SerializeField] float mouseSmoothSpeed = 10f;
    [SerializeField] float maxMouseInput = 3f;

    float smoothMouseX;
    float smoothMouseY;

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
        pitchInput = 0f;

        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Q)) pitchInput = 1f;
        if (Input.GetKey(KeyCode.E)) pitchInput = -1f;

        float rawMouseX = Input.GetAxis("Mouse X");
        float rawMouseY = Input.GetAxis("Mouse Y");

        rawMouseX = Mathf.Clamp(rawMouseX, -maxMouseInput, maxMouseInput);
        rawMouseY = Mathf.Clamp(rawMouseY, -maxMouseInput, maxMouseInput);

        smoothMouseX = Mathf.SmoothDamp(smoothMouseX, rawMouseX, ref mouseVelY, 0.05f);
        smoothMouseY = Mathf.SmoothDamp(smoothMouseY, rawMouseY, ref mouseVelY, 0.05f);

        isBraking = Input.GetKey(KeyCode.Space);;
    }

    void FixedUpdate()
    {
        Vector3 forwardDir = transform.TransformDirection(-Vector3.right);
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
            rb.AddTorque(transform.forward * speedMultAngle * smoothMouseY * -1, ForceMode.VelocityChange);
            rb.AddTorque(transform.up * speedMultAngle * smoothMouseX, ForceMode.VelocityChange);
        }

        if (Mathf.Abs(pitchInput) > 0.01f)
        {
            transform.Rotate(
                Vector3.right,
                pitchInput * speedMultAngle * 100f * Time.fixedDeltaTime,
                Space.Self
            );
        }

        float currentRoll = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);

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

        rb.AddForce(forwardDir * currentSpeed, ForceMode.Acceleration);
    }
}