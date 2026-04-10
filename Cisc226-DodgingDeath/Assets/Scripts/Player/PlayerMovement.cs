using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float roomHeight = 12f;
    private float roomWidth;
    private Vector2 _movement;
    private Rigidbody2D _rb;

    private MapGenerator mapGenerator;
    [SerializeField] private float dashTransitionRatio;
    private float dashTimer = 0;
    private bool dashing;
    private Vector2 dashDir;
    public TMP_Text staminaText;
    public StaminaBar staminabar;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        mapGenerator = FindFirstObjectByType<MapGenerator>();
    }

    void Start()
    {
        GameController.stamina = GameController.maxStamina;
        staminabar.SetMaxStamina(GameController.maxStamina);
        roomWidth = roomHeight * (16f / 9f);
    }

    private void Update()
    {
        staminabar.SetStamina(GameController.stamina);
        staminaText.text = "Stamina: " + Mathf.Floor(GameController.stamina);

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        Vector2 position = transform.position;
        Vector2 velocity = _movement * GameController.moveSpeed;

        int roomX = Mathf.RoundToInt(position.x / roomWidth);
        int roomY = Mathf.RoundToInt(position.y / roomHeight);
        int currentIndex = roomY * 10 + roomX;

        float halfWidth = roomWidth / 2f;
        float halfHeight = roomHeight / 2f;
        float roomCenterX = roomX * roomWidth;
        float roomCenterY = roomY * roomHeight;
        float leftEdge = roomCenterX - halfWidth;
        float rightEdge = roomCenterX + halfWidth;
        float topEdge = roomCenterY + halfHeight;
        float bottomEdge = roomCenterY - halfHeight;

        // Dash logic
        checkDash(ref velocity);

        // RIGHT EDGE
        if (position.x > rightEdge - 0.2f && !mapGenerator.RoomExists(currentIndex + 1))
            velocity.x = Mathf.Min(0, velocity.x);

        // LEFT EDGE
        if (position.x < leftEdge + 0.2f && !mapGenerator.RoomExists(currentIndex - 1))
            velocity.x = Mathf.Max(0, velocity.x);

        // TOP EDGE
        if (position.y > topEdge - 0.2f && !mapGenerator.RoomExists(currentIndex + 10))
            velocity.y = Mathf.Min(0, velocity.y);

        // BOTTOM EDGE
        if (position.y < bottomEdge + 0.2f && !mapGenerator.RoomExists(currentIndex - 10))
            velocity.y = Mathf.Max(0, velocity.y);

        
        _rb.linearVelocity = velocity;

        staminaCalc();
    }

    private void checkDash(ref Vector2 velocity)
    {
        if (Input.GetKeyDown("v") && GameController.stamina >= 1)
        {
            GameController.stamina--;
            dashDir = _movement.normalized;
            dashing = true;
            dashTimer = 0;
        }

        if (dashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer < GameController.dashDuration * (1 - dashTransitionRatio))
            {
                velocity = dashDir * GameController.dashSpeed;
            }
            else if (dashTimer < GameController.dashDuration)
            {
                velocity = dashDir * GameController.dashSpeed * dashTransitionRatio;
            }
            else
            {
                dashing = false;
            }
        }
    }

    private void staminaCalc()
    {
        if(GameController.stamina <GameController.maxStamina)
        {
            GameController.stamina += GameController.staminaRegenRate * Time.deltaTime;
        }
        if(GameController.stamina >GameController.maxStamina)
        {
            GameController.stamina = (int)GameController.maxStamina;
        }
    }

}
