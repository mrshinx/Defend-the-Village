using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using static ResearchNode;

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

    public void Add_Bonus(NodeType nodeType, float magnitude)
    {
        TowerProperties propCache = TowerSelector.currentTarget.GetComponent<TowerProperties>();
        switch (nodeType)
        {
            case NodeType.ProjectileDamage:
                propCache.bonusDamageResearch += magnitude;
                break;
        }

        propCache.CalculateFinalStat();
    }

    public void Remove_Bonus(NodeType nodeType, float magnitude)
    {
        TowerProperties propCache = TowerSelector.currentTarget.GetComponent<TowerProperties>();
        switch (nodeType)
        {
            case NodeType.ProjectileDamage:
                propCache.bonusDamageResearch -= magnitude;
                break;
        }

        propCache.CalculateFinalStat();
    }

    public void RedrawNodes()
    {
        foreach (GameObject node in TowerSelector.currentTarget.GetComponent<TowerProperties>().activeResearch)
        {
            node.GetComponent<ResearchNode>().ReColor();
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
