using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ToggleLight : MonoBehaviour
{
    Light2D myLight;
    Collider2D myCollider;

    [SerializeField] float offCooldown = 1f;
    [SerializeField] float onCooldown = 1f;
    float nextToggleTime;
    bool isLightOn;

    enum State{
        Blink,
        Hold
    }

    State myState = State.Blink;

    void Awake()
    {
        myLight = GetComponent<Light2D>();
        myCollider = GetComponent<Collider2D>();

        nextToggleTime = Time.time + onCooldown;
        isLightOn = true;
    }

    void Update()
    {
        switch(myState){
            case State.Blink:
                Blink();
                break;
            case State.Hold:
                Hold();
                break;
        }
        
    }

    void Blink(){
        if (Time.time >= nextToggleTime)
        {
            isLightOn = !isLightOn;
            myLight.enabled = isLightOn;
            myCollider.enabled = isLightOn;

            if (isLightOn)
            {
                nextToggleTime = Time.time + onCooldown;
            }
            else
            {
                nextToggleTime = Time.time + offCooldown;
            }
        }
    }

    void Hold(){
        myLight.enabled = true;
        myCollider.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            myState++;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "Player"){
            myState--;
            // nextToggleTime = Time.time + onCooldown;
        }
    }
    
}
