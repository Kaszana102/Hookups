using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testbutton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Move) ;
    }
    void Move()
    {
       MainMenuController.returnToMenu();   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
