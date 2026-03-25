using UnityEngine;

public class AttackSpeedItem : MonoBehaviour, Item
{
    [SerializeField] private float attackSpeedMult = 0.5f;
    public Sprite icon
    {
        get
        {
            return icon;
        }
    }

    public void onPickup()
    {
        GameController.attackSpeed *= attackSpeedMult;
        Debug.Log(GameController.attackSpeed);
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
