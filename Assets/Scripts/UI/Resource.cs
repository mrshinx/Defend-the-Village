using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour {

    public static int gold;
    public static int magicCrystal;
    public static int life;
    [SerializeField] Text goldText;
    [SerializeField] Text magicCrystalText;
    [SerializeField] Text lifeText;

	// Use this for initialization
	void Start () {
        gold = 150;
        magicCrystal = 100;
        life = 50;
	}
	
	// Update is called once per frame
	void Update () {
        goldText.text ="" +gold;
        magicCrystalText.text ="" +magicCrystal;
        lifeText.text = "" + life;

	}

}
