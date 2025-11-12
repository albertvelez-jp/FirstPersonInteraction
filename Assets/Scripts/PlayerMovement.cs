using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float raycastLength = 1.1f;
    public LayerMask groundMask;

    public Rigidbody rigidBody;
    public BoxCollider boxCollider;
    public bool isGrounded;
    public Animator playeranimator;

    public float currentSpeed;

    void Update()
    {
        Vector3 center = boxCollider.bounds.center;

        isGrounded = Physics.Raycast(center, Vector3.down, raycastLength, groundMask);
        Debug.DrawRay(center, Vector3.down * raycastLength, isGrounded ? Color.green : Color.red);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * vertical + transform.right * horizontal;

        currentSpeed = speed;
        playeranimator.SetBool("Walking", false);
        playeranimator.SetBool("WalkingBackwards", false);
        playeranimator.SetBool("RightStrafeWalking", false);
        playeranimator.SetBool("Floating", false);
        playeranimator.SetBool("LeftStrafeWalking", false);

        if (vertical > 0 && horizontal == 0)
        {
            currentSpeed = speed;  // adelante 100%
            playeranimator.SetBool("Walking",true);
        }

        if (vertical < 0 && horizontal == 0)
        {
            currentSpeed = speed * 0.5f;  // atras 50%
            playeranimator.SetBool("WalkingBackwards", true);
        }

        if (horizontal > 0 && vertical == 0)
        {
            currentSpeed = speed * 0.75f;  // derecha 75%
            playeranimator.SetBool("RightStrafeWalking", true);
        }

        if (horizontal < 0 && vertical == 0)
        {
            currentSpeed = speed * 0.75f;  // izquierda 75%
            playeranimator.SetBool("LeftStrafeWalking", true);
        }

        if (vertical > 0 && horizontal > 0)
        {
            currentSpeed = speed;  // adelante diagonal derecha 100%
            playeranimator.SetBool("Walking", true);
        }

        if (vertical > 0 && horizontal < 0)
        {
            currentSpeed = speed;  // adelante diagonal izquierda 100%
            playeranimator.SetBool("Walking", true);
        }

        if (vertical < 0 && horizontal > 0)
        {
            currentSpeed = speed * 0.5f;  // atras diagonal derecha 50%
            playeranimator.SetBool("WalkingBackwards", true);
        }

        if (vertical < 0 && horizontal < 0)
        {
            currentSpeed = speed * 0.5f;  // atras diagonal izquierda 50%
            playeranimator.SetBool("WalkingBackwards", true);
        }

        if (isGrounded && Input.GetKey(KeyCode.LeftShift) && vertical > 0)
        {
            currentSpeed *= 2f;  // sprint 200%
        }

        if (isGrounded == false)
        {
            currentSpeed *= 0.5f;  // en el aire 50%
            playeranimator.SetBool("Floating", true);
        }

        rigidBody.linearVelocity = new Vector3(move.x * currentSpeed, rigidBody.linearVelocity.y, move.z * currentSpeed);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
