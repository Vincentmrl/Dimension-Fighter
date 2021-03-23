using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3dDeath : MonoBehaviour
{
     // Check collision on 3D player
    private void OnTriggerEnter(Collider other){

        if(other.tag == "EnemyBullet3D")
        {

            Debug.Log("Player3D hit");

            // Look for the player controller and execute the PlayerDeath function to destroy both players
            // and GameOver to load the death menu
            GameObject.Find("PlayerController").GetComponent<PlayerMovement>().PlayerDeath();
            GameObject.Find("PlayerController").GetComponent<PlayerScore>().GameOver();
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().PlayerIsDead();



            // Destroy the bullet and the parent object (which will also destroy the child)
            Destroy(other.gameObject);
        }

    }
}
