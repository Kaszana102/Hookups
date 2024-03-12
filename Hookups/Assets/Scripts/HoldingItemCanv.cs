using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingItem : MonoBehaviour
{
    public GameObject holdingItem;
    public GameObject defaultItem;
    // Start is called before the first frame update
    public void grabItem(GameObject grabbedItem)
    {
        holdingItem = grabbedItem;
        Replace(defaultItem, holdingItem);
    }

    public void throwItem()
    {
        Replace(holdingItem, defaultItem);
    }

    void Replace(GameObject item1, GameObject item2)
    {
        Instantiate(item2, item1.transform.position, Quaternion.identity);
        Destroy(item1);
    }
}
