using UnityEngine;
using UnityEngine.InputSystem;
using Events;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EdgeCollider2D col;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite walkSprite;
    [SerializeField] private Sprite swim;
    [SerializeField] private Sprite normal;
    
    
    private Quaternion _playerWalkRotation;
    private Vector2 _movement;
    
    public bool canJump;
    private bool _canMove = true;
    public bool isJumping;
    public bool atWaterSurface;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    [FormerlySerializedAs("_facingRight")] public bool facingRight;
    private Vector2 _rayDirection = new(-0.15f, -0.25f);
    


    private delegate void FlipOperation();
    private FlipOperation _flip;
    
    
    private delegate void MoveOperation();
    private MoveOperation _move;
    private MoveOperation _previousMove;
    
    void Start()
    {
        
        _flip = WalkFlip;
        _move = Walk;
        _playerWalkRotation = transform.rotation;
        GameEventsManager.Instance.InputEvents.PauseGameAction += PauseHandler;
        GameEventsManager.Instance.InputEvents.ResumeGameAction += ResumeHandler;
        GameEventsManager.Instance.InputEvents.MoveAction += OnMove;
        GameEventsManager.Instance.InputEvents.JumpAction += OnJump;
        GameEventsManager.Instance.PlayerEvents.OnDisablePlayerMovement += RestrictMovement;
        GameEventsManager.Instance.PlayerEvents.OnEnablePlayerMovement += EnableMovement;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetSwim += SetPlayerSwimming;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetWalk += SetPlayerWalking;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.InputEvents.PauseGameAction -= PauseHandler;
        GameEventsManager.Instance.InputEvents.ResumeGameAction -= ResumeHandler;
        GameEventsManager.Instance.InputEvents.MoveAction -= OnMove;
        GameEventsManager.Instance.InputEvents.JumpAction -= OnJump;
        GameEventsManager.Instance.PlayerEvents.OnDisablePlayerMovement -= RestrictMovement;
        GameEventsManager.Instance.PlayerEvents.OnEnablePlayerMovement -= EnableMovement;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetSwim -= SetPlayerSwimming;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetWalk -= SetPlayerWalking;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        _move?.Invoke();
    }

    private void ResumeHandler()
    {
        EnableMovement();
    }

    private void PauseHandler()
    {
        RestrictMovement();
    }

    public void RestrictMovement()
    {
        rb.linearVelocity = Vector2.zero;
        _canMove = false;
    }

    public void EnableMovement()
    {
        _canMove = true;
    }
    
    public void OnMove(Vector2 dir)
    {
        _movement = dir;
    }
    public void OnJump()
    {
        if (!canJump) return;
        rb.linearVelocity = Vector2.zero;
        isJumping = true;
        Vector2 direction = new Vector2(4.0f, 4.0f);
        rb.AddForce(direction, ForceMode2D.Impulse);
        canJump = false;
    }

    public void OnOpenInventory(InputValue value)
    {
        Debug.Log("Input Recieved");
    }

    private void SetPlayerSwimming()
    {
        isJumping = false;
        sr.sprite = swim;
        rb.linearVelocity = new Vector2(0, 0);
        col.enabled = false;
        capsule.enabled = true;
        rb.gravityScale = 0f;
        _flip = WaterFlip;
        _move = Swim;
    }

    private void SetPlayerWalking()
    {
        facingRight = true;
        sr.sprite = walkSprite;
        sr.flipY = false;
        transform.rotation = _playerWalkRotation;
        rb.linearVelocity = new Vector2(0, 0);
        col.enabled = true;
        capsule.enabled = false;
        rb.gravityScale = 1.0f;
        _flip = WalkFlip;
        _move = Walk;
    }
    
    private void Swim()
    {
        if (!_canMove) return;
        CheckFlip();
        if (atWaterSurface)
        {
            rb.linearVelocity = _movement.y < 0 ? new Vector2(_movement.x, _movement.y).normalized * PlayerManager.speed : new Vector2(_movement.x, 0).normalized * PlayerManager.speed ;
            playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * PlayerManager.speed;
        }
        if (_movement.magnitude > 0.1f) //if input change rotation, if not keep old rotation as to not reset to 0
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            playerTransform.rotation = Quaternion.Euler(0f, 0f, angle); 
        }
    }

    private void Walk()
    {
        if(!_canMove) return;
        if (isJumping) return;
        CheckFlip();
        bool isGrounded = CheckGroundAhead();
        if(isGrounded)
            rb.linearVelocity = new Vector2(_movement.x, 0).normalized * PlayerManager.walkingSpeed;
        else
        {
            //Debug.Log("I can't move");  //For when player movement seems broken
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    private void CheckFlip()
    {
        switch (_movement.x)
        {
            case > 0 when !facingRight:
            case < 0 when facingRight:
                _flip();
                break;
        }
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
        facingRight = !facingRight;

        // Flip the player's scale on the X-axis
        float newYRotation = facingRight ? 0f : 180f;
        playerTransform.rotation = Quaternion.Euler(0f, newYRotation, 0f);
        
        _rayDirection = facingRight ? new Vector2(0.5f, -0.5f) : new Vector2(-0.5f, -0.5f);
    }

    private void WaterFlip()
    {
        facingRight = !facingRight;
        // Flip the player's scale on the X-axis
        sr.flipY = !facingRight;
    }
}
