using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static float health = 50;
    public static int maxHealth = 120;
    public static float moveSpeed = 5f;
    public static int playerDamage = 4;
    public static float attackSpeed = 0.5f;
    public static float healthDrainPerSec = 1f;
    public static float spinDuration = 0.17f;
    public static float abilityCD = 3f;
    public static float abilityRegenRate = 1f;
    
    public static float Health{ get => health; set => health = value; }
    public static int Damage { get => playerDamage; set => playerDamage = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }  
    public static float HealthDrainPerSec { get => healthDrainPerSec; set => healthDrainPerSec = value; }
    public TMP_Text healthText;
    public HealthBar healthbar;
    public TMP_Text stattext;

    public static int maxStamina = 3;
    public static float stamina = 3f;
    public static float staminaRegenRate = 0.3f;
    public static float dashSpeed = 20f;
    public static float dashDuration = 0.2f;





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

        stattext.text = "Max Health: " + maxHealth + "\nDamage: " + playerDamage 
        + "\nMove Speed: " + moveSpeed + "\nAttack Speed: " + attackSpeed
        + "\nMax Stamina: " + maxStamina;

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
        // health = 0;
        // Time.timeScale = 0;
    }
}
