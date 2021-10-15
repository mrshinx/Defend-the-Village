using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Path : MonoBehaviour {

    public GameObject firstNode;
    public GameObject secondNode;
    Image firstNodeColorCache;
    Image secondNodeColorCache;
    Image ColorCache;
    Color originalColor;

	void Start () {
        firstNodeColorCache = firstNode.GetComponent<Image>();
        secondNodeColorCache = secondNode.GetComponent<Image>();
        ColorCache = gameObject.GetComponent<Image>();
        originalColor = ColorCache.color;
    }
	
	// Update is called once per frame
	void Update () {

        if (firstNodeColorCache.color.a == secondNodeColorCache.color.a)
        {
            Color tmp = firstNodeColorCache.color;
            ColorCache.color = tmp;
        }
        else ColorCache.color = originalColor;

	}
}
