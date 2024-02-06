using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [System.NonSerialized]
    public GameObject target;
    [System.NonSerialized]
    public List<Collider2D> targets;
    public List<GameObject> chainedtargets;
    [System.NonSerialized]
    public GameObject lootManager;
    [System.NonSerialized]
    public TowerProperties towerProp;
    [System.NonSerialized]
    public float baseSpeed;
    [System.NonSerialized]
    public float damage;
    [System.NonSerialized]
    public float dmgAmp;
    [System.NonSerialized] 
    public float armorPen;
    public bool isCrit = false;
    [System.NonSerialized]
    public List<AuraObject> debuffs;
    GameObject newDebuff;
    DoT cacheDoT;
    int debuffCount;
    [System.NonSerialized]
    public float knockbackX;
    [System.NonSerialized]
    public float knockbackY;
    public bool knockBack = false;

    // Special bonus
    public bool canChain = false;
    public int chainCount;

    public virtual void Start() {
        lootManager = GameObject.FindWithTag("Loot Manager");

        towerProp = transform.parent.gameObject.GetComponent<TowerProperties>();
        canChain = towerProp.canChain;
        chainCount = towerProp.chainCount;

        debuffs = towerProp._debuffs;

        // Determine if this projectile is gonna crit
        Crit(towerProp.critChance);

        baseSpeed = towerProp.projectileSpeed;
        damage = towerProp.damage;

        dmgAmp = towerProp.dmgAmp;
        armorPen = towerProp.armorPen;
        knockbackX = towerProp.knockbackX;
        knockbackY = towerProp.knockbackY;
    }
	
	void Update () {

        if (target == null) Destroy(gameObject);
        else Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == target.gameObject.GetComponent<Collider2D>())
        {
            Hit();
            InflictDebuff();
            if (chainCount == 0) Destroy(gameObject);
            chainCount--;
        }
    }

    public abstract void Move();

    public abstract void Hit();

    public void InflictDebuff()
    {
        if (!target.TryGetComponent(out IAffectableByAura _target)) return;
        foreach (AuraObject debuff in debuffs)
        {
            Aura debuffInstance = AuraManager.CreateAuraInstance(debuff);
            debuffInstance.source = transform.parent.gameObject;
            _target.ApplyAura(debuffInstance);
        }
    }

    public virtual void Crit(float chance)
    {
        float roll = Random.Range(0f, 1f);
        if (roll <= chance)
        {
            isCrit = true;
            damage *= (towerProp.critMulti);
        }
    }




}


