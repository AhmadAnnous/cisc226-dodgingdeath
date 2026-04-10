using System;
using System.Collections;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public ArrayList items = new ArrayList();
    public Inventory inventory;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {

            Item item = other.GetComponent<Item>();

            inventory.AddItemIcon(item.icon);
            getItem(item);

            
        }
    }

    private void getItem(Item item)
    {
        item.onPickup();
        item.delete();
    }
    
}



