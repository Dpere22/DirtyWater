using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private Transform playerTransform;
    
    private Vector2 _movement;

    private bool _isSwimming;
    public bool canJump;
    public bool isJumping;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private bool _facingRight = true;
    private Vector2 _rayDirection = new(0.5f, -0.5f);

    public int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isSwimming = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isSwimming)
        {
            Swim();
        }
        else
        {
            Walk();
        }
    }

    /// <summary>
    /// Works by using Unity's PlayerInput calls
    /// </summary>
    /// <param name="value"> value from controller </param>
    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        if (_movement.x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (_movement.x < 0 && _facingRight)
        {
            Flip();
        }
    }

    public void OnJump(InputValue value)
    {
        if (canJump)
        {
            isJumping = true;
            Vector2 move = new Vector2(-10.0f, 10.0f);
            rb.AddForce(move, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    private void Swim()
    {
        rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * speed; //normalized to avoid greater speed in diagonal
        if (_movement.magnitude > 0.1f) //if input change rotation, if not keep old rotation as to not reset to 0
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            playerTransform.rotation = Quaternion.Euler(0, 0, angle); 
        }
    }

    private void Walk()
    {
        if (isJumping) return;
        bool isGrounded = CheckGroundAhead();
        if(isGrounded)
            rb.linearVelocity = new Vector2(_movement.x, 0).normalized * speed;
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    public void StartSwimming()
    {
        rb.linearVelocity = new Vector2(0, 0);
        _isSwimming = true;
        col.enabled = false;
        capsule.enabled = true;
        rb.gravityScale = 0f;
    }
    private bool CheckGroundAhead()
    {
        // Cast a ray downward from the groundCheck position
        Vector2 rayOrigin = groundCheck.position;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, _rayDirection, groundCheckDistance, groundLayer);
        
        // Debugging: Visualize the raycast in the Scene view
        Debug.DrawRay(rayOrigin, _rayDirection * groundCheckDistance, Color.red);

        return hit.collider; // True if ground is detected
    }

    private void Flip()
    {
        Debug.Log("Flipping!");
        // Toggle the facing direction
        _facingRight = !_facingRight;

        // Flip the player's scale on the X-axis
        float newYRotation = _facingRight ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, newYRotation, 0f);
        
        _rayDirection = _facingRight ? new Vector2(0.5f, -0.5f) : new Vector2(-0.5f, -0.5f);
    }
}
