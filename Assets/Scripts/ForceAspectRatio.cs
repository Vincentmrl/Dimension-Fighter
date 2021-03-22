using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        float targetAspect = 16.0f / 9.0f;
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Control if window respects target aspect ratio
        float ScaleHeight = windowAspect / targetAspect;

        Camera mainCam = GetComponent<Camera>();

        // Get width and height of camera
        Rect forceAspectCamera = mainCam.rect;

        // If 
        if (ScaleHeight < 1.0f)
        {

            forceAspectCamera.width = 1.0f;
            forceAspectCamera.height = ScaleHeight;
            forceAspectCamera.x = 0;
            forceAspectCamera.y = (1.0f - ScaleHeight) / 2.0f;



        }
        else
        {

            float scaleWidth = 1.0f / ScaleHeight;
            
            forceAspectCamera.width = scaleWidth;
            forceAspectCamera.height = 1.0f;
            forceAspectCamera.x = (1.0f - scaleWidth) / 2.0f;
            forceAspectCamera.y = 0;

        }

        mainCam.rect = forceAspectCamera;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
