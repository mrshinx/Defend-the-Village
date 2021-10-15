using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    // Use this for initialization

    float ratioheight;
    float ratiowidth;

    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        float aspect =(float)Screen.width/(float)Screen.height;
        float Camheight = 1080f/18f * Camera.main.orthographicSize * 2f;
        float Camwidth = Camheight * aspect;
        ratioheight =  Camheight/1080f ;
        ratiowidth = Camwidth/1920f;
        gameObject.transform.localScale = new Vector3(ratiowidth, ratioheight, 1);
    }

}
