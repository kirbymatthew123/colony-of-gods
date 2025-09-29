using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;       // Speed of movement
    private Rigidbody2D rb;
    private Vector2 movement;

    private Animator animator;         // reference to Animator

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // get Animator on this GameObject
    }

    void Update()
    {
        // Reset movement every frame
        movement = Vector2.zero;

        // Read keyboard input
        Keyboard kb = Keyboard.current;
        if (kb == null) return;

        if (kb.wKey.isPressed || kb.upArrowKey.isPressed)
            movement.y += 1;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed)
            movement.y -= 1;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)
            movement.x -= 1;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed)
            movement.x += 1;

        // Normalize so diagonal speed is consistent
        movement = movement.normalized;

        // Tell Animator if the ant is moving
        bool isMoving = movement != Vector2.zero;
        animator.SetBool("isMoving", isMoving);

        // Debug log to check state
        if (isMoving)
        {
            Debug.Log("Ant is moving...");
        }
        else
        {
            Debug.Log("Ant is idle...");
        }
    }

    void FixedUpdate()
    {
        // Move the ant
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // Rotate to face direction of movement
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}
