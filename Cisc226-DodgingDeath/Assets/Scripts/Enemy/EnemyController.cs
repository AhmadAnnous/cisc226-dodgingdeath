using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyState
{
    Wander,
    Follow,
    Die
};

public class EnemyController : MonoBehaviour
{

    GameObject player;
    public EnemyState currstate = EnemyState.Wander;
    public float range = 10;
    public float speed = 1;
    private bool chooseDir = false;
    private bool dead = false;
    private Vector3 randDir;

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

        }

        if(isPlayerInRange(range) && currstate != EnemyState.Die)
        {
            currstate = EnemyState.Follow;
        }
        else if(!isPlayerInRange(range) && currstate != EnemyState.Die)
        {
            currstate = EnemyState.Wander;
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


    void Death()
    {
        Destory(gameObject);
    }
}
