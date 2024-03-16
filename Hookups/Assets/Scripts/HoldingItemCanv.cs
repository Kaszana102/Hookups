using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldingItem : MonoBehaviour
{
    public Sprite throwObject;
    public Sprite grabObject;
    public Image itemIcon;
    public GameObject itemIconPrefab;
    private bool itemGrabbed = false; 
    public float scaleMax = 1f;
    public float scaleMin = 0.5f;
    private float scale = 1.0f;
    public float scaleSpeed = 0.005f;
    private bool scaleChange = false;
    // Start is called before the first frame update
    public void grabItem()
    {
        itemIcon.sprite = throwObject;
        itemGrabbed = true;
    }

    public void throwItem()
    {
        itemIcon.sprite = grabObject;
        itemGrabbed = false;
        itemIconPrefab.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        scale = 1.0f;
    }

    private void Update()
    {
        if (itemGrabbed)
        {
            if (!scaleChange) {
                scale -= scaleSpeed;
                if (scale <= scaleMin)
                    scaleChange = true;
            }
            else
            {
                scale += scaleSpeed;
                if (scale >= scaleMax)
                    scaleChange = false;

            }
            itemIconPrefab.transform.localScale = new Vector3(scale, scale, 1.0f);
        }
    }
    void Replace(GameObject item1, GameObject item2)
    {
        Instantiate(item2, item1.transform.position, Quaternion.identity);
        Destroy(item1);
    }
}
