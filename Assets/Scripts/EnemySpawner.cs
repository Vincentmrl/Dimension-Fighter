using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyObject;
    public GameObject player2dExists;
    public GameObject player3dExists;
    [SerializeField] private float enemySpawnSpeed = 2f;

    void Start()
    {
        StartCoroutine("SpawnEnemy");

    }



    IEnumerator SpawnEnemy()
    {

        // Spawn enemies...
        while(true)
        {
        yield return new WaitForSeconds(enemySpawnSpeed);

        Instantiate(enemyObject);

        };
    }
        //...until the player is dead

    public void PlayerIsDead()
    {

        StopAllCoroutines();

    }

}
