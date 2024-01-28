using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button exit;
    public int scene = 2;
    static bool wasPlaying = false;
    public GameObject firstMenu, secondMenu;
    public GameObject cube1, cube2;
    // Start is called before the first frame update

    void Start()
    {
        exit.onClick.AddListener(AppQuit);
        if(wasPlaying == true)
        {
            firstMenu.SetActive(false);
            secondMenu.SetActive(true);
            cube1.SetActive(false);
            cube2.SetActive(true);
        }
    }
    public void AppQuit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setForest()
    {
        scene = 2;
        Debug.Log("Map set to forrest");
    }
    
    public void loadScene()
    {
        switch (scene)
        {
            case 2:
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FarmScene");
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
    public static void returnToMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");
        wasPlaying = true;
    }
}
