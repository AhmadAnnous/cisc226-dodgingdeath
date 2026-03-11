using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType
{
    Melee,
    Ranged,
};

public class EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currstate = EnemyState.Wander;
    public EnemyType enemyType;
    public float range = 10;
    public float speed = 1;
    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randDir;
    public float attackRange;
    private bool cooldownAttack = false;
    public float cooldown;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;


    //placeholder values, feel free to edit
    public int health = 10;
    public double lifestealValue = 5;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currstate)
        {
            case(EnemyState.Wander):
                Wander();
                break;
            case(EnemyState.Follow):
                Follow();
                break;
            case(EnemyState.Die):
                break;
            case(EnemyState.Attack):
                Attack();
                break;

        }

        if(isPlayerInRange(range) && currstate != EnemyState.Die)
        {
            currstate = EnemyState.Follow;
        }
        else if(!isPlayerInRange(range) && currstate != EnemyState.Die)
        {
            currstate = EnemyState.Wander;
        }
        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currstate = EnemyState.Attack;
        }
    }

    private bool isPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randDir = new Vector3(0,0, Random.Range(0,360));
        Quaternion nextRotation = Quaternion.Euler(randDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }

    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;
        if(isPlayerInRange(range))
        {
            currstate = EnemyState.Follow;
        }
    }

    void Follow()
    {   
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if(!cooldownAttack)
        {
            switch(enemyType)
            {
                case(EnemyType.Melee):
                    GameController.DamagePlayer(10);
                    StartCoroutine(Cooldown());
                    break;
                case(EnemyType.Ranged):
                    Shoot();
                    StartCoroutine(Cooldown());
                    break;
            }
            
            
        }
    }

    private IEnumerator Cooldown()
    {
        cooldownAttack = true;
        yield return new WaitForSeconds(cooldown);
        cooldownAttack = false;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Vector2 direction = (player.transform.position - firePoint.position).normalized;
        bullet.GetComponent<BulletController>().Initialize(direction, bulletSpeed);
    }
    void Death()
    {
        player.GetComponent<PlayerHealth>().addHealth(lifestealValue);
        Destroy(gameObject);
    }

    public void takeDamage(int damageDealt)
    {
        health -= damageDealt;
        if(health <= 0)
        {
            Death();
        }
    }
}
