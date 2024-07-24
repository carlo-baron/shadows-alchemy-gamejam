using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    LadderClimb ladderClimbScript;
    bool isFlipped = false;
    public bool isInShadow { get; private set; }
    public float defaultGravity { get; private set; }
    public float fallGravity { get; private set;}

    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask lightLayer;

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
    bool canJump;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ladderClimbScript = GetComponent<LadderClimb>();

        defaultGravity = 3f;
        fallGravity = 4f;

        isInShadow = true;
        ladderClimbScript.enabled = false;
        canJump = true;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (!isInShadow)
        {
            rb.velocity = new Vector2(moveInput * runSpeed * inLightSlowdownValue, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
        }
    }
    void Update()
    {
        if(canJump){
            JumpHandler();
        }
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

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallGravity;
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }

        if (moveInput != 0)
        {
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
        }


    }

    void FlipHandler()
    {
        if (moveInput > 0 && isFlipped)
        {
            transform.Rotate(0, 180, 0);
            isFlipped = !isFlipped;
        }
        else if (moveInput < 0 && !isFlipped)
        {
            transform.Rotate(0, 180, 0);
            isFlipped = !isFlipped;
        }
    }

    void JumpHandler()
    {
        bool grounded = Physics2D.OverlapCircle(feet.transform.position, groundDetectionRadius, groundLayer);
        if (grounded)
        {
            cayoteTimeCounter = cayoteTime;
        }
        else
        {
            cayoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBuffer;
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

    void SwitchToLadder(){
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        ladderClimbScript.enabled = true;
        this.enabled = false;
}

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Light":
                isInShadow = false;
                canJump = false;
                lightSource = other.gameObject;
                break;
            case "Ladder":
               SwitchToLadder();
               break;
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Ladder"){
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
