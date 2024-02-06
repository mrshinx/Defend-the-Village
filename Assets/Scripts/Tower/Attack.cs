using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class Attack : MonoBehaviour {

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject firePos;
    [SerializeField] AudioClip[] fireSound;
    bool canAttack = true;
    bool repeat = false;
    int layerMask = 1 << 8;
    [System.NonSerialized]
    public TowerProperties towerProp;
    List<Collider2D> targetsInRange;
    List<Collider2D> inflicted_targets = new List<Collider2D>();
    List<Collider2D> targetsTohit;
    GameObject projectile;
    // [System.NonSerialized]
    public List<AuraObject> debuffs;
    [System.NonSerialized]
    public float attackRange;

    // Special bonus
    public int projectileCount;
    public float repeatChance;

    public virtual void Start()
    {
        towerProp = gameObject.GetComponent<TowerProperties>();
        attackRange = towerProp.attackRange;
        debuffs = towerProp._debuffs;

        projectileCount = towerProp.projectileCount;
        repeatChance = towerProp.repeatChance;
    }

    void Update() {

        // Scan targets in range
        targetsInRange = Physics2D.OverlapCircleAll(transform.position, attackRange, layerMask).ToList();
        if (targetsInRange.Count == 0) return;
        targetsInRange.Sort((a,b) => a.gameObject.transform.position.x.CompareTo(b.transform.position.x));

        if (towerProp.auraTower) InflictDebuff();
        else if (targetsInRange.Count >0 && canAttack) FireCheck();
    }

    private void InflictDebuff()
    {
        List<Collider2D> temp_list = new List<Collider2D>();

        // Find targets that are already inflicted and now out of range
        temp_list = inflicted_targets.FindAll(t => !targetsInRange.Contains(t));
        foreach (Collider2D target in temp_list)
        {
            // Remove inflicted aura (or debuff) on this target now that it's out of this tower's range
            foreach (AuraObject debuff in debuffs)
            {
                target.gameObject.GetComponent<Monster>().Auras.RemoveAll(aura => aura.auraId == debuff.auraId && aura.source == gameObject);
            }

            // Remove target from inflicted list
            inflicted_targets.Remove(target);
        }

        // Find targets in range that are not yet inflicted in range
        temp_list = targetsInRange.FindAll(t => !inflicted_targets.Contains(t));
        foreach (Collider2D target in temp_list)
        {
            foreach (AuraObject debuff in debuffs)
            {
                Aura debuffInstance = AuraManager.CreateAuraInstance(debuff);
                debuffInstance.source = gameObject;
                target.GetComponent<Monster>().ApplyAura(debuffInstance);
            }

            // Add target to inflicted list
            inflicted_targets.Add(target);
        }
    }

    private void FireCheck()
    {
        // Pick closest target to the left in detect range
        targetsTohit = targetsInRange.Take(projectileCount).ToList();

        // Determine if this attack is repeated
        float roll = UnityEngine.Random.Range(0f, 1f);
        if (roll <= repeatChance)
        {
            repeat = true;
        }

        foreach (Collider2D target in targetsTohit)
        {
            // Check condition to attack
            if (target.CompareTag("Monster") && (target != null)) //&& (gameObject.GetComponent<Mana>().currentMana >= 25))
            {
                if (gameObject.GetComponent<TowerProperties>().caster)
                {
                    if (gameObject.GetComponent<Mana>().currentMana < gameObject.GetComponent<TowerProperties>().ManaCost) return;
                    gameObject.GetComponent<Mana>().currentMana -= gameObject.GetComponent<TowerProperties>().ManaCost;
                }

                Fire(target.gameObject);
            }
        }
        StartCoroutine(Cooldown());
    }
    private void Fire(GameObject target)
    {
        if (target != null)
        {
            // Play firing audio
            if (fireSound.Length > 0)
            {
                int soundIndex = UnityEngine.Random.Range(0, fireSound.Length);
                AudioSource.PlayClipAtPoint(fireSound[soundIndex], Camera.main.transform.position, 0.08f);
            }

            projectile = Instantiate(projectilePrefab, firePos.transform.position, Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2((target.transform.position.y - transform.position.y + 0.5f), (target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg));
            projectile.transform.parent = gameObject.transform;
            projectile.GetComponent<Projectile>().target = target;
            if (target.transform.position.x > transform.position.x) projectile.GetComponent<Projectile>().knockBack = true;
        }
    }
    IEnumerator Cooldown()
    {
        canAttack = false;

        if (repeat) yield return new WaitForSeconds(0.1f);
        else yield return new WaitForSeconds(gameObject.GetComponent<TowerProperties>().attackSpeed);

        repeat = false;
        canAttack = true;
    }

    public void CalculateUpgrade()
    {
        projectileCount = towerProp.projectileCount;
        repeatChance = towerProp.repeatChance;
    }
}
