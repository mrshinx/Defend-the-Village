using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerSelector : MonoBehaviour {

    GameObject[] passiveDisplays = new GameObject[9];
    GameObject passiveDatabase;
    GameObject towerImage;
    GameObject towerName;
    GameObject towerEXP;
    GameObject towerLevel;
    GameObject towerDamage;
    GameObject towerAttackSPD;
    GameObject towerCritChance;
    GameObject towerCritMulti;
    GameObject towerMana;
    Sprite towerSprite;
    public static GameObject currentTarget;
    static GameObject lastTarget;
    Text manatextCache;
    Mana manaCache;

    // Use this for initialization
    void Start() {
        passiveDatabase = GameObject.Find("Passive Database");
        towerImage = GameObject.FindWithTag("Tower Image");
        towerName = GameObject.FindWithTag("Tower Name");
        towerEXP = GameObject.FindWithTag("Tower EXP");
        towerLevel = GameObject.FindWithTag("Tower Level Text");
        towerDamage = GameObject.FindWithTag("Tower Damage Text");
        towerAttackSPD = GameObject.FindWithTag("Tower Attack Speed Text");
        towerCritChance = GameObject.FindWithTag("Tower Crit Chance Text");
        towerCritMulti = GameObject.FindWithTag("Tower Crit Multi Text");
        towerMana = GameObject.FindWithTag("Tower Mana Text");
        for(int i = 0; i <= 8; i++)
        {
            passiveDisplays[i] = GameObject.Find("Passive " + (i+1).ToString());
        }
        manatextCache = towerMana.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {

        if (currentTarget != null)
        {
           manatextCache.text = "Mana: " + (float)System.Math.Round(manaCache.currentMana, 0) + "/" + manaCache.maximumMana; // Get Mana
        }

    }

    public void TowerSelect(GameObject tower,Sprite towerSprite, string name, float EXP,float level, float damage, float atkSPD, float critChance, 
        float critMulti, float mana, float maximumMana, List<GameObject> debuffs)

    {
        currentTarget = tower;
        manaCache = currentTarget.gameObject.GetComponent<Mana>();

        currentTarget.GetComponent<SpriteRenderer>().color = new Color(0, 0.9245283f, 0.3235681f); //Make current target green

        if ((lastTarget != null) && (lastTarget!= currentTarget))
        {
            lastTarget.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1); //Revert last target color
            lastTarget.GetComponent<TowerProperties>().isSelected = false;
        }

        towerImage.GetComponent<Image>().sprite = towerSprite;  // Get tower image
        towerImage.GetComponent<Image>().enabled = true; // Enable tower image

        towerName.GetComponent<Text>().text = name; // Get tower name

        towerEXP.GetComponent<RectTransform>().offsetMin = new Vector2(Mathf.Lerp(4.9337f, 0.109f,EXP), 0.10875f); // Get EXP

        towerLevel.GetComponent<Text>().text = "Level " + level; // Get tower level

        towerDamage.GetComponent<Text>().text = "Attack Damage: " + (float)System.Math.Round(damage,1); // Get tower damage

        towerAttackSPD.GetComponent<Text>().text = "Attack Speed: " + (float)System.Math.Round(atkSPD, 2); // Get tower atkSPD

        towerCritChance.GetComponent<Text>().text = "Critical Strike Chance: " + (float)System.Math.Round(critChance * 100, 2)+"%"; // Get tower Crit Chance

        towerCritMulti.GetComponent<Text>().text = "Critical Strike Multiplier: " + (float)System.Math.Round(critMulti * 100, 2)+ "%"; // Get tower Crit Multiplier

        foreach (GameObject passiveDisplay in passiveDisplays)
        {
            Image cacheDisplay = passiveDisplay.GetComponent<Image>();
            cacheDisplay.sprite = null;
            cacheDisplay.color = new Color(1, 1, 1, 0);
            passiveDisplay.GetComponent<Tooltip>().hardTooltip = "";
        }

        int index = 0; 
        foreach (GameObject debuff in debuffs)
        {
            Image displayCache = passiveDisplays[index].GetComponent<Image>();
            Tooltip tooltipCache = passiveDisplays[index].GetComponent<Tooltip>();
            displayCache.sprite = debuff.GetComponent<SpriteRenderer>().sprite;
            displayCache.color = new Color(1f, 1f, 1f, 1f);
            tooltipCache.hardTooltip = debuff.GetComponent<Text>().text;

            index += 1;
        }

        lastTarget = tower;

    }

    public void SellTower()
    {
        currentTarget.transform.parent.gameObject.GetComponent<TowerSpawn>().haveTower = false;
        Resource.gold += currentTarget.GetComponent<TowerProperties>().cost;
        Destroy(currentTarget);
        NoTarget();

    }

    private void NoTarget()
    {
        towerImage.GetComponent<Image>().sprite = null;
        towerImage.GetComponent<Image>().enabled = false;

        towerName.GetComponent<Text>().text = null;

        towerEXP.GetComponent<RectTransform>().offsetMin = new Vector2(Mathf.Lerp(4.89f, 0.109f, 0), 0.10875f);

        towerLevel.GetComponent<Text>().text = "Level ";

        towerDamage.GetComponent<Text>().text = "Attack Damage: ";

        towerAttackSPD.GetComponent<Text>().text = "Attack Speed: ";

        towerCritChance.GetComponent<Text>().text = "Critical Strike Chance: ";

        towerCritMulti.GetComponent<Text>().text = "Critical Strike Multiplier: ";

        manatextCache.text = "Mana: ";
    }
}
