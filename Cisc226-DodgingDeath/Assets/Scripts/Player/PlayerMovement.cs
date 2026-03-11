using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;

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

    }

    void Start()
    {
        stamina = (int) maxStamina;
    }

    private void Update()
    {
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
