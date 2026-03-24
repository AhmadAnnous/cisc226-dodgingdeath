using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool open = false;

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
}
