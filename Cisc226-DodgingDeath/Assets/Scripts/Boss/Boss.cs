using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 200;
    public bool isActive = false;
    
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            GameController.youWin();
        }
    }
    
}
