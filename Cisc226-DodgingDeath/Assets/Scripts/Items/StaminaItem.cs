using UnityEngine;

public class StaminaItem : MonoBehaviour, Item
{
    [SerializeField] private int StaminaRegenMult = 2;
    private SpriteRenderer sr;
    public Sprite icon
    {
        get
        {
            return sr.sprite;
        }
    }

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void onPickup()
    {
        GameController.staminaRegenRate += StaminaRegenMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
}
