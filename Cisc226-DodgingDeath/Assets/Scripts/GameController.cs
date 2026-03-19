using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static float health = 50;
    private static int maxHealth = 120;
    private static float moveSpeed = 5f;
    private static int playerDamage = 1;
    private static float attackSpeed = 0.5f;
    private static float healthDrainPerSec = 1f;
    
    public static float Health{ get => health; set => health = value; }
    public static int Damage { get => playerDamage; set => playerDamage = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }  
    public static float HealthDrainPerSec { get => healthDrainPerSec; set => healthDrainPerSec = value; }
    public TMP_Text healthText;
    public HealthBar healthbar;

    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        health -= Time.deltaTime * healthDrainPerSec;
        healthbar.SetHealth(health);
        healthText.text = "Health: " + Mathf.Floor(health);
        if (health <= 0)
        {
            KillPlayer();
        }
    }

    

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            KillPlayer();
        }

    }
    
    public static void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    private static void KillPlayer()
    {
        health = 0;
        Time.timeScale = 0;
    }
}
