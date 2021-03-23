using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update

    public int score;
    public TextMeshProUGUI scoreUI;
    public GameObject gameOverScreen;
    void Start()
    {

        score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        scoreUI.text = "Score: " + score.ToString();

    }


    public void IncreaseScore()
    {

        score = score + 100;

    } 

    public void GameOver()
    {
        // Enable the game over screen on death
        gameOverScreen.SetActive(true);

    }
}
