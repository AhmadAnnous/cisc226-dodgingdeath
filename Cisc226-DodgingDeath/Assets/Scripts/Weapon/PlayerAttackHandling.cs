using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] public static float attackDelay;
    [SerializeField] public static int damage;
    float timeUntilMelee = 0;
    


    public void Start()
    {
        attackDelay = 1f;
        damage = 3;
    }

    void Update()
    {
        if(timeUntilMelee <= 0f)
        {
            
            if(Input.GetMouseButton(0))
            {
                GetComponent<HingeRotator>().rotate();
                anim.SetTrigger("Attack");
                timeUntilMelee = attackDelay;
            }
        } else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().takeDamage(damage);
        }
    }



}