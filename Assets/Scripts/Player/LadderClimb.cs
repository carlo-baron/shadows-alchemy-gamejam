using System.Collections;
using System.Collections.Generic;
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
    }

    void OnTriggerExit2D(Collider2D other){
        rb.gravityScale = mainScript.defaultGravity;
        mainScript.enabled = true;
        this.enabled = false;
    }
}
