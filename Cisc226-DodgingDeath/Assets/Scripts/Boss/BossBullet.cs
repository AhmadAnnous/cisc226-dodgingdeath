using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float angle = 0f;
    private float rotSpeed = 720f;
    private float speed = 12f;
    private int damage = 5;
    public float lifetime = 5f;
    private Vector2 dir;
    private GameObject player;
    private Rigidbody2D rb;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        dir = (player.transform.position - transform.position).normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        angle += rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.linearVelocity = dir * speed;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameController.DamagePlayer(damage);
            delete();
        }

    }

    public void delete()
    {
        Destroy(gameObject);
    }
}
