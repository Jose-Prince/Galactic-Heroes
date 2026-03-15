using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    float verticalMove;
    float horizontalMove;
    float mouseInputX;
    float mouseInputY;
    float rollInputt;

    [SerializeField] float speedMult = 1;
    [SerializeField] float speedMultAngle = 0.5f;
    [SerializeField] float speedRollMultAngle = 0.05f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        rollInputt = Input.GetAxis("Roll");

        mouseInputX = Input.GetAxis("Mouse X");
        mouseInputY = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
        rb.AddForce(rb.transform.TransformDirection(-Vector3.right) * verticalMove * speedMult, ForceMode.VelocityChange);

        rb.AddForce(rb.transform.TransformDirection(Vector3.forward) * horizontalMove * speedMult, ForceMode.VelocityChange);
 
        rb.AddTorque(rb.transform.forward * speedMultAngle * mouseInputY * -1, ForceMode.VelocityChange);
        rb.AddTorque(rb.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);

        rb.AddTorque(-rb.transform.right * speedMultAngle * rollInputt, ForceMode.VelocityChange);
    }
}
