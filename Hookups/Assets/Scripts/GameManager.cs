using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    public Button again;
    // Start is called before the first frame update
    [SerializeField] GameObject gameOver;

    [SerializeField]
    DamageableObject redPlayer,bluePlayer;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (redPlayer.healthPoints <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            CheckButton();
            GameOver(false);
        }

        if (bluePlayer.healthPoints <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            CheckButton();
            GameOver(true);
        }
    }

    void CheckButton()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            ResetLevel();
        }


        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            MainMenuController.returnToMenu();
        }
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
