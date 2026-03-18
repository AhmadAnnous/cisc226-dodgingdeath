using System;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public static String[] itemList = new string[3];
    public static bool[] itemsObtained = new bool[itemList.Length]; //for stopping duplicates
    [SerializeField] public int thisItem;


    //I stg I'm not this stupid, C# 9 literally won't let you initialize a list properly
    public void Start()
    {
        itemList[0] = "AttackSpeed";
        itemList[1] = "Damage";
        itemList[2] = "PlayerSpeed";

        for(int i = 0; i < itemList.Length; i++)
        {
            itemsObtained[i] = false;
        }
    }


    public void delete()
    {
        Destroy(gameObject);
    }
}
