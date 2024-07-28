using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D myCollider;
    LadderClimb ladderClimbScript;
    bool isFlipped = false;
    public Animator anim { get; private set; }
    public bool isInShadow { get; private set; }
    public float defaultGravity { get; private set; }
    public float fallGravity { get; private set; }

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask lightLayer;
    [SerializeField] LayerMask deathLayer;

    [Header("GameObjects")]
    [SerializeField] GameObject lightSource;
    [SerializeField] GameObject feet;

    [Header("Run & Jump")]
    [SerializeField] float runSpeed;
    [SerializeField, Range(0, 1)] float inLightSlowdownValue;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDetectionRadius;
    [SerializeField] float cayoteTime;
    [SerializeField] float jumpBuffer;
    float cayoteTimeCounter;
    float jumpBufferCounter;
    float moveInput;
    bool canRun;
    bool canJump;
    bool grounded;

    [Header("Death")]
    [SerializeField] float deathRiseSpeed;
    bool checkForDestroy = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ladderClimbScript = GetComponent<LadderClimb>();
        myCollider = GetComponent<Collider2D>();

        defaultGravity = 3f;
        fallGravity = 4f;

        isInShadow = true;
        ladderClimbScript.enabled = false;
        canJump = true;
        canRun = true;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (canRun)
        {
            if (!isInShadow)
            {
                rb.velocity = new Vector2(moveInput * runSpeed * inLightSlowdownValue, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
            }
        }
    }
    void Update()
    {
        JumpHandler();
        FlipHandler();

        if (lightSource != null)
        {
            if (Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + 1f), lightSource.transform.position, lightLayer))
            {
                isInShadow = true;
            }
            else
            {
                isInShadow = false;
            }
        }

        if (!grounded && rb.velocity.y < 0)
        {
            rb.gravityScale = fallGravity;
            anim.SetBool("isFalling", true);
            anim.SetBool("onJump", false);
        }
        else
        {
            rb.gravityScale = defaultGravity;
            anim.SetBool("isFalling", false);
        }

        if (moveInput != 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }

        if (checkForDestroy)
        {
            if (rb.velocity.y < 0)
            {
                myCollider.enabled = false;
            }

            if (transform.position.y <= -10f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            DeathDetection();
        }

    }

    void FlipHandler()
    {
        if (moveInput > 0 && isFlipped)
{
            Flip();
        }
        else if (moveInput < 0 && !isFlipped)
        {
            Flip();
        }
    }

    public void Flip(){
        transform.Rotate(0, 180, 0);
        isFlipped = !isFlipped;
    }

    void DeathDetection()
    {
        bool isDeathTile = Physics2D.OverlapCircle(feet.transform.position, groundDetectionRadius, deathLayer);
        if (isDeathTile)
        {
            Die();
        }
    }

    public void Die()
    {
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(rb.velocity.x, deathRiseSpeed);
        checkForDestroy = true;
        canRun = false;
    }

    void JumpHandler()
    {
        grounded = Physics2D.OverlapCircle(feet.transform.position, groundDetectionRadius, groundLayer);
        if (grounded)
        {
            cayoteTimeCounter = cayoteTime;
        }
        else
        {
            cayoteTimeCounter -= Time.deltaTime;
        }

        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpBufferCounter = jumpBuffer;
                anim.SetBool("onJump", true);
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (jumpBufferCounter > 0 && cayoteTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpBufferCounter = 0;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                }
            }

            if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                cayoteTimeCounter = 0;
            }
        }

    }

    void SwitchToLadder()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        ladderClimbScript.enabled = true;
        anim.SetBool("onLadder", true);

        anim.SetBool("onJump", false);
        anim.SetBool("isFalling", false);
        this.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Light":
                lightSource = other.gameObject;
                if (lightSource.GetComponent<RotatingLamp>() != null)
                {
                    if (lightSource.GetComponent<RotatingLamp>().playerDetected)
                    {
                        isInShadow = false;
                        canJump = false;
                    }
                }
                else
                {
                    isInShadow = false;
                    canJump = false;
                }
                break;
            case "Ladder":
                SwitchToLadder();
                break;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
            SwitchToLadder();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        lightSource = null;
        canJump = true;
        isInShadow = true;
    }

}
