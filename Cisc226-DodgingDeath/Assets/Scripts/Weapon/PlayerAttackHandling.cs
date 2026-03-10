using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] private float attackDelay;
    [SerializeField] private int damage;
    float timeUntilMelee = 0;
    


    void Update()
    {
        if(timeUntilMelee <= 0f)
        {
            
            if(Input.GetMouseButton(0))
            {
                Debug.Log("Swinging");
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
            other.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("damaged enemy");
        }
    }



}