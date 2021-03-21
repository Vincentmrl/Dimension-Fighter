using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTimeChangeScript : MonoBehaviour
{
    
    public GameObject Vcamera3D;
    public GameObject Vcamera2D;
    [SerializeField] private bool perspSwitch = true;




    void Start()    // Make sure the 2D camera is active at the start
    {
        Vcamera2D.SetActive(true);  // Cinemachine priority will make it the default
        Vcamera3D.SetActive(true);
        StartCoroutine("CameraChange");

    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator CameraChange()
    {
        Debug.Log("Coroutine started");
        while(true){
            Debug.Log("10 seconds wait started");
            yield return new WaitForSeconds(10f);

            if (perspSwitch == false)
            {
                Vcamera2D.SetActive(false);
                Debug.Log("Camera 3D enabled");
            }
            else 
            {
                Vcamera2D.SetActive(true);
                Debug.Log("Camera 2D enabled");
            }


            Camera.main.orthographic = perspSwitch; 
            perspSwitch = !perspSwitch;

        }
        
    }
}
