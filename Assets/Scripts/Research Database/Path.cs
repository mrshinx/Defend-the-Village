using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Path : MonoBehaviour {

    public ResearchNode firstNode;
    public ResearchNode secondNode;

	// Update is called once per frame
	void Update () {

        if (firstNode.active && secondNode.active)
        {
            Color tmp = gameObject.GetComponent<Image>().color;
            tmp.a = 1f;
            gameObject.GetComponent<Image>().color = tmp; // brighten color
        }
        else
        {
            Color tmp = gameObject.GetComponent<Image>().color;
            tmp.a = 0.5f;
            gameObject.GetComponent<Image>().color = tmp; // darken color
        }

	}
}
