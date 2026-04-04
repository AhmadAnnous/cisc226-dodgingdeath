using UnityEngine;

public class AbilityCDItem : MonoBehaviour, Item
{
    [SerializeField] private float abilityRegenMult = 3f;
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
        GameController.abilityRegenRate *= abilityRegenMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
}
