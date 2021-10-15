using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class Passive : MonoBehaviour {

    public enum PassiveTypes
    {
        DamageOverTime, //0
        Weaken, //1
        CrowdControl, //2

    }
    public PassiveTypes passiveType;
    public GameObject debuffSource;
    public string description;
    public float baseDuration;
    public float baseDamage;
    public float baseChance;
    public float Damage;
    public float duration;
    public float chance;
    public float debuffTick;
    public string toolTip;
    Monster monster;

	// Use this for initialization
	void Start () {
        duration = baseDuration;
        Damage = baseDamage / duration;

        debuffTick = 0;
        monster = gameObject.transform.parent.gameObject.GetComponent<Monster>();

    }
	
	// Update is called once per frame
	void Update () {
        if (duration > 0) { duration -= Time.deltaTime; debuffTick += Time.deltaTime; }
        if (debuffTick >= 0.25f)
        {
            monster.lastHiter = debuffSource;
            monster.currentHP -= Damage / 4f;
            debuffTick = 0;
        }
        if (duration <= 0)
        {
            monster.lastHiter = debuffSource;
            monster.currentHP -= Damage / 4f;
            Destroy(gameObject);
        }

    }


    public void ResetDebuffDuration()
    {
        duration = baseDuration;
    }

    public string GetTooltip()
    {
        switch (passiveType)
        {
            case PassiveTypes.DamageOverTime:
                toolTip = "Attacks have ";
                break;
            case PassiveTypes.Weaken:
                break;
            case PassiveTypes.CrowdControl:
                break;
        }
        return toolTip;
    }
}
