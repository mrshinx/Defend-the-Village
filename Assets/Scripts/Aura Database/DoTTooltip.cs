using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoTTooltip : MonoBehaviour {

    public enum DoTTypes
    {
        Bleed,
        Monster,
        Tower,
        Passive
    }
    public DoTTypes DoTType;

    public string tooltip;
    float Damage;
    float Duration;
    float Chance;
    DoT objCache;
    AuraInformation auraInfo;
    Text tooltiptext;

    // Use this for initialization
    void Start () {
        tooltiptext = gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        
	}
    public void GetTooltip()
    {
        objCache = gameObject.GetComponent<DoT>();
        auraInfo = gameObject.GetComponent<AuraInformation>();

        Duration = objCache.duration;
        Damage = objCache.Damage * Duration;
        Chance = auraInfo.chance;

        if (!auraInfo.stackable)
            tooltip = "Attacks have " + Chance * 100 + "% chance to inflict Bleed, dealing " + Damage + " damage in " + Duration + " seconds";
        else
            tooltip = "Attacks have " + Chance * 100 + "% chance to inflict " + auraInfo.AuraName + ", dealing " + System.Math.Round(Damage, 1) + " damage over " + Duration + " seconds. \n" + "Stacks up to " + auraInfo.maxStack + " times.";
        tooltiptext.text = tooltip;
    }
}
