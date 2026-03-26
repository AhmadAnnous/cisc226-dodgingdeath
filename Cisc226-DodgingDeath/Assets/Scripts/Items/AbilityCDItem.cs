using UnityEngine;

public class AbilityCDItem : MonoBehaviour, Item
{
    [SerializeField] private float abilityRegenMult = 3f;
    public Sprite icon
    {
        get
        {
            return icon;
        }
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
