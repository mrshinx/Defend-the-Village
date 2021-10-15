using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{

    public GameObject target;
    TowerProperties towerCache;
    GameObject tower;
    float baseSpeed;
    float aoe;
    float damage;
    float dmgAmp;
    float armorPen;
    float knockbackX;
    float knockbackY;
    float t = 0;
    public bool bezier = false;
    Vector3 direction;
    Vector2 ortho;
    Vector3 firePos;
    [SerializeField] GameObject explosionAnim;
    [SerializeField] float explosionTime;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] float volume;
    Collider2D[] targets;
    Rigidbody2D rb;
   
    void Start()
    {
        tower = transform.parent.gameObject;
        towerCache = tower.GetComponent<TowerProperties>();
        baseSpeed = towerCache.projectileSpeed;
        aoe = towerCache.aoe;
        damage = towerCache.damage;
        dmgAmp = towerCache.dmgAmp;
        armorPen = towerCache.armorPen;
        knockbackX = towerCache.knockbackX;
        knockbackY = towerCache.knockbackY;
        if (rb = gameObject.GetComponent<Rigidbody2D>()) ;
    }

    void Update()
    {
        if (target == null) Destroy(gameObject);
        else
        {
            if (rb != null)
            {
                direction = target.transform.position + new Vector3(0, 0.5f, 0) - transform.position;
                Vector2 dir2d = new Vector2(direction.x, direction.y);

                ortho.x = dir2d.x;
                ortho.y = -dir2d.y;

                rb.velocity = dir2d.normalized * 4 + ortho.normalized * 5 * Mathf.Sin(20 * Time.time);

            }
            else
            {
                if (!bezier)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(0, 0.5f, 0), baseSpeed * Time.deltaTime);
                    transform.position += new Vector3(0, 0, -4f);
                }
                else
                {
                    Vector2 startPoint = new Vector2(transform.position.x, transform.position.y);
                    Vector2 endPoint = new Vector2(target.transform.position.x, target.transform.position.y+0.5f);
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
                    t += 0.5f*Time.deltaTime;
                    if (t >= 1) t = 1;
                    transform.position = Mathf.Pow(1 - t, 3) * startPoint + 3 * Mathf.Pow(1 - t, 2) * t * startCurve + 3 * (1 - t) * Mathf.Pow(t, 2) * endCurve + Mathf.Pow(t, 3) * endPoint;
                }
            }
        }

    }
    private void FixedUpdate()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == target.gameObject.GetComponent<Collider2D>())
        {
            AoE();
        }
    }

    public void AoE()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, aoe);
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.tag == "Monster")
                {
                    targets[i].gameObject.GetComponent<Monster>().lastHiter = transform.parent.gameObject;
                    targets[i].gameObject.GetComponent<Defense>().DamageCalc(damage, (int)transform.parent.gameObject.GetComponent<TowerProperties>().damageType, dmgAmp, armorPen);
                    if (targets[i].transform.position.x > tower.transform.position.x) // check if knockback
                    targets[i].gameObject.GetComponent<Monster>().Knockback(knockbackX, knockbackY); // apply knockback
                }
            }
        }
        CharDeath.DeathAnim(explosionAnim , gameObject.transform, explosionTime, explosionSound, volume);
        Destroy(gameObject);
    }
}
