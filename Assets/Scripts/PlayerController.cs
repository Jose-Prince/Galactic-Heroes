using Unity.Mathematics;
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
    [SerializeField] GameObject horizontalPivot;

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
        HandleTurning();
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
        rb.linearVelocity = forwardVelocity;
    }

    void HandleRotation()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.AddTorque(-transform.right * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
            rb.AddTorque(transform.up * speedMultAngle * mouseInputX * stats.handling, ForceMode.VelocityChange);
        }
    }

    void HandleRoll()
    {
        float currenRoll = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);

        if ((rollInput > 0 && currenRoll < maxRollAngle) || 
            (rollInput < 0 && currenRoll > -maxRollAngle))
        {
            rb.AddTorque(-transform.right * speedMultAngle * rollInput, ForceMode.VelocityChange);
        }

        if (Mathf.Abs(currenRoll) >= maxRollAngle)
        {
            Vector3 angVel = rb.angularVelocity;
            angVel -= Vector3.Project(angVel, transform.right);
            rb.angularVelocity = angVel;
        }
    }

    void HandleTurning()
    {
        if (horizontalPivot == null) return;

        float turnInput = horizontalMove;
        float speedFactor = currentSpeed / stats.speed;

        float turnSpeed = turnInput * stats.handling * speedFactor;

        Vector3 offset = transform.position - horizontalPivot.transform.position;

        Quaternion rotation = quaternion.Euler(0, turnSpeed, 0);

        offset = rotation * offset;

        Vector3 newPosition = horizontalPivot.transform.position + offset;

        rb.MovePosition(newPosition);

        rb.MoveRotation(rotation * rb.rotation);
    }
}