using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AuraTooltipInfo : MonoBehaviour {

    public int AuraID;
    public float AuraBaseDuration;
    public float AuraDuration;
    public int stacks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = (AuraBaseDuration-AuraDuration) / AuraBaseDuration;
        if (stacks > 1)
        {
            transform.GetChild(2).gameObject.GetComponent<Text>().text = "" + stacks;
        }

    }
}
