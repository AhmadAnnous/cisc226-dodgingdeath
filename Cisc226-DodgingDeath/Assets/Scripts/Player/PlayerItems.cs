using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public ArrayList items = new ArrayList();
    public GameObject InventoryContent;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            getItem(other.GetComponent<Item>().thisItem, other);
            other.GetComponent<Item>().delete();
        }
    }

    private void getItem(int item, Collider2D other)
    {
        switch(item)
        {
            //Attack Speed
            case 0:
                if(!Item.itemsObtained[other.GetComponent<Item>().thisItem])
                
                    Item.itemsObtained[other.GetComponent<Item>().thisItem] = true;
                    GameController.attackSpeed *=  0.5f;
                    Debug.Log(GameController.attackSpeed);
                
                break;
            //Damage
            case 1:
                if(!Item.itemsObtained[other.GetComponent<Item>().thisItem])
                
                    Item.itemsObtained[other.GetComponent<Item>().thisItem] = true;
                    GameController.playerDamage *=  2;
                    Debug.Log(GameController.playerDamage);
                
                break;
            //Player Speed
            case 2:
                if(!Item.itemsObtained[other.GetComponent<Item>().thisItem])
                
                    Item.itemsObtained[other.GetComponent<Item>().thisItem] = true;
                    PlayerMovement.moveSpeed *= 2;
                    Debug.Log(PlayerMovement.moveSpeed);
                
                break;
        }
    }
    
}



