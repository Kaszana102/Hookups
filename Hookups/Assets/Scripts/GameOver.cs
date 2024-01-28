using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOver;    

    List<DamageableObject> players= new List<DamageableObject>();

    private void Start()
    {
        gameOver.SetActive(false);
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player.GetComponent<DamageableObject>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(DamageableObject player in players)
        {
            if(player.healthPoints <= 0)
            {
                gameOver.SetActive(true);
            }
        }
    }
}
