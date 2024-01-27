using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth_rotation : MonoBehaviour
{
    public bool is_menu;
    private GameObject obj;
    public int rot = 0;
    private void Start()
    {
        obj = gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        obj.transform.Rotate(0, rot * Time.deltaTime, 0);
    }
}
