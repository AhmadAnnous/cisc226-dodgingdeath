using System;
using UnityEngine;

public class Cell : MonoBehaviour
{
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
}
