using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject enemyBullet3D;
    public GameObject enemyBullet2D;

    [SerializeField] float enemyMinFireRate = 2f; // Set how many bullets per second enemies can fire,
    [SerializeField] float enemyMaxFireRate = 5f; // choose a random value between these on spawn
    [SerializeField] float enemyMovementSpeed = 1; // Choose a random enemy speed at the start

    [SerializeField] float currentFireRate; // Chosen firerate at start
    [SerializeField] private float enemybulletX; // Enemy bullet offset
    [SerializeField] private float enemybulletY;
    [SerializeField] private float enemybulletZ;
    private Transform enemy3D;




    // Start is called before the first frame update
    void Start()
    {   
        // Obtain the child object which is the 2D version of the enemy
        enemy3D = this.gameObject.transform.GetChild(0);

        // Give a random spawn position, movement speed and fire rate to the enemy
        float enemyPosX = Random.Range(-8.4f, 8.4f);
        float EnemyPosY = Random.Range(0f, 4.5f); 
        enemyMovementSpeed = Random.Range(-10f, 10f);
        currentFireRate = Random.Range(enemyMinFireRate, enemyMaxFireRate);

        // Position the enemy on the spawn value
        transform.position = new Vector2(enemyPosX, EnemyPosY);
        enemy3D.position = new Vector3(enemyPosX, EnemyPosY, Random.Range(8f, 16f));

        // Start shooting
        StartCoroutine("EnemyShooting");

    }

    void Update()
    {
        // Move the enemy
        transform.Translate(new Vector3(enemyMovementSpeed, 0f, 0f) * Time.deltaTime);

        // Make sure the enemy doesn't go over the screen boundaries and teleport it to the other side
        if(transform.position.x < -8.5f)
        {
            transform.position = new Vector2(8.4f, transform.position.y);

        }

          if(transform.position.x > 8.5f)
        { 
            transform.position = new Vector2(-8.4f, transform.position.y);
        }

    }

    IEnumerator EnemyShooting()
    {
        // Make it always happen as long as the object is not destroyed
        while(true){


        yield return new WaitForSeconds(currentFireRate);

        // Spawn enemy bullets in enemy positions
        Instantiate(enemyBullet3D,  enemy3D.position + new Vector3(enemybulletX, enemybulletY, enemybulletZ), Quaternion.identity);
        Instantiate(enemyBullet2D,  transform.position + new Vector3(enemybulletX, enemybulletY, 0), Quaternion.identity);

        }

    }


    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);

        }

    }


}
