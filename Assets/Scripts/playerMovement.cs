using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public GameObject player3D;
    public GameObject player2D;
    public GameObject vCamera3D;
    public GameObject vCamera2D;
    private bool playerShoot;  // This variable exists only to avoid bugs auto firing at the first frame

    [SerializeField] private bool is3dMovementOn = false;       // Check if player is in 3D mode
    [SerializeField] private bool playerAlive = true;            // Check if the player is alive
    [SerializeField] private GameObject playerBullet2D;         // Bullets
    [SerializeField] private GameObject playerBullet3D;        
    [SerializeField] float moveSpeed = 1;                       // Player movement speed

    [SerializeField] private float fireRate = 0.5f;             // How much time to wait until you can shoot again?
    [SerializeField] private bool canFire = true;               // Flag that gets enabled when enough time has passed to fire again
    [SerializeField] private float bulletX;     // Bullet spawn coordinates in order to instantiate them in front of the ship
    [SerializeField] private float bulletY;
    [SerializeField] private float bulletZ;


    void Start()                    
    {
        // Make sure the 2D camera is active at the start
        // The Cinemachine virtual camera priority will make the 2D camera the default
        vCamera2D.SetActive(true);  
        vCamera3D.SetActive(true);

        // Start the coroutine which will take care of the camera change every 10 seconds
        StartCoroutine("CameraChange");
        is3dMovementOn = false;
        playerShoot = false;
        canFire = true;
        playerAlive = true;

        // Always spawn the player in the same position
        player2D.transform.position = new Vector2(0f, -4.1f);
        player3D.transform.position = new Vector3(0f, -4.1f, 12f);
    }

    void Awake()
    {
        // Disable logger if the game is not a debug unit or running in-editor
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
    }
    

    void Update()
    {
        // Update inputs every frame
        float horXaxis = Input.GetAxis("Horizontal");
        float verYaxis = Input.GetAxis("Vertical");
        float depthZaxis = Input.GetAxis("Depth");
        playerShoot = Input.GetButton("Shoot");
        
        // Check if the 3D movement is enabled
         if(is3dMovementOn == false)
        {
            // If it's disabled don't send the depth
            depthZaxis = 0;
        }

        // The actual movement happens here
        Movement(horXaxis, verYaxis, depthZaxis);

        // Check if there's an input, if you can shoot and if the player is alive
        if(playerShoot == true && canFire == true && playerAlive == true)
        {
            ShootBullet();
        };

    }

    // Movement mechanic
    void Movement (float x, float y, float z)
    {
        if(playerAlive == true) // Check if the player is alive or don't send movement inputs
        {

        
        // Only move the 3D player
        player3D.transform.Translate(new Vector3(x, y, z) * moveSpeed * Time.deltaTime);


        // Make sure the player is within the gameplay range
        player3D.transform.position = new Vector3(Mathf.Clamp(player3D.transform.position.x, -14f, 14f), Mathf.Clamp(player3D.transform.position.y, -7.6f, 7.6f), Mathf.Clamp(player3D.transform.position.z, 8f, 16f));      
        // player2D takes the same X and Y coordinates as player3D 
        player2D.transform.position = new Vector2(player3D.transform.position.x, player3D.transform.position.y);
        }
    }

    // Shooting mechanic
    void ShootBullet()
    {
        // Check if the player is in 3D or 2D mode and instantiate just the 3d bullet or both
        if(is3dMovementOn == true)
        {

            Instantiate(playerBullet3D, player3D.transform.position + new Vector3(bulletX, bulletY, bulletZ), Quaternion.identity);

        }
        else
        {
            Instantiate(playerBullet2D, player2D.transform.position + new Vector3(bulletX, bulletY, 0), Quaternion.identity);
            Instantiate(playerBullet3D, player3D.transform.position + new Vector3(bulletX, bulletY, bulletZ), Quaternion.identity);

        }
        canFire = false;

        // Start the timer
        StartCoroutine("FireRatio");
    }

    // Death mechanic
    public void PlayerDeath()
    {
        // Stops coroutines
        StopAllCoroutines();        
        // Tell the update to not send inputs when the player dies and then destroy the GameObjects
        playerAlive = false;
        Destroy(player3D);
        Destroy(player2D);

    }

  

    // Coroutine to change camera position and perspective/ortographic modes
    IEnumerator CameraChange()
    {
        Debug.Log("Coroutine started");
        while(true){
            Debug.Log("10 seconds wait started");
            yield return new WaitForSeconds(10f);   // Wait a 10 seconds delay before starting,
                                                    // use it as the 10 seconds switch after.
            if (is3dMovementOn == false)            // If controls are already 2D enable 3D
            {
                vCamera2D.SetActive(false);
                Debug.Log("Camera 3D enabled");

                // Disable the 2D hitbox and enable the 3D one.
                player2D.GetComponent<CircleCollider2D>().enabled = false;
                player3D.GetComponent<SphereCollider>().enabled = true;

            }
            else 
            {
                vCamera2D.SetActive(true);
                Debug.Log("Camera 2D enabled");
                // Disable the 3D hitbox and enable the 2D one.
                player2D.GetComponent<CircleCollider2D>().enabled = true;
                player3D.GetComponent<SphereCollider>().enabled = false;
            }

            // Switch camera between ortographic/perspective and swap the variable right after
            Camera.main.orthographic = is3dMovementOn;
            is3dMovementOn = !is3dMovementOn;          

        };


        


    }
    
    // Coroutine to count the amount of seconds to wait before shooting
    IEnumerator FireRatio()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;


    }
    

}
