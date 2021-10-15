using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition : MonoBehaviour {

    // Use this for initialization

    [SerializeField] float width;
    static Vector2 fallSpeed = new Vector2(0.005f,0 );

    void Start () {

    }
    // Update is called once per frame
    void Update()
    {
        //   Rect viewportRect = Camera.main.pixelRect;
        //   Vector3 newPos = new Vector3(viewportRect.xMin+ width , 0, 1);
        //   this.transform.position = Camera.main.ScreenToWorldPoint(newPos);

        transform.position = new Vector2(transform.position.x, transform.position.y) + fallSpeed;
    }
}
