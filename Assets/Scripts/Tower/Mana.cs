using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour {

    public float currentMana ;
    public float basemaximumMana;
    public float maximumMana;
    public GameObject manaDisplay;
    public float basemanaRegen;
    public float manaRegen;

    void Start () {

        maximumMana = basemaximumMana;
        manaRegen = basemanaRegen;

	}
	
	// Update is called once per frame
	void Update () {

        if (manaDisplay != null)
        {
            if (currentMana <= maximumMana) currentMana += manaRegen * Time.deltaTime;
            if (currentMana > maximumMana) currentMana = maximumMana;
            manaDisplay.GetComponent<RectTransform>().offsetMax = new Vector2(-(0.78425f - (0.78425f - 0.01f) * (currentMana / maximumMana)), 0);
        }

    }
}
