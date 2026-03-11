using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] double health;
    [SerializeField] double healthDrainPerSec;
    [SerializeField] double maxHealth;
    [SerializeField] double startingHealth;
    
    //for intro and whatnot, mainly for later use
    bool healthDrainable = true;

    void Start()
    {
        health = startingHealth;
    }

    void Update()
    {
        if(healthDrainable)
        {
            health -= (Time.deltaTime * healthDrainPerSec);
        }
    }

    public void addHealth(double healthGain)
    {
        health += healthGain;
    }
}
