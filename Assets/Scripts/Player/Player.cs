using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isInShadow { get; private set; }


    [Header("Layers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask lightLayer;

    
    [Header("GameObjects")]
    [SerializeField] GameObject lightSource;
    [SerializeField] GameObject feet;


    [Header("Run & Jump")]
    [SerializeField] float runSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float groundDetectionRayLength;
    [SerializeField] float cayoteTime;
    [SerializeField] float jumpBuffer;
    float cayoteTimeCounter;
    float jumpBufferCounter;
    float moveInput;


    // [Header("Inventory")]


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJump();

        Debug.DrawLine(rb.position, lightSource.transform.position, Color.blue);
        if (Physics2D.Linecast(rb.position, lightSource.transform.position, lightLayer))
        {
            isInShadow = true;
        }
        else
        {
            isInShadow = false;
        }

        if(rb.velocity.y < 0){
            rb.gravityScale = 4;
        }else{
            rb.gravityScale = 3;
        }
    }

    void HandleJump(){
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

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * runSpeed, rb.velocity.y);
    }
}
