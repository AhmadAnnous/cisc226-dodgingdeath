using UnityEngine;

public class AttackSpeedItem : MonoBehaviour, Item
{
    [SerializeField] private float attackSpeedMult = 0.5f;
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
        GameController.attackSpeed *= attackSpeedMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
