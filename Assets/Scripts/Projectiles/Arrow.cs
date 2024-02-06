using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arrow : Projectile {


    public override void Move()
    {
        transform.rotation = Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2((target.transform.position.y - transform.position.y + 0.5f), (target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(0, 0.5f, 0), baseSpeed * Time.deltaTime);
        transform.position += new Vector3(0, 0, -4f);
    }

    public override void Hit()
    {
        target.gameObject.GetComponent<Monster>().lastHiter = transform.parent.gameObject;
        target.gameObject.GetComponent<Defense>().DamageCalc(damage, (int)transform.parent.gameObject.GetComponent<TowerProperties>().damageType, dmgAmp, armorPen);

        //towerProp.Proc(target); // proc auras

        if (isCrit)
            lootManager.GetComponent<Loot>().Crit(target.gameObject.GetComponent<Defense>().damage, transform.position); //apply crit notifier

        if (knockBack)
            target.gameObject.GetComponent<Monster>().Knockback(knockbackX, knockbackY); // apply Knockback
        if (canChain) Chain();
    }

    private void Chain()
    {
        chainedtargets.Add(target);
        int count1 = 0;
        int count2 = 0;

        targets = Physics2D.OverlapCircleAll(transform.position, 2f).ToList();
        if (targets.Count == 0) return;
        targets.Sort((a, b) => a.gameObject.transform.position.x.CompareTo(b.transform.position.x));

        for (int i = 0; i < targets.Count; i++)
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
                    target = targets[i].gameObject;
                    break;
                }
            }
        }
        if (count2 == 0) Destroy(gameObject);
    }
}


