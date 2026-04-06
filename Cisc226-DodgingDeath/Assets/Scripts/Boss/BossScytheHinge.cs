using Unity.VisualScripting;
using UnityEngine;

public class BossScytheHinge : MonoBehaviour
{
    [SerializeField]  private float rotSpeed = -60f;
    public float angle = 0;


    void Update()
    {
        angle += rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
