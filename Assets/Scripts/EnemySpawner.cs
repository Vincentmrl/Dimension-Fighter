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

        // Spawn until the player dies
        while(player2dExists.scene.IsValid() && player3dExists.scene.IsValid())
        {
        yield return new WaitForSeconds(enemySpawnSpeed);

        Instantiate(enemyObject);
        };
    }

}
