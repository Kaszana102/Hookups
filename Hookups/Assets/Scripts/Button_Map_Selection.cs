using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Button_Map_Selection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject firstMenu = GameObject.Find("FirstMenu");
    public GameObject mapSelection = GameObject.Find("MapSelection");

    private void Start()
    {
        firstMenu.SetActive(!firstMenu.activeInHierarchy);
        mapSelection.SetActive(!mapSelection.activeInHierarchy);
    }

}
