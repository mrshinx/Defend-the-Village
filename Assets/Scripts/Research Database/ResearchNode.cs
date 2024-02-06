using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class ResearchNode : MonoBehaviour {
    public enum NodeType
    {
        ProjectileDamage,
        AttackSpeed,
        CriticalStrikeChance,
        CriticalStrikeMultiplier,
        Knockback
    }

    public NodeType nodeType;
    [Range(0.0f, 100f)] public float magnitude;
    GameObject ResearchManagerObj;
    public ResearchNode[] prerequisiteNodes;

    [System.NonSerialized]
    public bool active = false;
    void Start() {

        ResearchManagerObj = GameObject.Find("Research Manager");
        ResearchManagerObj.GetComponent<ResearchManager>().Reset += ResetColor;

    }

    // Update is called once per frame
    void Update() {


    }

    private void OnMouseDown()
    {
        if (TowerSelector.currentTarget == null) return;

        if (prerequisiteNodes.Length==0 || prerequisiteNodes.Any(node => node.active))
        {
            Activate();
            return;
        }

    }

    private void ResetColor()
    {
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 0.5f;
        gameObject.GetComponent<Image>().color = tmp;
    }

    public void ReColor()
    {
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 1f;
        gameObject.GetComponent<Image>().color = tmp;
    }

    private void Activate()
    {
        active = true;
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 1f;
        gameObject.GetComponent<Image>().color = tmp; // brighten color

        ResearchManagerObj.GetComponent<ResearchManager>().Add_Bonus(nodeType,magnitude / 100); // add value
    }

    private void Disable()
    {
        active = false;
        Color tmp = gameObject.GetComponent<Image>().color;
        tmp.a = 0.5f;
        gameObject.GetComponent<Image>().color = tmp; // darken color

        ResearchManagerObj.GetComponent<ResearchManager>().Remove_Bonus(nodeType, magnitude / 100); // remove value
    }
}
