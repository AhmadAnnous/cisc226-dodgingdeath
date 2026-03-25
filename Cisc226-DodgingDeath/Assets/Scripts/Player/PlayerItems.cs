using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditorInternal.Profiling.Memory.Experimental;
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

            getItem(item);
            inventory.AddItemIcon(item.icon);

            
        }
    }

    private void getItem(Item item)
    {
        item.onPickup();
        item.delete();
    }
    
}



