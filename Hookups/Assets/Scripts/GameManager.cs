using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.text = "Player Alfa Wins!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(bool x)
    {
        gameOver.SetActive(true);
        if (x == true)
        {
            textMeshProUGUI.text = "Blue Player Wins!";
        }
        else
        {
            textMeshProUGUI.text = "Red Player Wins!";
        }
    }

    public void ResetLevel()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FarmScene");
    }
}
