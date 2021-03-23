using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2dDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "EnemyBullet2D")
        {

            Debug.Log("Player2D hit");
            // Look for the player controller and execute the PlayerDeath function to destroy both players
            // and GameOver to load the death menu
            GameObject.Find("PlayerController").GetComponent<PlayerMovement>().PlayerDeath();
            GameObject.Find("PlayerController").GetComponent<PlayerScore>().GameOver();
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().PlayerIsDead();


            // Destroy the bullet 
            Destroy(other.gameObject);
        }

    }
}
