using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 200;
    public bool isActive = false;
    public GameObject winscreen;
    
    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
            isActive = false;
            winscreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Update()
    {
        if (!isActive)
        {
            if (IsNearCamera(6f))
        {
            isActive = true;
        }
        }
    }

    bool IsNearCamera(float range)
    {
        Vector2 camPos = Camera.main.transform.position;
        Vector2 enemyPos = transform.position;

        return Mathf.Abs(camPos.x - enemyPos.x) <= range &&
           Mathf.Abs(camPos.y - enemyPos.y) <= range;
    }
    
}
