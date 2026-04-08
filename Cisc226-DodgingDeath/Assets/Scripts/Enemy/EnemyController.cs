using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;

public enum EnemyState
{
    Wander,
    Follow,
    Idle,
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
    private Rigidbody2D rb;
    public EnemyState currstate = EnemyState.Idle;
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


    private MapGenerator mapGenerator;
    [SerializeField] private float roomSize = 12f;
    

    //placeholder values, feel free to edit
    public int health = 10;
    public float lifestealValue = 5;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        mapGenerator = FindFirstObjectByType<MapGenerator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!IsNearCamera(6f))
        {
            currstate = EnemyState.Idle;
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currstate = EnemyState.Attack;
        }
        else if (isPlayerInRange(range))
        {
            currstate = EnemyState.Follow;
        }
        else
        {
            currstate = EnemyState.Wander;
        }

        switch(currstate)
        {
            case EnemyState.Wander:
                Wander();
                break;

            case EnemyState.Follow:
                Follow();
                break;

            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Idle:
                Idle();
                break;
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

        rb.linearVelocity = -transform.right * speed;
        if(isPlayerInRange(range))
        {
            currstate = EnemyState.Follow;
        }
    }

    void Follow()
    {       
        Vector2 dir = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;    
    
    }

    void Attack()
    {
        rb.linearVelocity = Vector2.zero;
        if(!cooldownAttack)
        {
            switch(enemyType)
            {
                case EnemyType.Melee:
                    GameController.DamagePlayer(10);
                    StartCoroutine(Cooldown());
                    break;
                case EnemyType.Ranged:
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
        GameController.HealPlayer(lifestealValue);
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


    bool IsNearCamera(float range)
{
    Vector2 camPos = Camera.main.transform.position;
    Vector2 enemyPos = transform.position;

    return Mathf.Abs(camPos.x - enemyPos.x) <= range &&
           Mathf.Abs(camPos.y - enemyPos.y) <= range;
    }
    void Idle()
    {
        rb.linearVelocity = Vector2.zero;
    }
}

    