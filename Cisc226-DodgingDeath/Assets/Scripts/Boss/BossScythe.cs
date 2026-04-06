using UnityEngine;
using UnityEngine.UIElements;

public class BossScythe : MonoBehaviour
{
    private int damage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameController.DamagePlayer(damage);
        }

    }
}
