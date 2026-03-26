using UnityEngine;

public class StaminaItem : MonoBehaviour, Item
{
    [SerializeField] private int StaminaRegenMult = 2;
    public Sprite icon
    {
        get
        {
            return icon;
        }
    }

    public void onPickup()
    {
        GameController.staminaRegenRate += StaminaRegenMult;
    }
    public void delete()
    {
        Destroy(gameObject);
    }
}
