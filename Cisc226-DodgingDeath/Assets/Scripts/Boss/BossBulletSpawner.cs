using UnityEngine;
using System.Collections;


public class BossBulletSpawner : MonoBehaviour
{
    private bool onCooldown = false;
    private float cooldown = 0.5f;
    private float waveCooldown = 8f;
    private int waveNum = 5;
    private int waveCount = 1;
    public GameObject BossBullet;
    public Vector3 thisLocation = new Vector3(0,0,0);
    

    //PLEASE CALL THIS ON SPAWN OF BOSS
    public void setLoc(Vector3 location)
    {   
        thisLocation = location;
    }

    void Update()
    {
        Vector2 randLoc;
        
        if(!onCooldown)
        {
            randLoc = generateRand3();
            if(GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>().isActive)
            {
                Shoot(randLoc);
            }
            
        }
    }
    private IEnumerator Cooldown()
    {
        onCooldown = true;
        if(waveCount < waveNum)
        {
            yield return new WaitForSeconds(cooldown);
            waveCount++;
        } else
        {
            yield return new WaitForSeconds(waveCooldown);
            waveCount = 0;
        }
        onCooldown = false;
    }

    void Shoot(Vector3 location)
    {
        GameObject bullet = Instantiate(BossBullet, location, Quaternion.Euler(0,0,0));
        StartCoroutine(Cooldown());
    } 

    private Vector3 generateRand3()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);

        Vector3 loc = new Vector3(x, y, 0);
        
        while(loc.magnitude < 10)
        {
            loc *= 1.1f;
        }

        return loc + thisLocation;
    }
}
