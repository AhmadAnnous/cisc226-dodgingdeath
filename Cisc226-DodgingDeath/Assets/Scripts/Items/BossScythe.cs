using UnityEngine;
using UnityEngine.UIElements;

public class BossScythe : MonoBehaviour
{
    private float degPerSec = 30;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameController.DamagePlayer(5);
        }

    }

    void Update()
    {
        
    }
}
