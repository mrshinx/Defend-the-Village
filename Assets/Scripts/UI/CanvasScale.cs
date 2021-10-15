using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScale : MonoBehaviour {

    float ratioheight;
    float ratiowidth;
    float originalWidth;
    float originalHeight;
    float x;
    float y;

    // Use this for initialization
    void Start()
    {
        originalWidth = Screen.width;
        originalHeight = Screen.height;
        x = gameObject.transform.localScale.x;
        y = gameObject.transform.localScale.y;
            float aspect = (float)Screen.width / (float)Screen.height;
            float Camheight = 1080f / 18f * Camera.main.orthographicSize * 2f;
            float Camwidth = Camheight * aspect;
            ratioheight = Camheight / 1080f;
            ratiowidth = Camwidth / 1920f;
            gameObject.transform.localScale = new Vector3(x * ratiowidth, y * ratioheight, gameObject.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if ((originalWidth != Screen.width) || ((originalHeight != Screen.height)))
        {
            originalWidth = Screen.width;
            originalHeight = Screen.height;
            float aspect = (float)Screen.width / (float)Screen.height;
            float Camheight = 1080f / 18f * Camera.main.orthographicSize * 2f;
            float Camwidth = Camheight * aspect;
            ratioheight = Camheight / 1080f;
            ratiowidth = Camwidth / 1920f;
            gameObject.transform.localScale = new Vector3(x* ratiowidth, y * ratioheight, gameObject.transform.localScale.z);
        }
    }
}
