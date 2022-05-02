using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private PlayerAction playerAction;

    private float moveSpeed = 4f;

    private float jumpForce = 6f;

    private Animator anim;

    private Vector3 move;

    private SpriteRenderer sprite;

    private GameManager gameManager;

    private ShopManager shopManager;

    private AudioManager audioManager;

    Rigidbody2D rb;

    Vector2 input;

    BoxCollider2D coll;

    [SerializeField]
    private LayerMask jumpAbleGround;

    private enum PlayerState
    {
        idle,
        running,
        jumping,
        falling
    }

    private void Awake()
    {
        playerAction = new PlayerAction();
        playerAction.Player.Jump.performed += _ => Jump();
        gameManager = GameManager.instance;
        shopManager = ShopManager.instance;
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        moveSpeed = shopManager.character[gameManager.selectedCharacter].speed;
        jumpForce =
            shopManager.character[gameManager.selectedCharacter].jumpForce;
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerAction.Enable();
    }

    private void OnDisable()
    {
        playerAction.Disable();
    }

    void Update()
    {
        input = playerAction.Player.Move.ReadValue<Vector2>();
        move = new Vector2(input.x, 0);
        transform.Translate((move * moveSpeed) * Time.deltaTime);
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        PlayerState state;
        if (input.x > 0f)
        {
            state = PlayerState.running;
            sprite.flipX = false;
        }
        else if (input.x < 0f)
        {
            state = PlayerState.running;
            sprite.flipX = true;
        }
        else
        {
            state = PlayerState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = PlayerState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = PlayerState.falling;
        }
        anim.SetInteger("state", (int) state);
    }

    void Jump()
    {
        if (IsGround())
        {
            audioManager.PlaySound("Jump");
            rb.velocity = new Vector3(0, jumpForce, 0);
        }
    }

    private bool IsGround()
    {
        return Physics2D
            .BoxCast(coll.bounds.center,
            coll.bounds.size,
            0f,
            Vector2.down,
            .1f,
            jumpAbleGround);
    }
}
