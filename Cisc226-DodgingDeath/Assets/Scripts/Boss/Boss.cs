using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 200;
    
    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
