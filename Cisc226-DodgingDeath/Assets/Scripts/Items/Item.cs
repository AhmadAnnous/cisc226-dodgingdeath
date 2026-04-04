using UnityEngine;

interface Item
{
    void onPickup();
    void delete();
    Sprite icon { get; }
}
