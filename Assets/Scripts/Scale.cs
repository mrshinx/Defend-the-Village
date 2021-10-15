using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour {

    float ratioheight;
    float ratiowidth;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float aspect = (float)Screen.width / (float)Screen.height;
        float Camheight = 1080f / 18f * Camera.main.orthographicSize * 2f;
        float Camwidth = Camheight * aspect;
        ratioheight = Camheight / 1080f;
        ratiowidth = Camwidth / 1920f;
        gameObject.transform.localScale = new Vector3(ratiowidth * -1, ratioheight, 1);
    }
}
