using UnityEngine;

public class DashSpeedItem : MonoBehaviour, Item
{
    [SerializeField] private float dashSpeedMult = 2f;
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
        GameController.dashSpeed *= dashSpeedMult;
        GameController.dashDuration /= dashSpeedMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
