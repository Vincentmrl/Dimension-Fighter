using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuControl : MonoBehaviour
{
   
    public void PlayGame()
    {
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        // Self explanatory, quits the game
        Debug.Log("Quitting Game");
        Application.Quit();

    }

    public void LoadMenu()
    {
        // Loads the main menu
        SceneManager.LoadScene("Main Menu");

    }

    public void ReloadLevel()
    {
        // Reloads the level
        SceneManager.LoadScene("GameLevel");
        
    }



}
