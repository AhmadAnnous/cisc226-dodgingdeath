using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public static float moveSpeed = GameController.moveSpeed;
    [SerializeField] private float roomSize = 12f;

    private Vector2 _movement;
    private Rigidbody2D _rb;

    private MapGenerator mapGenerator;
    [SerializeField] private int maxStamina;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
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
        stamina = maxStamina;
        staminabar.SetMaxStamina(maxStamina);
    }

    private void Update()
    {
        staminabar.SetStamina(stamina);
        staminaText.text = "Stamina: " + Mathf.Floor(stamina);

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        Vector2 position = transform.position;
        Vector2 velocity = _movement * moveSpeed;

        int roomX = Mathf.RoundToInt(position.x / roomSize);
        int roomY = Mathf.RoundToInt(position.y / roomSize);
        int currentIndex = roomY * 10 + roomX;

        float halfRoom = roomSize / 2f;
        float roomCenterX = roomX * roomSize;
        float roomCenterY = roomY * roomSize;
        float leftEdge = roomCenterX - halfRoom;
        float rightEdge = roomCenterX + halfRoom;
        float topEdge = roomCenterY + halfRoom;
        float bottomEdge = roomCenterY - halfRoom;

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

        // Dash logic
        checkDash(ref velocity);

        _rb.linearVelocity = velocity;

        staminaCalc();
    }

    private void checkDash(ref Vector2 velocity)
    {
        if (Input.GetKeyDown("v") && stamina >= 1)
        {
            stamina--;
            dashDir = _movement.normalized;
            dashing = true;
            dashTimer = 0;
        }

        if (dashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer < dashDuration * (1 - dashTransitionRatio))
            {
                velocity = dashDir * dashSpeed;
            }
            else if (dashTimer < dashDuration)
            {
                velocity = dashDir * dashSpeed * dashTransitionRatio;
            }
            else
            {
                dashing = false;
            }
        }
    }

    private void staminaCalc()
    {
        if(stamina < maxStamina)
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }
        if(stamina > maxStamina)
        {
            stamina = (int) maxStamina;
        }
    }

}
