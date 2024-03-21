using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public float movementSpeed = 3f;
    public Vector3 rotationSpeed = new Vector3(0,50,0);
    private Vector3 inputDirection;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 inputs = new Vector3(Input.GetAxisRaw("Horizontal View"), Input.GetAxisRaw("Vertical Movement"), Input.GetAxisRaw("Lateral Movement"));
        inputDirection = inputs.normalized;
    }

    void FixedUpdate() {
        Quaternion deltaRotation = Quaternion.Euler(inputDirection.x * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.MovePosition(rb.position + ((transform.forward * movementSpeed * inputDirection.y) + (transform.right * movementSpeed * inputDirection.z)) * Time.deltaTime);
    }
}
