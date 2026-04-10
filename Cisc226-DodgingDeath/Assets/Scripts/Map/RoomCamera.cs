using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField] private float roomHeight = 12f;
    [SerializeField] private float doorSize = 2.5f; // width of doorway
    private float roomWidth;

    public Transform player;
    private int currentRoomX = 0;
    private int currentRoomY = 0;
    private bool isTransitioning = false;

    void Start()
    {
        Camera.main.orthographicSize = roomHeight / 2f;

        roomWidth = roomHeight * (16f / 9f);

        SetAspectRatio();

        Invoke(nameof(InitializeCamera), 0.05f);
    }

    void InitializeCamera()
    {
        Vector3 pos = player.position;

        currentRoomX = Mathf.RoundToInt(pos.x / roomWidth);
        currentRoomY = Mathf.RoundToInt(pos.y / roomHeight);

        transform.position = new Vector3(
            currentRoomX * roomWidth,
            currentRoomY * roomHeight,
            -10
        );
    }

    public void MoveRoom(Vector2 direction)
    {
        if (isTransitioning) return;

        isTransitioning = true;

        currentRoomX += Mathf.RoundToInt(direction.x);
        currentRoomY += Mathf.RoundToInt(direction.y);

        // Move camera
        Vector3 newCamPos = new Vector3(
            currentRoomX * roomWidth,
            currentRoomY * roomHeight,
            -10
        );

        transform.position = newCamPos;

        float offset = 1.5f; // how far inside the room they appear

        Vector3 playerPos = player.position;

        if (direction == Vector2.right)
            playerPos.x = newCamPos.x - (roomWidth / 2f) + offset;

        else if (direction == Vector2.left)
            playerPos.x = newCamPos.x + (roomWidth / 2f) - offset;

        else if (direction == Vector2.up)
            playerPos.y = newCamPos.y - (roomHeight / 2f) + offset;

        else if (direction == Vector2.down)
            playerPos.y = newCamPos.y + (roomHeight / 2f) - offset;

        player.position = playerPos;

        Invoke(nameof(ResetTransition), 0.2f);
    }
    void ResetTransition()
    {
        isTransitioning = false;
    }

    void SetAspectRatio()
    {
        Camera cam = Camera.main;

        float targetAspect = 16f / 9f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Rect rect = cam.rect;

        if (scaleHeight < 1.0f)
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        cam.rect = rect;
    }
}