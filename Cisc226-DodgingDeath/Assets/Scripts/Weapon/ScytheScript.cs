using System;
using UnityEngine;

public class ScytheScript : MonoBehaviour
{
    Boolean clicking = false;
    public Boolean active = false;
    int frame = 0;
    int animLength = 30;


    
    void Update()
    {
        if(!active)
        {
            if(clicking)
            {
                active = true;
            }
        } else
        {
            if(frame >= animLength)
            {
                frame = 0;
                active = false;
                clicking = false;
            }
            else
            {
                frame++;
            }
        }
    }

    void OnMouseDown()
    {
        clicking = true;
    }


}
