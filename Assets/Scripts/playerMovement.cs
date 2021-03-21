using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] bool player3dMovement = true;
    public GameObject Player3D;
    public GameObject Player2D;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    

    void Update()
    {
                             // Update inputs every frame
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float depthInput = Input.GetAxis("Depth");
        bool shootPlayer = Input.GetButton("Fire1");
        
        switch(player3dMovement){

            case true:      // If true activate 3d movement controls
                Movement3D(horizontalInput, verticalInput, depthInput, shootPlayer);
            break;

            case false:     // If false activate 2d movement controls
                 Movement2D(horizontalInput, verticalInput, shootPlayer); 
            break;
        };

    }

    
    void Movement3D (float h, float v, float d, bool s){
        
        
        Player3D.transform.Translate(new Vector3(h, v, d) * moveSpeed * Time.deltaTime);
        if (s == true){

            // Instantiate bullet

        };
    
    }



    void Movement2D (float h, float v, bool s){


        Player2D.transform.Translate(new Vector2(h, v) * moveSpeed * Time.deltaTime);
          
        if (s == true){

            // Instantiate bullet

        };

    }

      

}
