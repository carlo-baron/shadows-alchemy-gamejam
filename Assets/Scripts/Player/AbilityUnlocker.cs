using UnityEngine;

public class AbilityUnlocker : MonoBehaviour
{
    public int numberOfAbilities = 0;
    Player player;

    void Awake(){
        player = GetComponent<Player>();
    }
    

    public void SetAbilities(){
        switch(numberOfAbilities){
            case 1:
                player.unlockDash = true;
                break;
            case 2:
                player.unlockDoubleJump = true;
                break;
            case 3:
                player.unlockInvincible = true;
                break;
            default:
                break;
        }
        GameManager.abilities = numberOfAbilities;
    }
    public void ReplenishAbility(){
        switch(numberOfAbilities){
            case 1:
                player.unlockDash = true;
                break;
            case 2:
                player.unlockDash = true;
                player.unlockDoubleJump = true;
                break;
            case 3:
                player.unlockDash = true;
                player.unlockDoubleJump = true;
                player.unlockInvincible = true;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Replenish"){
            ReplenishAbility();
        }
    }
}
