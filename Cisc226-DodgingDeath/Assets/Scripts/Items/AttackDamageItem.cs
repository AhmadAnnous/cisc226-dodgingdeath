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
        Debug.Log(GameController.playerDamage);
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
