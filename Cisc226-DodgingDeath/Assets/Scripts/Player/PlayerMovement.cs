using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
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
    


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        mapGenerator = FindObjectOfType<MapGenerator>();
    }

    void Start()
    {
        stamina = (int) maxStamina;
    }

    private void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);

        Vector2 position = transform.position;
        Vector2 newVelocity = _movement * _moveSpeed;

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
        if (position.x > rightEdge - 0.2f)
        {
            if (!mapGenerator.RoomExists(currentIndex + 1))
                newVelocity.x = Mathf.Min(0, newVelocity.x);
        }

        // LEFT EDGE
        if (position.x < leftEdge + 0.2f)
        {
            if (!mapGenerator.RoomExists(currentIndex - 1))
                newVelocity.x = Mathf.Max(0, newVelocity.x);
        }

        // TOP EDGE
        if (position.y > topEdge - 0.2f)
        {
            if (!mapGenerator.RoomExists(currentIndex - 10))
                newVelocity.y = Mathf.Min(0, newVelocity.y);
        }

        // BOTTOM EDGE
        if (position.y < bottomEdge + 0.2f)
        {
            if (!mapGenerator.RoomExists(currentIndex + 10))
                newVelocity.y = Mathf.Max(0, newVelocity.y);
        }

        _rb.linearVelocity = newVelocity;

        if(!dashing)
        {
            _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
            _rb.linearVelocity = _movement * _moveSpeed;
        }
        staminaCalc();
        checkDash();
    }

    private void checkDash()
    {
        if(Input.GetKeyDown("v") && stamina > 1)
        {
            stamina--;
            dashDir = _movement;
            dashing = true;
            dashTimer = 0;
        }
        if(dashing)
        {
            dashTimer += Time.deltaTime;
            if(dashTimer < dashDuration * (1-dashTransitionRatio))
            {
                _rb.linearVelocity = dashDir * dashSpeed;
            } else if (dashTimer < dashDuration)
            {
                _rb.linearVelocity = dashDir * dashSpeed * dashTransitionRatio;
            } else
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
