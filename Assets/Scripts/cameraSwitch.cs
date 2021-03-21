using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{

    [SerializeField] private bool perspSwitch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("Fire1"))
       {
           SwitchCamera();
           
       }
    }

    public void SwitchCamera()
    {
        perspSwitch = !perspSwitch;

        Camera.main.orthographic = perspSwitch;
        Debug.Log("Camera Switched");

    }

}
