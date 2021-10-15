using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchManager : MonoBehaviour {

    public delegate void ResetNode();
    public ResetNode Reset;

    GameObject ResearchMenu;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResearchMenu = GameObject.Find("Research Menu");
            ResearchMenu.SetActive(false);
            Reset();

        }

    }

    public void Projectile_Damage(float value, GameObject node, bool respect)
    {
        TowerSelector.currentTarget.GetComponent<ResearchBonus>().Projectile_Damage += value;
        if (!respect)
            TowerSelector.currentTarget.GetComponent<TowerProperties>().activeResearch.Add(node);
        else
            TowerSelector.currentTarget.GetComponent<TowerProperties>().activeResearch.Remove(node);
    }

    public void RedrawNodes()
    {
        foreach (GameObject node in TowerSelector.currentTarget.GetComponent<TowerProperties>().activeResearch)
        {
            node.GetComponent<ProjectileDamage>().ReColor();
        }
    }


    public void ChainEnable(bool state, GameObject node, bool respect)
    {
        TowerSelector.currentTarget.GetComponent<ResearchBonus>().chain = state;
        if (!respect)
            TowerSelector.currentTarget.GetComponent<TowerProperties>().activeResearch.Add(node);
        else
            TowerSelector.currentTarget.GetComponent<TowerProperties>().activeResearch.Remove(node);
    }

}
