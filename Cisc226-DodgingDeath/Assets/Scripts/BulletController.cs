using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    public float lifetime = 5f;

    public void Initialize(Vector2 dir, float bulletSpeed)
    {
        direction = dir;
        speed = bulletSpeed;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameController.DamagePlayer(5);
            Destroy(gameObject);
        }

    }
}