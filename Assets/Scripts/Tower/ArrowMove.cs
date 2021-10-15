using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour {

    public GameObject target;
    Collider2D[] targets;
    public List<GameObject> chainedtargets;
    GameObject lootManager;
    TowerProperties towerCache;
    float baseSpeed;
    public float damage;
    float dmgAmp;
    float armorPen;
    public bool isCrit = false;
    public List<GameObject> debuffs;
    GameObject newDebuff;
    DoT cacheDoT;
    int debuffCount;
    float knockbackX;
    float knockbackY;
    public bool knockBack = false;
    ResearchBonus Bonus;
    public bool chain = false;
    public int chainsleft=3;

    private void Awake()
    {
        
    }


    void Start () {
        towerCache = transform.parent.gameObject.GetComponent<TowerProperties>();
        Bonus = transform.parent.gameObject.GetComponent<ResearchBonus>();
        chain = Bonus.chain;

        debuffs = towerCache.learnedDebuffs;
        lootManager = GameObject.FindWithTag("Loot Manager");
        baseSpeed = towerCache.projectileSpeed;
        damage = towerCache.damage*(1+Bonus.Projectile_Damage);
        Crit(towerCache.critChance);
        dmgAmp = towerCache.dmgAmp;
        armorPen = towerCache.armorPen;
        knockbackX = towerCache.knockbackX;
        knockbackY = towerCache.knockbackY;
    }
	
	void Update () {

        if (target == null) Destroy(gameObject);
        else
        {
            transform.rotation = Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2((target.transform.position.y - transform.position.y + 0.5f), (target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(0, 0.5f, 0), baseSpeed * Time.deltaTime);
            transform.position += new Vector3(0, 0, -4f);
        }
        if(gameObject.GetComponent<Collider2D>().IsTouching(target.GetComponent<Collider2D>()))
            Hit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == target.gameObject.GetComponent<Collider2D>())
        {
            Hit();
        }
    }

    private void Hit()
    {

        target.gameObject.GetComponent<Monster>().lastHiter = transform.parent.gameObject;
        target.gameObject.GetComponent<Defense>().DamageCalc(damage, (int)transform.parent.gameObject.GetComponent<TowerProperties>().damageType, dmgAmp, armorPen);

        towerCache.Proc(target); // proc auras

        if (isCrit)
        lootManager.GetComponent<Loot>().Crit(target.gameObject.GetComponent<Defense>().damage, transform.position); //apply crit notifier

        if(knockBack)
        target.gameObject.GetComponent<Monster>().Knockback(knockbackX, knockbackY); // apply Knockback
        if (chain) Chain();
        else
        Destroy(gameObject);
    }

    private void Crit(float chance)
    {
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            isCrit = true;
            damage *= (towerCache.critMulti);
        }


    }

    private void Chain()
    {
        chainedtargets.Add(target);
        int count1 = 0;
        int count2 = 0;

        targets = Physics2D.OverlapCircleAll(transform.position, 2f);
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.tag == "Monster")
                {
                    count1 = 0;
                    foreach (GameObject chainedTarget in chainedtargets)
                    {
                        if (targets[i].gameObject == chainedTarget)
                            count1++;
                    }
                    if (count1 == 0)
                    {
                        count2++;
                        Debug.Log("count 2: " +count2);
                        target = targets[i].gameObject;
                        chainsleft--;
                        if (chainsleft <= 0) chain = false;
                        break;
                    }
                }
                
            }
            if (count2 == 0) Destroy(gameObject);
        }
        else Destroy(gameObject);
    }


}


