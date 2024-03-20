using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class MainMenuController : MonoBehaviour
{
    public Button exit;
    public int scene = 2;
    static bool wasPlaying = false;
    public GameObject firstMenu, secondMenu, levelButtons;
    public GameObject cube1, cube2;
    // Start is called before the first frame update

    Color chosen = new Color(0.3843137f, 0.4862745f, 1f);
    Color nonchosen = Color.white;

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

    void SetChosen(int index, Color color, Color textColor)
    {
        var forest = levelButtons.transform.GetChild(index-1).gameObject;
        var forImg = forest.GetComponent<Image>();
        forImg.color = color;
        forImg.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = textColor;
        
    }

    public void setForest()
    {
        SetChosen(scene, nonchosen,Color.black);
        scene = 2;
        SetChosen(scene, chosen, Color.white);
        Debug.Log("Map set to forrest");

    }

    public void setShipyard()
    {
        SetChosen(scene, nonchosen, Color.black);
        scene = 3;
        SetChosen(scene, chosen, Color.white);
        Debug.Log("Map set to shipyard");
    }

    public void loadScene()
    {
        AsyncOperation asyncLoad;
        switch (scene)
        {
            case 2:
                asyncLoad = SceneManager.LoadSceneAsync("FarmScene");
                break;
            case 3:
                asyncLoad = SceneManager.LoadSceneAsync("Shipyard");
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
