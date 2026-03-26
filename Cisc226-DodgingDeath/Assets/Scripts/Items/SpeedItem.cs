using UnityEngine;

public class SpeedItem : MonoBehaviour, Item
{
    [SerializeField] private float speedMult = 2f;
    public Sprite icon
    {
        get
        {
            return icon;
        }
    }

    public void onPickup()
    {
        GameController.moveSpeed *= speedMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
