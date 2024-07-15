using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public float jumpHeight = 7f;
    private bool readyForJump = false;
    public float movementSpeed = 3f;
    public Vector3 rotationSpeed = new Vector3(0,50,0);
    private Vector3 inputDirection;
    private Rigidbody rb;

    public bool IsGrounded() {
        RaycastHit hit;
        float rayLength = 2f;
        return (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength));
    }

    private bool JumpConditionTrue()
    {
        return IsGrounded() && Input.GetKeyDown(KeyCode.Space); // use the Input instead
    }

    private void HandleJump()
    {
        Vector3 currentVel = rb.velocity;
        Vector3 newVel = new Vector3(currentVel.x, jumpHeight, currentVel.y);
        
        if (readyForJump) {
        	readyForJump = false;
            rb.velocity = newVel;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 inputs = new Vector3(Input.GetAxisRaw("Horizontal View"), Input.GetAxisRaw("Vertical Movement"), Input.GetAxisRaw("Lateral Movement"));
        inputDirection = inputs.normalized;
        if (JumpConditionTrue() && !readyForJump)
        	readyForJump = true;
    }

    void FixedUpdate() {
        Quaternion deltaRotation = Quaternion.Euler(inputDirection.x * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        rb.MovePosition(rb.position + ((transform.forward * movementSpeed * inputDirection.y) + (transform.right * movementSpeed * inputDirection.z)) * Time.deltaTime);
        HandleJump();
    }

}
