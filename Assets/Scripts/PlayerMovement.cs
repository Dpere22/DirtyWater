using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Events;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsSwimming = Animator.StringToHash("isSwimming");
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EdgeCollider2D col;
    [SerializeField] private CapsuleCollider2D capsule;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite walkSprite;
    [SerializeField] private Sprite swim;
    [SerializeField] private Sprite normal;
    [SerializeField] private Transform enterWaterPoint;
    private Animator _animator;
    private Vector2 _playerSpawn;
    
    private Quaternion _playerWalkRotation;
    private Vector2 _movement;

    private bool _canMove = true;
    public bool isJumping;
    private bool _isSwimming;
    public bool atWaterSurface;
    public float groundCheckDistance = 1f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    [FormerlySerializedAs("_facingRight")] public bool facingRight;
    private Vector2 _rayDirection = new(0.15f, -0.15f);
    


    private delegate void FlipOperation();
    private FlipOperation _flip;
    
    
    private delegate void MoveOperation();
    private MoveOperation _move;
    private MoveOperation _previousMove;
    
    void Start()
    {
        _playerSpawn = rb.transform.position;
        _animator = GetComponentInChildren<Animator>();
        _flip = WalkFlip;
        _move = Walk;
        _playerWalkRotation = transform.rotation;
        GameEventsManager.Instance.PauseEvents.OnPause += PauseHandler;
        GameEventsManager.Instance.PauseEvents.OnResume += ResumeHandler;
        GameEventsManager.Instance.InputEvents.MoveAction += OnMove;
        GameEventsManager.Instance.DayEvents.OnJumpIntoWater += JumpIntoWater;
        GameEventsManager.Instance.PlayerEvents.OnDisablePlayerMovement += RestrictMovement;
        GameEventsManager.Instance.PlayerEvents.OnEnablePlayerMovement += EnableMovement;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetSwim += SetPlayerSwimming;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetWalk += SetPlayerWalking;
        GameEventsManager.Instance.DayEvents.OnRespawnPlayer += HandleRespawn;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.PauseEvents.OnPause -= PauseHandler;
        GameEventsManager.Instance.PauseEvents.OnResume -= ResumeHandler;
        GameEventsManager.Instance.InputEvents.MoveAction -= OnMove;
        GameEventsManager.Instance.DayEvents.OnJumpIntoWater -= JumpIntoWater;
        GameEventsManager.Instance.PlayerEvents.OnDisablePlayerMovement -= RestrictMovement;
        GameEventsManager.Instance.PlayerEvents.OnEnablePlayerMovement -= EnableMovement;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetSwim -= SetPlayerSwimming;
        GameEventsManager.Instance.PlayerEvents.OnPlayerSetWalk -= SetPlayerWalking;
        GameEventsManager.Instance.DayEvents.OnRespawnPlayer -= HandleRespawn;
    }

    private void Update()
    {
        _animator.SetBool(IsSwimming, _isSwimming);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _move?.Invoke();
        _animator.SetFloat(XVelocity, Math.Abs(rb.linearVelocity.magnitude));
    }

    private void ResumeHandler()
    {
        EnableMovement();
    }

    private void PauseHandler()
    {
        RestrictMovement();
    }

    private void RestrictMovement()
    {
        rb.linearVelocity = Vector2.zero;
        _canMove = false;
    }

    private void EnableMovement()
    {
        _canMove = true;
    }

    private void HandleRespawn()
    {
        rb.transform.position = _playerSpawn;
    }
    
    public void OnMove(Vector2 dir)
    {
        _movement = dir;
    }
    private void JumpIntoWater()
    {
        StartCoroutine(WaitForFadeOut());
    }

    IEnumerator WaitForFadeOut()
    {
        yield return new WaitForSeconds(1f);
        rb.linearVelocity = Vector2.zero;
        isJumping = true;
        rb.position = enterWaterPoint.position;
        SetPlayerSwimming();
        StartCoroutine(WaitOneFrame());
    }

    IEnumerator WaitOneFrame()
    {
        yield return new WaitForSeconds(0.5f);
        GameEventsManager.Instance.DayEvents.StartDayTimer();
        GameEventsManager.Instance.DayEvents.EnterWater();
    }

    public void OnOpenInventory(InputValue value)
    {
        Debug.Log("Input Recieved");
    }

    private void SetPlayerSwimming()
    {
        _isSwimming = true;
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
        _isSwimming = false;
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
            rb.linearVelocity = _movement.y < 0 ? new Vector2(_movement.x, _movement.y).normalized * GameEventsManager.Instance.PlayerManager.SwimmingSpeed : new Vector2(_movement.x, 0).normalized * GameEventsManager.Instance.PlayerManager.SwimmingSpeed ;
            playerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            rb.linearVelocity = new Vector2(_movement.x, _movement.y).normalized * GameEventsManager.Instance.PlayerManager.SwimmingSpeed;
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
            rb.linearVelocity = new Vector2(_movement.x, 0).normalized * GameEventsManager.Instance.PlayerManager.WalkingSpeed;
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
