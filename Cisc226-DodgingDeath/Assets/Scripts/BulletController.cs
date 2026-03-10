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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameController.DamagePlayer(5);
            Destroy(gameObject);
        }

    }
}