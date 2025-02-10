using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite swim;
    [SerializeField] private Sprite normal;
    
    private Vector2 _movement;

    public bool _isSwimming;
    public bool canJump;
    public bool isJumping;
    public bool atWaterSurface;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public bool _facingRight = false;
    private Vector2 _rayDirection = new(0.5f, -0.5f);

    public int speed;


    private delegate void FlipOperation();
    private FlipOperation flip;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flip = WalkFlip;
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
    }
    public void OnJump(InputValue value)
    {
        if (canJump)
        {
            isJumping = true;
            Vector2 move = new Vector2(-8.0f, 8.0f);
            rb.AddForce(move, ForceMode2D.Impulse);
            canJump = false;
        }
    }
    public void OnInteract(InputValue value)
    {
        Debug.Log("Interact");
    }

    private void Swim()
    {
        CheckFlip();
        if (atWaterSurface)
        {
            rb.linearVelocity = _movement.y < 0 ? new Vector2(_movement.x, _movement.y).normalized * speed : new Vector2(_movement.x, 0).normalized * speed ;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * speed;
        }
        if (_movement.magnitude > 0.1f) //if input change rotation, if not keep old rotation as to not reset to 0
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            playerTransform.rotation = Quaternion.Euler(0f, 0f, angle); 
        }
    }

    private void Walk()
    {
        if (isJumping) return;
        CheckFlip();
        bool isGrounded = CheckGroundAhead();
        if(isGrounded)
            rb.linearVelocity = new Vector2(_movement.x, 0).normalized * speed;
        else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    private void CheckFlip()
    {
        switch (_movement.x)
        {
            case > 0 when !_facingRight:
            case < 0 when _facingRight:
                flip();
                break;
        }
    }

    public void StartSwimming()
    {
        sr.sprite = swim;
        rb.linearVelocity = new Vector2(0, 0);
        _isSwimming = true;
        col.enabled = false;
        capsule.enabled = true;
        rb.gravityScale = 0f;
        flip = WaterFlip;
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

    private void WalkFlip()
    {
        // Toggle the facing direction
        _facingRight = !_facingRight;

        // Flip the player's scale on the X-axis
        float newYRotation = _facingRight ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, newYRotation, 0f);
        
        _rayDirection = _facingRight ? new Vector2(0.5f, -0.5f) : new Vector2(-0.5f, -0.5f);
    }

    private void WaterFlip()
    {
        _facingRight = !_facingRight;
        // Flip the player's scale on the X-axis
        sr.flipY = !_facingRight;
    }
}
