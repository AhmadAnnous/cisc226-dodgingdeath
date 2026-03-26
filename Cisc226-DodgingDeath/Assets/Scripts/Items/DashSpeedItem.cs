using UnityEngine;

public class DashSpeedItem : MonoBehaviour, Item
{
    [SerializeField] private float dashSpeedMult = 2f;
    public Sprite icon
    {
        get
        {
            return icon;
        }
    }

    public void onPickup()
    {
        GameController.dashSpeed *= dashSpeedMult;
        GameController.dashDuration /= dashSpeedMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
