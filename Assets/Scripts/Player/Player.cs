using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D myCollider;
    LadderClimb ladderClimbScript;
    VcamManager vcamManager;
    SpriteRenderer sr;
    TrailRenderer trailRenderer;
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
    [SerializeField] GameObject feet;

    [Header("Run & Jump")]
    public float runSpeed = 5;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField, Range(0, 1)] float inLightSlowdownValue;
    [SerializeField] float jumpForce;
    [SerializeField] float doubleJumpForce;
    [SerializeField] float groundDetectionRadius;
    [SerializeField] float cayoteTime;
    [SerializeField] float jumpBuffer;
    float cayoteTimeCounter;
    float jumpBufferCounter;
    float moveInput;
    bool canRun;
    bool canJump;
    bool grounded;
    bool canDoubleJump;
    bool canDash = true;
    bool isDashing;
    bool isInvincible;

    //abilities bool
    public bool unlockDash = false;
    public bool unlockDoubleJump = false;
    public bool unlockInvincible = false;


    [Header("Invincible")]
    [SerializeField] float invincibleTime;
    [SerializeField] float invicibilitySpeed;
    [SerializeField, Range(0, 1)] float invicibilityAlpha;

    [Header("Death")]
    [SerializeField] float deathRiseSpeed;
    bool checkForDestroy = false;
    float deathHeight;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ladderClimbScript = GetComponent<LadderClimb>();
        myCollider = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        vcamManager = GameObject.FindObjectOfType<VcamManager>();
        vcamManager.isDead = false;

        defaultGravity = 3f;
        fallGravity = 4f;

        isInShadow = true;
        ladderClimbScript.enabled = false;
        canJump = true;
        canRun = true;
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        moveInput = Input.GetAxisRaw("Horizontal");
        if (canRun)
        {
            if (!isInvincible)
            {
                if (!isInShadow)
                {
                    rb.velocity = new Vector2(moveInput * runSpeed * inLightSlowdownValue, rb.velocity.y);
                }
                else
                {
                    isInShadow = true;
                    rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
                }
            }
            else if(isInvincible)
            {
                rb.velocity = new Vector2(moveInput * invicibilitySpeed, rb.velocity.y);
            }
        }
    }
    void Update()
    {
        if (isDashing)
        {
            return;
        }

        JumpHandler();
        FlipHandler();
        DoubleJump();
        Dash();
        Invincible();

        if (!grounded && rb.velocity.y < 0)
        {
            rb.gravityScale = fallGravity;
            anim.SetBool("isFalling", true);
            anim.SetBool("onJump", false);
        }
        else if (grounded)
        {
            rb.gravityScale = defaultGravity;
            anim.SetBool("isFalling", false);
        }else if(rb.velocity.y > 0){
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
            myCollider.enabled = false;

            if (transform.position.y <= deathHeight)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            DeathDetection();
        }

    }

    void DoubleJump()
    {
        if (canDoubleJump && Input.GetKeyDown(KeyCode.Space) && !grounded)
        {

            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            canDoubleJump = false;
            unlockDoubleJump = false;
        }
    }

    void Dash()
    {
        if (unlockDash)
        {
            if (canDash && Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(DoDash());
                unlockDash = false;
            }
        }
    }

    private IEnumerator DoDash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveInput * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void Invincible()
    {
        if (unlockInvincible)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !isInvincible)
            {
                isInvincible = true;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, invicibilityAlpha);
                Invoke("Vulnerable", invincibleTime);
                unlockInvincible = false;
            }
        }
    }

    void Vulnerable()
    {
        isInvincible = false;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
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

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        isFlipped = !isFlipped;
    }

    void DeathDetection()
    {
        bool isDeathTile = Physics2D.OverlapCircle(feet.transform.position, groundDetectionRadius, deathLayer);
        if (isDeathTile)
        {
            Die(isDeathTile);
        }
    }

    public void Die(bool deathTile)
    {
        if(deathTile){
            rb.velocity = Vector2.zero;
        }
        rb.velocity = new Vector2(rb.velocity.x, deathRiseSpeed);
        checkForDestroy = true;
        canRun = false;
        deathHeight = transform.position.y - 10f;
        vcamManager.isDead = true;
    }

    void JumpHandler()
    {
        grounded = Physics2D.OverlapCircle(feet.transform.position, groundDetectionRadius, groundLayer);
        if (grounded)
        {
            cayoteTimeCounter = cayoteTime;
            if (unlockDoubleJump)
            {
                canDoubleJump = true;
            }
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
                if(!isInvincible) {
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
        switch (other.tag)
        {
            case "Light":
                if(!isInvincible) {
                    isInShadow = false;
                    canJump = false;
                }
                break;
            case "Ladder":
                SwitchToLadder();
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canJump = true;
        isInShadow = true;
    }

}
