using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.5f; // Set player's movement speed.

    [SerializeField]
    private float rotationSpeed = 100.0f; // Set player's rotation speed.

    //jump force
    // [SerializeField]
    // private float jumpForce = 0.1f;

    private Rigidbody rb; // Reference to player's Rigidbody.
    private float movementX;
    private float movementY;

    //Jump action 
    // private InputAction jumpAction;
    private InputAction movement;

    private GameManager gameManager;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
                                        // jumpAction = InputSystem.actions.FindAction("Jump");
        movement = InputSystem.actions.FindAction("Move");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Handle physics-based movement and rotation.
    private void FixedUpdate()
    {

        if (gameManager.isGameActive)
        {
            OnMove();
            // Move player based on vertical input.
            CalculPosition();

            // Rotate player based on horizontal input.
            CaculRotation();
        }
    }

    private void CalculPosition()
    {
        Vector3 movement = transform.forward * movementY * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
    private void CaculRotation()
    {
        float turn = movementX * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void OnMove()
    {
        Vector2 movementVector = movement.ReadValue<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

}