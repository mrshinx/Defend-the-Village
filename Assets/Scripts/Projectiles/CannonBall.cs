using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonBall : Projectile
{

    GameObject tower;
    float aoe;
    float t = 0;
    public bool bezier = false;
    [SerializeField] GameObject explosionAnim;
    [SerializeField] float explosionTime;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float volume;
   
    public override void Start()
    {
        base.Start();
        tower = transform.parent.gameObject;
        aoe = towerProp.aoe;
    }

    public override void Move()
    {

        if (!bezier)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position + new Vector3(0, 0.5f, 0), baseSpeed * Time.deltaTime);
            // transform.position += new Vector3(0, 0, -4f);
        }
        else
        {
            Vector2 startPoint = new Vector2(transform.position.x, transform.position.y);
            Vector2 endPoint = new Vector2(target.transform.position.x, target.transform.position.y + 0.5f);
            Vector2 startCurve;
            Vector2 endCurve;
            if (target.transform.position.x > tower.transform.position.x)
            {
                startCurve = new Vector2(startPoint.x + 0.2f, startPoint.y + 0.2f);
                endCurve = new Vector2(endPoint.x - 0.2f, endPoint.y + 0.2f);
            }
            else
            {
                startCurve = new Vector2(startPoint.x - 0.2f, startPoint.y + 0.2f);
                endCurve = new Vector2(endPoint.x + 0.2f, endPoint.y + 0.2f);
            }
            t += 0.5f * Time.deltaTime;
            if (t >= 1) t = 1;
            transform.position = Mathf.Pow(1 - t, 3) * startPoint + 3 * Mathf.Pow(1 - t, 2) * t * startCurve + 3 * (1 - t) * Mathf.Pow(t, 2) * endCurve + Mathf.Pow(t, 3) * endPoint;
        }
    }
    public override void Hit()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, aoe).ToList();
        if (targets.Count == 0) return;
        targets.Sort((a, b) => a.gameObject.transform.position.x.CompareTo(b.transform.position.x));

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].gameObject.tag == "Monster")
            {
                targets[i].gameObject.GetComponent<Monster>().lastHiter = transform.parent.gameObject;
                targets[i].gameObject.GetComponent<Defense>().DamageCalc(damage, (int)transform.parent.gameObject.GetComponent<TowerProperties>().damageType, dmgAmp, armorPen);
                if (targets[i].transform.position.x > tower.transform.position.x) // check if knockback
                targets[i].gameObject.GetComponent<Monster>().Knockback(knockbackX, knockbackY); // apply knockback
            }
        }

        AnimationManager.PlayDeathAnimation(explosionAnim , gameObject.transform, explosionTime, explosionSound, volume);
        Destroy(gameObject);
    }
}
