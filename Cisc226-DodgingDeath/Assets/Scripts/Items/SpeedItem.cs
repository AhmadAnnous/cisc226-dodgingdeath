using UnityEngine;
using UnityEngine.Rendering;

public class SpeedItem : MonoBehaviour, Item
{
    [SerializeField] private float speedMult = 2f;
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
        GameController.moveSpeed *= speedMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
    
}
