using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    [SerializeField] GameObject rotSrc;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = rotSrc.transform.rotation;
    }
}
