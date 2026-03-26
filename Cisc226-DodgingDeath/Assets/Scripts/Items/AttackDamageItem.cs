using UnityEngine;

public class AttackDamageItem : MonoBehaviour, Item
{
    [SerializeField] private int damageMult = 2;
    public Sprite icon
    {
        get
        {
            return icon;
        }
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
