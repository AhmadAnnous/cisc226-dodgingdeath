using System;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    int hp = 1;
    int iframes = 60;
    Boolean invulnerable = false;
    int currentIframe = 0;
    // Update is called once per frame
    void Update()
    {
        iframeCalc();

        if(hp <= 0)
        {
            
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        
        ScytheScript scytheScript = other.GetComponent<ScytheScript>();

        if (scytheScript != null)
        {
            if(scytheScript.active)
            {
                if(!invulnerable)
                {
                    Destroy(this.gameObject);
                }
            }

        }
        
    }
    void iframeCalc()
    {
        if(currentIframe != 0)
        {
            currentIframe++;
            invulnerable = true;
        }
        if(currentIframe > iframes)
        {
            currentIframe = 0;
            invulnerable = false;
        }
    }
}
