using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public Transform player;
    public float roomSize = 12f;

    void LateUpdate()
    {
        int roomX = Mathf.RoundToInt(player.position.x / roomSize);
        int roomY = Mathf.RoundToInt(player.position.y / roomSize);

        Vector3 targetPosition = new Vector3(roomX * roomSize, roomY * roomSize, -10);

        transform.position = targetPosition;
    }
}
