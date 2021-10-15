using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector2(-1f, 1f);
        Debug.Log(transform.localScale);
    }
}
