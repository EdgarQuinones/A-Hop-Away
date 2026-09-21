 using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : BasicMovement
{
    // Variables used for running/jumping
    public float jumpForce = 20.0f; // Height of the jump
    public Transform groundCheck; // Object used to check if touching the ground
    public float checkRadius = 0.2f; // Distance from ground check to see if touching
    public LayerMask groundLayer; // What objects are defined as the ground
    private bool isGrounded; // Wether or not the player can actually jump
    private bool jumpRequested = false; // Checked every frame
    public float speed = 10.0f; // run speed

    private Rigidbody2D rb;

    // Animation
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // If user falls off the map, game restarts
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("Start");
            GameManager.resetScore();
        }

        // Checks if the player is touching the ground by making 
        // an invisible circle on the bottom of the players feet
        // if that collides with an object with the "ground" 
        // layer, the player will be considered grounded.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Flips sprite based on x direction
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("Left");
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("Right");
            spriteRenderer.flipX = false;
        }

        // Jump requested only if touching the ground
        if ((isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) || (isGrounded && Input.GetKey(KeyCode.Space)))
        {
            jumpRequested = true;
            Debug.Log("Jump");
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            movement.x += speed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            movement.x -= speed * Time.fixedDeltaTime;

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }//end if

        bool isMoving = movement.magnitude > 0.01f;
        animator.SetBool("isRunning", isMoving);

        setLastMovement(movement);

        rb.linearVelocity = new Vector2(movement.x / Time.fixedDeltaTime, rb.linearVelocity.y);
    }
}