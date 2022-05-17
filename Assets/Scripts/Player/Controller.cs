using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("For Movement")]
    private float moveSpeed;

    private float airMoveSpeed;

    private float XDirectionalInput;

    private bool facingRight = true;

    private bool isMoving;

    [Header("For Jumping")]
    private float jumpForce;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    Transform groundCheckPoint;

    [SerializeField]
    Vector2 groundCheckSize;

    private bool grounded;

    private bool canJump;

    [Header("For WallSliding")]
    [SerializeField]
    float wallSlideSpeed;

    [SerializeField]
    LayerMask wallLayer;

    [SerializeField]
    Transform wallCheckPoint;

    [SerializeField]
    Vector2 wallCheckSize;

    private bool isTouchingWall;

    private bool isWallSliding;

    [Header("For WallJumping")]
    [SerializeField]
    float walljumpforce;

    [SerializeField]
    Vector2 walljumpAngle;

    [SerializeField]
    float walljumpDirection = -1;

    [Header("Other")]
    private Animator anim;

    private Rigidbody2D rb;

    private PlayerAction playerAction;

    private enum PlayerState
    {
        idle,
        running,
        jumping,
        falling,
        hang
    }

    PlayerState state;

    private GameManager gameManager;

    private ShopManager shopManager;

    private AudioManager audioManager;

    private void Awake()
    {
        playerAction = new PlayerAction();
        playerAction.Player.Jump.performed += _ => DoJump();

        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        walljumpAngle.Normalize();
        moveSpeed = shopManager.character[gameManager.selectedCharacter].speed;
        jumpForce =
            shopManager.character[gameManager.selectedCharacter].jumpForce;
        airMoveSpeed =
            shopManager.character[gameManager.selectedCharacter].speed + 3;
    }

    private void OnEnable()
    {
        playerAction.Enable();
    }

    private void OnDisable()
    {
        playerAction.Disable();
    }

    private void Update()
    {
        Inputs();
        CheckWorld();
        AnimationControl();
    }

    private void FixedUpdate()
    {
        Movement();
        WallSlide();
    }

    void Inputs()
    {
        XDirectionalInput = playerAction.Player.Move.ReadValue<Vector2>().x;
    }

    void CheckWorld()
    {
        grounded =
            Physics2D
                .OverlapBox(groundCheckPoint.position,
                groundCheckSize,
                0,
                groundLayer);
        isTouchingWall =
            Physics2D
                .OverlapBox(wallCheckPoint.position,
                wallCheckSize,
                0,
                wallLayer);
    }

    void Movement()
    {
        //for Animation
        if (XDirectionalInput != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        rb.velocity = new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);

        //for movement
        // if (grounded)
        // {
        //     rb.velocity =
        //         new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);
        // }
        // else if (
        //     !grounded &&
        //     (!isWallSliding || !isTouchingWall) &&
        //     XDirectionalInput != 0
        // )
        // {
        //     rb.AddForce(new Vector2(airMoveSpeed * XDirectionalInput, 0));
        //     if (Mathf.Abs(rb.velocity.x) > moveSpeed)
        //     {
        //         rb.velocity =
        //             new Vector2(XDirectionalInput * moveSpeed, rb.velocity.y);
        //     }
        // }
        //for fliping
        if (XDirectionalInput < 0 && facingRight)
        {
            Flip();
        }
        else if (XDirectionalInput > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        if (!isWallSliding)
        {
            walljumpDirection *= -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }
    }

    void DoJump()
    {
        canJump = true;
        if (canJump && grounded)
        {
            Jump();
        }
        else if ((isWallSliding) && canJump)
        {
            WallJump();
        }
    }

    void Jump()
    {
        // rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        rb.velocity = new Vector3(0, jumpForce, 0);
        canJump = false;
    }

    void WallSlide()
    {
        if (isTouchingWall && !grounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
    }

    void WallJump()
    {
        rb
            .AddForce(new Vector2(walljumpforce *
                walljumpAngle.x *
                walljumpDirection,
                walljumpforce * walljumpAngle.y),
            ForceMode2D.Impulse);
        Flip();
        canJump = false;
    }

    void AnimationControl()
    {
        if (isMoving)
        {
            state = PlayerState.running;
        }
        else
        {
            state = PlayerState.idle;
        }
        if (isTouchingWall)
        {
            state = PlayerState.hang;
        }
        else
        {
            if (rb.velocity.y > .1f)
            {
                state = PlayerState.jumping;
            }
            else if (rb.velocity.y < -.1f)
            {
                state = PlayerState.falling;
            }
        }
        anim.SetInteger("state", (int) state);
        // anim.SetBool("isMoving", isMoving);
        // anim.SetBool("isGrounded", grounded);
        // anim.SetBool("isSliding", isTouchingWall);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
}
