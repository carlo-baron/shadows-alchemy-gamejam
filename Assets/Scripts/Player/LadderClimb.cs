using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [Header("Climb")]
    [SerializeField] float climbSpeed;

    Rigidbody2D rb;
    float moveInputX, moveInputY;

    Player mainScript;

    void Awake()
    {
        mainScript = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveInputX * climbSpeed, moveInputY * climbSpeed);

        if(rb.velocity.magnitude != 0f){
            mainScript.anim.SetBool("isClimbing", true);
        }else{
            mainScript.anim.SetBool("isClimbing", false);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        rb.gravityScale = mainScript.defaultGravity;
        mainScript.anim.SetBool("onLadder", false);
        mainScript.anim.SetBool("isClimbing", false);
        mainScript.enabled = true;
        this.enabled = false;
    }
}
