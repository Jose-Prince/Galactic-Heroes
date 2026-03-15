using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInput;

    [SerializeField] float speedMult = 1;
    [SerializeField] float speedMultAngle = 0.5f;
    [SerializeField] float speedRollMultAngle = 0.05f;

    [SerializeField] float maxRollAngle = 45f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");

        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.TransformDirection(-Vector3.right) * verticalMove * speedMult, ForceMode.VelocityChange);
        rb.AddForce(transform.TransformDirection(Vector3.forward) * horizontalMove * speedMult, ForceMode.VelocityChange);

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
    }
}