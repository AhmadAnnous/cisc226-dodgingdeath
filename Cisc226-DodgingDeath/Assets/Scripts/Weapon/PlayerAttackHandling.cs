using UnityEngine;
using System.Collections;
using UnityEngine.Accessibility;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    float timeUntilMelee = 0;
    public bool eraseBullets = false;
    float abilityCDTimer = 0f;
    public TMP_Text abilitytext;
    public StaminaBar abilitybar;

    void Start()
    {
        abilitybar.SetMaxStamina((int) GameController.abilityCD);
    }

    void Update()
    {
        abilitybar.SetStamina(GameController.abilityCD - abilityCDTimer);
        if (abilityCDTimer <= 0)
        {
            abilitytext.text = "";
        }
        else
        {
            abilitytext.text = "" + (Mathf.Floor(abilityCDTimer) + 1);
        }
        

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
            abilityCDTimer -= Time.deltaTime * GameController.abilityRegenRate;
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
        if(other.tag == "Boss")
        {
            other.GetComponent<BossController>().takeDamage(GameController.playerDamage);
        }
        if(other.tag == "BossBullet" && eraseBullets)
        {
            other.GetComponent<BossBullet>().delete();
        }
    }



}
