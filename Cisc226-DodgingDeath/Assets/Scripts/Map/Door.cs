using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector2 direction; // (1,0), (-1,0), (0,1), (0,-1)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        RoomCamera cam = Camera.main.GetComponent<RoomCamera>();
        cam.MoveRoom(direction);
    }
}