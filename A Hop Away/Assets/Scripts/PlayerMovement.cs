 using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerMovement : BasicMovement
{
    // Variables used for running/jumping
    public float jumpForce = 20.0f; // Height of the jump

    // ground check
    public Transform groundCheck; // Object used to check if touching the ground
    [SerializeField] private float boxWidth = 0.2f; // Distance from ground check to see if touching
    [SerializeField] private float boxHeight = 0.2f; // Distance from ground check to see if touching

    public LayerMask groundLayer; // What objects are defined as the ground
    private bool isGrounded; // Wether or not the player can actually jump
    private bool jumpRequested = false; // Checked every frame
    public float speed = 10.0f; // run speed

    private float jumpCooldown = 0.5f;
    private float timeRemaining;
    private bool timerStarted = false;
    private bool canJump = true;

    private Rigidbody2D rb;

    // Animation
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Sounds
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip fallSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // handling jump cooldown so it cant be spammed
        animator.SetBool("isJumping", !isGrounded);

        if (!canJump && !timerStarted)
        {
            timerStarted = true;
            timeRemaining = jumpCooldown;

        }

        if (timerStarted)
        {
            if (timeRemaining > 0f)
            {
                timeRemaining -= Time.deltaTime;
            }

            if (timeRemaining <= 0f)
            {
                timeRemaining = 0f;
                canJump = true;
                timerStarted = false;
            }
        }

        // If user falls off the map, game restarts
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("Start");
            AudioManager.Instance.PlaySound(fallSound);
            GameManager.resetScore();
        }

        // Checks if the player is touching the ground by making 
        // an invisible square on the bottom of the players feet
        // if that collides with an object with the "ground" 
        // layer, the player will be considered grounded.
        isGrounded = Physics2D.OverlapBox(
            groundCheck.position,
            new Vector2(boxWidth, boxHeight),
            0f,
            groundLayer
        );

        // Flips sprite based on x direction
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Left");
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Right");
            spriteRenderer.flipX = false;
        }

        // Jump requested only if touching the ground
        if (canJump && ((isGrounded && Input.GetKeyDown(KeyCode.UpArrow)) || (isGrounded && Input.GetKey(KeyCode.Space))))
        {
            jumpRequested = true;
            //Debug.Log("Jump");
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
            canJump = false;
            AudioManager.Instance.PlaySound(jumpSound);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }//end if

        bool isMoving = movement.magnitude > 0.01f;
        animator.SetBool("isRunning", isMoving);

        setLastMovement(movement);

        rb.linearVelocity = new Vector2(movement.x / Time.fixedDeltaTime, rb.linearVelocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.DrawWireCube(
                groundCheck.position,
                new Vector2(boxWidth, boxHeight)
            );
        }
    }

}
