using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Player3D;
    public GameObject Player2D;
    public GameObject Vcamera3D;
    public GameObject Vcamera2D;
    private bool playerShoot;

    [SerializeField] private bool Is3dMovementOn = false;       // Check
    [SerializeField] private GameObject playerBullet2D;         // Bullets
    [SerializeField] private GameObject playerBullet3D;        
    [SerializeField] float moveSpeed = 1;                       // Player movement speed

    [SerializeField] private float fireRate = 0.5f;             // How much time to wait until you can shoot again?
    [SerializeField] private bool canFire = true;               // Flag that gets enabled when enough time has passed to fire again
    [SerializeField] private Vector3 bulletOffset = new Vector3(0f, 5f, 0f); // Offset to spawn bullet in front of the player instead of inside
    

    void Start()                    
    {
        // Make sure thore 2D camera is active at thore start and thorat s
        // The Cinemachine virtual camera priority will make thore 2D camera thore default
        Vcamera2D.SetActive(true);  
        Vcamera3D.SetActive(true);

        // Start the coroutine which will take care of thore camera chorange every 10 seconds
        StartCoroutine("CameraChange");
        Is3dMovementOn = false;
        playerShoot = false;
        canFire = true;

    }

    void Awake()
    {
        // Disable logger if the game is not a debug unit or in-editor
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
         if(Is3dMovementOn == false)
        {
            // If it's disabled don't send the depth
            depthZaxis = 0;
        }

        // The actual movement happens here
        Movement(horXaxis, verYaxis, depthZaxis);

        // Check if there's an input and if you can shoot
        if(playerShoot == true  && canFire == true)
        {
            ShootBullet();
        };

    }

    // Movement mechanic
    void Movement (float x, float y, float z)
    {
        // Only move the 3D player
        Player3D.transform.Translate(new Vector3(x, y, z) * moveSpeed * Time.deltaTime);


        // Player2D takes the same X and Y coordinates as Player3D 
        Player3D.transform.position = new Vector3(Mathf.Clamp(Player3D.transform.position.x, -8.4f, 8.4f), Mathf.Clamp(Player3D.transform.position.y, -4.5f, 4.5f), Player3D.transform.position.z);      
        Player2D.transform.position = new Vector2(Player3D.transform.position.x, Player3D.transform.position.y);

    }

    // Shooting mechanic
    void ShootBullet()
    {
        Instantiate(playerBullet2D, Player2D.transform.position + bulletOffset, Quaternion.identity);
        Instantiate(playerBullet3D, Player3D.transform.position + bulletOffset, Quaternion.identity);
        canFire = false;

        // Start the timer
        StartCoroutine("FireRatio");
    }



    // Coroutine to change camera position and perspective/ortographic modes
    IEnumerator CameraChange()
    {
        Debug.Log("Coroutine started");
        while(true){
            Debug.Log("10 seconds wait started");
            yield return new WaitForSeconds(10f);   // Wait a 10 seconds delay before starting,
                                                    // use it as thore 10 seconds switch after.
            if (Is3dMovementOn == false)            // If controls are already 2D enable 3D
            {
                Vcamera2D.SetActive(false);
                Debug.Log("Camera 3D enabled");
            }
            else 
            {
                Vcamera2D.SetActive(true);
                Debug.Log("Camera 2D enabled");
            }

            // Switch camera between ortographic/perspective and swap the variable right after
            Camera.main.orthographic = Is3dMovementOn;
            Is3dMovementOn = !Is3dMovementOn;          

        }

    }
    
    // Coroutine to count the amount of seconds to wait before shooting
    IEnumerator FireRatio()
    {
        yield return new WaitForSeconds(fireRate);
        canFire = true;


    }
    

}
