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
    public List<AuraObject> _debuffs;
    public List<GameObject> activeResearch;

    public int cost;
    [System.NonSerialized]
    public float dmgAmp = 1f;
    public float armorPen;

    public float baseAttackSpeed;
    [System.NonSerialized]
    public float attackSpeed;
    float bonusAttackSpeedLvl;

    public float baseDamage;
    [System.NonSerialized]
    public float damage;
    float bonusDamageLvl;
    public float bonusDamageResearch;

    public float baseCritChance;
    [System.NonSerialized]
    public float critChance;
    float bonusCritChanceLvl;

    public float baseCritMulti;
    [System.NonSerialized]
    public float critMulti;
    float bonusCritMultiLvl;

    [System.NonSerialized]
    public float baseManaCost;
    public float ManaCost;
    float bonusManaCost;
    public float projectileSpeed;
    public float attackRange;
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
    public bool auraTower;

    // Special bonus
    public bool canChain = false;
    public int chainCount;
    public int projectileCount;
    public float repeatChance;

    GameObject selector;
    GameObject passiveDatabase;
    [SerializeField] GameObject levelUpVFX;
    public GameObject infuseShadow;
    public GameObject infuseFire;
    public List<GameObject> infuseList;
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

            CalculateFinalStat();
            CalculateUpgrade();
}
        
        selector = GameObject.FindWithTag("Tower Selector");
        passiveDatabase = GameObject.Find("Passive Database");
        spawnerName = transform.parent.name;

        foreach (int a in debuffIds)
        {
            debuffs.Add(passiveDatabase.GetComponent<PassiveDatabase>().PassivesInfo[a]);
            learnedDebuff = Instantiate(debuffs[a],transform.position,Quaternion.identity);
            learnedDebuff.transform.SetParent(transform);
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

        var _cacheStatGrowth = gameObject.GetComponent<StatGrowth>();

        bonusDamageLvl += _cacheStatGrowth.bonusDamageLvl;
        bonusAttackSpeedLvl += _cacheStatGrowth.bonusAttackSpeedLvl;
        bonusCritChanceLvl += _cacheStatGrowth.bonusCritChanceLvl;
        bonusCritMultiLvl += _cacheStatGrowth.bonusCritMultiLvl;

        CalculateFinalStat();
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

        var _cacheStatGrowth = gameObject.GetComponent<StatGrowth>();

        bonusDamageLvl += _cacheStatGrowth.bonusDamageLvl * (level-1);
        bonusAttackSpeedLvl += _cacheStatGrowth.bonusAttackSpeedLvl * (level - 1);
        bonusCritChanceLvl += _cacheStatGrowth.bonusCritChanceLvl * (level - 1);
        bonusCritMultiLvl += _cacheStatGrowth.bonusCritMultiLvl * (level - 1);

        CalculateFinalStat();
        CalculateUpgrade();
        selector = GameObject.FindWithTag("Tower Selector");
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        percentEXP = EXP / currentEXPCap;
        selector.GetComponent<TowerSelector>().TowerSelect(transform.gameObject, gameObject.GetComponent<SpriteRenderer>().sprite, towerName, percentEXP, level, damage, attackSpeed,
        critChance, critMulti, gameObject.GetComponent<Mana>().currentMana, gameObject.GetComponent<Mana>().maximumMana, learnedDebuffs);
    }

    public void CalculateFinalStat()
    {
        damage = baseDamage * (1 + bonusDamageLvl + bonusDamageResearch);

        attackSpeed = baseAttackSpeed / (1 + bonusAttackSpeedLvl);

        critChance = baseCritChance + baseCritChance * bonusCritChanceLvl;

        critMulti = baseCritMulti + bonusCritMultiLvl;

        var _cacheStatGrowth = gameObject.GetComponent<StatGrowth>();
        var _cacheMana = gameObject.GetComponent<Mana>();
        _cacheMana.maximumMana = _cacheMana.baseMaximumMana + _cacheStatGrowth.bonusManaLvl;
        _cacheMana.manaRegen = _cacheMana.baseManaRegen * (1 + _cacheStatGrowth.bonusManaRegenLvl * (level - 1));
    }

    public void CalculateUpgrade()
    {
        chainCount = Mathf.FloorToInt(TowerUpgrade.chainCount);
        projectileCount = Mathf.FloorToInt(TowerUpgrade.projectileCount);
        repeatChance = TowerUpgrade.repeatChance;
        gameObject.GetComponent<Attack>().CalculateUpgrade();
    }
}
