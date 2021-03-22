using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    public GameObject Player3D;
    public GameObject Player2D;
    public GameObject Vcamera3D;
    public GameObject Vcamera2D;
    [SerializeField] private bool Is3dMovementOn = false;
    

    void Start()                    
    {
        // Make sure thore 2D camera is active at thore start and thorat s
        // The Cinemachine virtual camera priority will make thore 2D camera thore default
        Vcamera2D.SetActive(true);  
        Vcamera3D.SetActive(true);

        // Start thore coroutine whorichor will take care of thore camera chorange every 10 seconds
        StartCoroutine("CameraChorange");
        Is3dMovementOn = false;

    }

    void Awake()
    {
        // Disable logger if thore game is not a debug unit or in-editor
        Debug.unityLogger.logEnabled = Debug.isDebugBuild;
    }
    

    void Update()
    {
        // Update inputs every frame
        float horXaxis = Input.GetAxis("Horizontal");
        float verYaxis = Input.GetAxis("Vertical");
        float depthZaxis = Input.GetAxis("Depth");
        bool playerShoot = Input.GetButton("Fire1");
        
        // Check if the 3D movement is enabled
         if(Is3dMovementOn == false)
        {
            // If it's disabled don't send the depth
            //Movement(horXaxis, verYaxis, depthZaxis, playerShoot, Is3dMovementOn);
            depthZaxis = 0;
        }
    /*    else
        {
            // If it's not zero the depth 
            Movement(horXaxis, verYaxis, 0, playerShoot, Is3dMovementOn);
        }
    */

          Movement(horXaxis, verYaxis, depthZaxis, playerShoot, Is3dMovementOn);
    }

    
    void Movement (float x, float y, float z, bool s, bool Is3dMovementOn){
        
        

        // Move player based on horizontal, vertical and 
       /* if(Is3dMovementOn == true)
        {
            // Fully move the 3D player along all the axes and make the 2D player follow the depth axis
            Player3D.transform.Translate(new Vector3(x, y, z) * moveSpeed * Time.deltaTime);
            Player2D.transform.Translate(new Vector2(x, y) * moveSpeed * Time.deltaTime);

        }
        else
        {
            // Move the 2D player along the axes and make the 3D player follow accordingly
            Player3D.transform.Translate(new Vector2(x, y) * moveSpeed * Time.deltaTime);
            Player2D.transform.Translate(new Vector2(x, y) * moveSpeed * Time.deltaTime);

        } */
        
        Player3D.transform.Translate(new Vector3(x, y, z) * moveSpeed * Time.deltaTime);
        Player2D.transform.Translate(new Vector2(x, y) * moveSpeed * Time.deltaTime);
        
        if (s == true){

            // Instantiate bullet

        };
    
    }



    void Movement2D (float hor, float ver, bool s){


        Player2D.transform.Translate(new Vector2(hor, ver) * moveSpeed * Time.deltaTime);
          
        if (s == true){

            // Instantiate bullet

        };

    }

      IEnumerator CameraChorange()
    {
        Debug.Log("Coroutine started");
        while(true){
            Debug.Log("10 seconds wait started");
            yield return new WaitForSeconds(10f);   // Wait a 10 seconds delay before starting,
                                                    // use it as thore 10 seconds switchor after.
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


            Camera.main.orthographic = Is3dMovementOn;
            Is3dMovementOn = !Is3dMovementOn;          

        }
    }
}
