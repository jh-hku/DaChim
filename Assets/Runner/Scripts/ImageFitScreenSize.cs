using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFitScreenSize : MonoBehaviour
{
    public LayoutElement backgroundLayout;

    // Start is called before the first frame update
    void Start() {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // Debug.Log("Screen width: " + screenWidth);
        // Debug.Log("Screen height: " + screenHeight);
        if (backgroundLayout.flexibleHeight / screenHeight > backgroundLayout.flexibleWidth / screenWidth)
        {
            backgroundLayout.minHeight = screenHeight;
        } else {
            backgroundLayout.minWidth = screenWidth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
