using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    private bool open = false;
    public Transform inconparent;
    public GameObject iconprefab;
    private List<Sprite> currenticons = new List<Sprite>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !open)
        {
            transform.Translate(-400,0,0);
            open = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && open)
        {
            transform.Translate(400,0,0);
            open = false;
        }
    }

    public void AddItemIcon(Sprite icon)
    {
        GameObject iconObj = Instantiate(iconprefab, inconparent);
        Image img =  iconObj.GetComponent<Image>();
        img.sprite = icon;

        currenticons.Add(icon);
    }
}
