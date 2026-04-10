using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Sprite doorSprite;
    public int index;
    public int value;
    public SpriteRenderer spriteRenderer;
    public void SetSpecialRoomSprite(Sprite icon)
    {
        spriteRenderer.sprite = icon;
    }

    public void SpawnBoss(GameObject bossprefab, Vector3 location)
    {
        GameObject boss = Instantiate(bossprefab, location, Quaternion.Euler(0,0,0));
        GameObject.FindGameObjectWithTag("BulletSpawner").GetComponent<BossBulletSpawner>().setLoc(location);
    }

    public void SpawnItem(GameObject item, Vector3 location)
    {
        GameObject bullet = Instantiate(item, location, Quaternion.Euler(0,0,0));
    }

    GameObject CreateDoor(Vector2 offset, float rotationZ)
    {
        float roomHeight = 12f;
        float roomWidth = roomHeight * (16f / 9f);

        GameObject door = new GameObject("Door");

        door.transform.position = (Vector2)transform.position + offset;
        door.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        door.transform.parent = transform;

        SpriteRenderer sr = door.AddComponent<SpriteRenderer>();
        sr.sprite = doorSprite;
        sr.sortingOrder = 5;

        Vector2 spriteSize = sr.sprite.bounds.size;

        float targetWidth = 1f;
        float targetHeight = 0.5f;

        door.transform.localScale = new Vector3(
            targetWidth / spriteSize.x,
            targetHeight / spriteSize.y,
            1f
        );

        // Collider
        BoxCollider2D col = door.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(targetWidth, targetHeight);

        // Door script
        door.AddComponent<Door>();

        return door;
    }

    public void CreateWalls()
    {
        float roomHeight = 12f;
        float roomWidth = roomHeight * (16f / 9f);

        float halfWidth = roomWidth / 2f;
        float halfHeight = roomHeight / 2f;

        float inset = 0.3f; // distance from visual edge

        // LEFT WALL
        CreateWall(new Vector2(-halfWidth + inset, 0),
                new Vector2(0.5f, roomHeight));

        // RIGHT WALL
        CreateWall(new Vector2(halfWidth - inset, 0),
                new Vector2(0.5f, roomHeight));

        // TOP WALL
        CreateWall(new Vector2(0, halfHeight - inset),
                new Vector2(roomWidth, 0.5f));

        // BOTTOM WALL
        CreateWall(new Vector2(0, -halfHeight + inset),
                new Vector2(roomWidth, 0.5f));
    }
    void CreateWall(Vector2 localPos, Vector2 size)
    {
        GameObject wall = new GameObject("Wall");

        wall.transform.SetParent(transform, false);
        wall.transform.localPosition = localPos;

        BoxCollider2D col = wall.AddComponent<BoxCollider2D>();
        col.size = size;
        col.isTrigger = false;
    }
    public void SetupDoors(MapGenerator map)
    {
        float roomHeight = 12f;
        float roomWidth = roomHeight * (16f / 9f);

        float halfWidth = roomWidth / 2f;
        float halfHeight = roomHeight / 2f;

        float inset = 0.2f;

        // RIGHT
        if (map.RoomExists(index + 1))
        {
            GameObject d = CreateDoor(new Vector2(halfWidth - inset, 0), -90f);
            d.GetComponent<Door>().direction = Vector2.right;
        }

        // LEFT
        if (map.RoomExists(index - 1))
        {
            GameObject d = CreateDoor(new Vector2(-halfWidth + inset, 0), 90f);
            d.GetComponent<Door>().direction = Vector2.left;
        }

        // TOP
        if (map.RoomExists(index + 10))
        {
            GameObject d = CreateDoor(new Vector2(0, halfHeight - inset), 0f);
            d.GetComponent<Door>().direction = Vector2.up;
        }

        // BOTTOM
        if (map.RoomExists(index - 10))
        {
            GameObject d = CreateDoor(new Vector2(0, -halfHeight + inset), 180f);
            d.GetComponent<Door>().direction = Vector2.down;
        }
    }
}
