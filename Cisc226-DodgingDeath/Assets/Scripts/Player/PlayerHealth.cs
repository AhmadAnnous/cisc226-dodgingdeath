using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] double health;
    [SerializeField] double healthDrainPerSec;
    [SerializeField] double maxHealth;
    [SerializeField] double startingHealth;

    void Start()
    {
        health = startingHealth;
    }

    void Update()
    {
        health -= (Time.deltaTime * healthDrainPerSec);
    }
}
