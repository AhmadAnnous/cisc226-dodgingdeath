using System;
using System.Collections;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public ArrayList items = new ArrayList();
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
                    PlayerAttack.attackDelay *=  0.5f;
                    Debug.Log(PlayerAttack.attackDelay *=  0.5f);
                break;
            //Damage
            case 1:
                if(!Item.itemsObtained[other.GetComponent<Item>().thisItem])
                    Item.itemsObtained[other.GetComponent<Item>().thisItem] = true;
                    PlayerAttack.damage *=  2;
                    Debug.Log(PlayerAttack.damage);
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



