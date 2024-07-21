using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Player player;
    [SerializeField] Slider healthBar;

    [SerializeField] float maxHealth;
    float health;

    [SerializeField] float healPerSecond;
    [SerializeField] float damagePerSecond;

    void Start()
    {
        player = GetComponent<Player>();
        health = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    void Update()
    {
        if(player.isInShadow){
            Heal(healPerSecond);
        }else{
            Damage(damagePerSecond);
        }
    }

    void Heal(float healPerSecond){
        if(health < maxHealth){
            health += healPerSecond * Time.deltaTime;
            healthBar.value = health;
        }
    }

    void Damage(float damagePerSecond){
        if(health > 0){
            health -= damagePerSecond * Time.deltaTime;
            healthBar.value = health;
        }
    }
}
