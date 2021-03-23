using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3DController : MonoBehaviour
{
    // Check collision on 3D enemy
    private void OnTriggerEnter(Collider other){

        if(other.tag == "PlayerBullet3D")
        {

            
            // Look for the player controller and execute the IncreaseScore function to increase the points
            GameObject.Find("PlayerController").GetComponent<PlayerScore>().IncreaseScore();

            Debug.Log("Score increased");

            // Destroy the bullet and the parent object (which will also destroy the child)
            Destroy(other.gameObject);
            Destroy(transform.parent.gameObject);

        }

    }
}
