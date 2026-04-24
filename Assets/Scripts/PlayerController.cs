using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;
    bool isBraking;

    float currentSpeed;

    [SerializeField] float speedMultAngle = 0.5f;
    [SerializeField] float speedRollMultAngle = 0.05f;
    [SerializeField] float maxRollAngle = 45f;

    [SerializeField] StatsData stats;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();

        rb.mass = stats.weight;
    }

    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");

        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");

        isBraking = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleRoll();
    }

    void HandleMovement()
    {
        if (!isBraking)
        {
            currentSpeed += stats.acceleration * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, stats.speed);
        }
        else
        {
            currentSpeed -= stats.brake * Time.fixedDeltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, stats.speed);
        }

        Vector3 forwardVelocity = -transform.right * currentSpeed;

        Vector3 sideVelocity = transform.forward * horizontalMove * stats.handling;

        rb.linearVelocity = forwardVelocity + sideVelocity;
    }

    void HandleRotation()
    {
        
    }

    void HandleRoll()
    {
        
    }
}