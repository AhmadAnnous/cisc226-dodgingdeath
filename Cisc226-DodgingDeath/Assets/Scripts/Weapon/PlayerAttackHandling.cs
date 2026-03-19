using UnityEngine;
using System.Collections;
using UnityEngine.Accessibility;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    float timeUntilMelee = 0;
    bool eraseBullets = false;
    float abilityCDTimer = 0f;

    void Update()
    {
        if(timeUntilMelee <= 0f)
        {
            eraseBullets = false;
            if(Input.GetMouseButton(0))
            {
                GetComponent<HingeRotator>().rotate();
                anim.SetTrigger("Attack");
                timeUntilMelee = GameController.attackSpeed;
            }
            
        } 
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }

        
        if(abilityCDTimer <= 0f)
        {
            if(Input.GetKeyDown("c") && abilityCDTimer <= 0 && timeUntilMelee <= 0)
            {
                GetComponent<HingeRotator>().rotate();
                anim.SetTrigger("Spin");
                timeUntilMelee = GameController.spinDuration;
                eraseBullets = true;
                abilityCDTimer = GameController.abilityCD;
            }
        } 
        else
        {
            abilityCDTimer -= Time.deltaTime;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().takeDamage(GameController.playerDamage);
        }
        if(other.tag == "Bullet" && eraseBullets)
        {
            other.GetComponent<BulletController>().delete();
        }
    }



}
