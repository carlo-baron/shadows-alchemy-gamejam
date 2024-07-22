using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    bool isFlipped = false;
    public bool isInShadow { get; private set; }

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
    [SerializeField] float groundDetectionRayLength;
    [SerializeField] float cayoteTime;
    [SerializeField] float jumpBuffer;
    float cayoteTimeCounter;
    float jumpBufferCounter;
    float moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isInShadow = true;
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if(!isInShadow){
            rb.velocity = new Vector2(moveInput * runSpeed * inLightSlowdownValue, rb.velocity.y);
        }else{
            rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
        }
    }
        void Update()
    {
        JumpHandler();
        FlipHandler();

        //transform.position starts at pivot, the player pivot is at the bottom. I added offset so the raycast wont be hitting the ground all the time
        if(lightSource != null){
            if (Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y + 1f), lightSource.transform.position, lightLayer))
            {
                isInShadow = true;
            }
            else
            {
                isInShadow = false;
            }
        }

        if(rb.velocity.y < 0){
            rb.gravityScale = 4;
        }else{
            rb.gravityScale = 3;
        }

        if(moveInput != 0){
            anim.SetBool("run", true);
        }else{
            anim.SetBool("run", false);
        }

        
    }

    void FlipHandler(){
        if(moveInput > 0 && isFlipped){
            transform.Rotate(0, 180, 0);
            isFlipped = !isFlipped;
        }else if(moveInput < 0 && !isFlipped){
            transform.Rotate(0, 180, 0);
            isFlipped = !isFlipped;
        }
    }

    void JumpHandler(){
        RaycastHit2D groundDetect = Physics2D.Raycast(feet.transform.position, Vector2.down, groundDetectionRayLength, groundLayer);
        if (groundDetect.collider != null)
        {
            cayoteTimeCounter = cayoteTime;
        }
        else
        {
            cayoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            jumpBufferCounter = jumpBuffer;
        }else{
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && cayoteTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if(Input.GetKey(KeyCode.Space)){
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpBufferCounter = 0;
            }else{
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *0.5f);
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y *0.5f);
            cayoteTimeCounter = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Light")){
            isInShadow = false;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        isInShadow = true;
    }

}
