using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;

public class DoT : MonoBehaviour
{

    public GameObject debuffSource;
    public int debuffId;
    public float baseDamage;
    public float Damage;
    public float chance;
    public float duration;
    public float debuffTick;
    bool onTarget = false;
    public bool onTower = false;
    Monster monster;

    // Use this for initialization
    void Start()
    {
        debuffTick = 0;
        if (transform.parent.gameObject!=null)
        {
            if ((transform.parent.gameObject.CompareTag("Monster")))
            {
                monster = gameObject.transform.parent.gameObject.GetComponent<Monster>();
                onTarget = true;
            }

            if ((transform.parent.gameObject.CompareTag("Tower")))
            {
                debuffSource = transform.parent.gameObject;
                duration = gameObject.GetComponent<AuraInformation>().baseDuration;
                chance = gameObject.GetComponent<AuraInformation>().chance;
                baseDamage = debuffSource.GetComponent<TowerProperties>().damage / 3f;
                Damage = baseDamage / duration;
                GetComponent<DoTTooltip>().GetTooltip();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onTarget)
        {
            debuffTick += Time.deltaTime;
            if (debuffTick >= 0.25f)
            {
                monster.lastHiter = debuffSource;
                monster.currentHP -= (Damage / 4f) * gameObject.GetComponent<AuraInformation>().stacks;
                debuffTick -= 0.25f;
            }
        }

    }

    
}
