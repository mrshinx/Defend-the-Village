using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProperties : MonoBehaviour {


    public delegate void OnHit(GameObject target);
    public OnHit Proc;

    public DamageTypesDatabase.DamageTypes damageType;

    public string spawnerName;
    public int TowerIndex;
    public int[] debuffIds;
    public List<GameObject> debuffs;
    public List<GameObject> learnedDebuffs;
    public GameObject learnedDebuff;
    public List<GameObject> activeResearch;
    public int cost;
    public float dmgAmp;
    public float armorPen;
    public float baseAttackSpeed;
    public float attackSpeed;
    float bonusAttackSpeedLvl;
    public float baseDamage;
    public float damage;
    float bonusDamageLvl;
    public float baseCritChance;
    public float critChance;
    float bonusCritChanceLvl;
    public float baseCritMulti;
    public float critMulti;
    float bonusCritMultiLvl;
    public float projectileSpeed;
    public float aoe;
    public float knockbackX;
    public float knockbackY;
    public string towerName;
    float percentEXP;
    float currentEXPCap;
    public float EXP;
    public bool isSelected;
    public float level;
    public bool upgrade;
    public bool caster;
    GameObject selector;
    GameObject passiveDatabase;
    [SerializeField] GameObject levelUpVFX;
    public GameObject infuseShadow;
    public GameObject infuseFire;
    [SerializeField] AudioClip levelUpSound;

    // Use this for initialization
    void Start () {
        if (!upgrade)
        {
            level = 1;
            EXP = 0;
            currentEXPCap = 5;
            percentEXP = EXP / currentEXPCap;
            isSelected = false;
            damage = baseDamage;
            attackSpeed = baseAttackSpeed;
            critChance = baseCritChance;
            critMulti = baseCritMulti;
        }
        
        selector = GameObject.FindWithTag("Tower Selector");
        passiveDatabase = GameObject.Find("Passive Database");
        spawnerName = transform.parent.name;

        foreach (int a in debuffIds)
        {
            debuffs.Add(passiveDatabase.GetComponent<PassiveDatabase>().PassivesInfo[a]);
            learnedDebuff = Instantiate(debuffs[a],transform.position,Quaternion.identity);
            learnedDebuff.transform.parent = transform;
            learnedDebuffs.Add(learnedDebuff);
        }

    }

    // Update is called once per frame

    void Update () {
        
        if (EXP >= currentEXPCap)
        {
            LevelUp();
        }
    }

    private void OnMouseDown()
    {
        isSelected = true;
        percentEXP = EXP / currentEXPCap;
        UpdateInfo();
    }

    private void LevelUp()
    {
        AudioSource.PlayClipAtPoint(levelUpSound, Camera.main.transform.position);
        GameObject tempVFX = Instantiate(levelUpVFX, transform.position + new Vector3(0, 0, -2), Quaternion.Euler(-90, 0, 0));
        Destroy(tempVFX, 2);

        EXP = EXP - currentEXPCap;
        level += 1;
        currentEXPCap = currentEXPCap *1.1f;
        percentEXP = EXP / currentEXPCap;

        bonusDamageLvl += gameObject.GetComponent<StatGrowth>().bonusDamageLvl;
        damage = baseDamage * (1 + bonusDamageLvl);

        bonusAttackSpeedLvl += gameObject.GetComponent<StatGrowth>().bonusAttackSpeedLvl;
        attackSpeed = baseAttackSpeed /(1 + bonusAttackSpeedLvl);

        bonusCritChanceLvl += gameObject.GetComponent<StatGrowth>().bonusCritChanceLvl;
        critChance = baseCritChance + baseCritChance*bonusCritChanceLvl;

        bonusCritMultiLvl += gameObject.GetComponent<StatGrowth>().bonusCritMultiLvl;
        critMulti = baseCritMulti + bonusCritMultiLvl;

        gameObject.GetComponent<Mana>().maximumMana = gameObject.GetComponent<Mana>().basemaximumMana + gameObject.GetComponent<StatGrowth>().bonusManaLvl;
        gameObject.GetComponent<Mana>().manaRegen = gameObject.GetComponent<Mana>().basemanaRegen*(1+ gameObject.GetComponent<StatGrowth>().bonusManaRegenLvl*(level-1));

        if (isSelected) UpdateInfo();

    }

    public void GetExp(float expAmount)
    {
        EXP += expAmount;
        if((EXP<currentEXPCap) && (isSelected))
        UpdateInfo();
    }

    public void Initialization(float iniLevel, float iniEXP)
    {
        upgrade = true;

        level = iniLevel;
        currentEXPCap = 5 * Mathf.Pow(1.1f, level-1);
        EXP = iniEXP;
        percentEXP = EXP / currentEXPCap;

        bonusDamageLvl += gameObject.GetComponent<StatGrowth>().bonusDamageLvl * (level-1);
        damage = baseDamage * (1 + bonusDamageLvl);

        bonusAttackSpeedLvl += gameObject.GetComponent<StatGrowth>().bonusAttackSpeedLvl * (level - 1);
        attackSpeed = baseAttackSpeed / (1 + bonusAttackSpeedLvl);

        bonusCritChanceLvl += gameObject.GetComponent<StatGrowth>().bonusCritChanceLvl * (level - 1);
        critChance = baseCritChance + baseCritChance * bonusCritChanceLvl;

        bonusCritMultiLvl += gameObject.GetComponent<StatGrowth>().bonusCritMultiLvl * (level - 1);
        critMulti = baseCritMulti + bonusCritMultiLvl;

        selector = GameObject.FindWithTag("Tower Selector");
        UpdateInfo();

    }

    public void UpdateInfo()
    {
        percentEXP = EXP / currentEXPCap;
        selector.GetComponent<TowerSelector>().TowerSelect(transform.gameObject, gameObject.GetComponent<SpriteRenderer>().sprite, towerName, percentEXP, level, damage, attackSpeed,
        critChance, critMulti, gameObject.GetComponent<Mana>().currentMana, gameObject.GetComponent<Mana>().maximumMana, learnedDebuffs);
    }
}
