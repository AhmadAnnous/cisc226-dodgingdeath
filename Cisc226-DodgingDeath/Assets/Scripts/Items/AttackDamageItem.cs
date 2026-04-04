using UnityEngine;

public class AttackDamageItem : MonoBehaviour, Item
{
    [SerializeField] private int damageMult = 2;
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
        GameController.playerDamage *= damageMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
