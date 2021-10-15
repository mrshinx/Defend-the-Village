using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFire : MonoBehaviour {

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject firePos;
    [SerializeField] AudioClip[] fireSound;
    bool shootDone = true;
    int layerMask = 1 << 8;
    int soundIndex;
    Collider2D[] targets;
    GameObject target;
    GameObject arrow;
    public float range;
 
    void Start()
    {

    }

    

    // Update is called once per frame
    void Update() {

        targets = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        if (targets != null)
        {
            target = targets[0].gameObject;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.transform.position.x < target.transform.position.x)
                    target = targets[i].gameObject;
            }
            if ((target.CompareTag("Monster")) && (target != null) && (shootDone == true)) //&& (gameObject.GetComponent<Mana>().currentMana >= 25))
            {
                // gameObject.GetComponent<Mana>().currentMana -= 25;
                StartCoroutine(Fire());
            }
        }


    }

    IEnumerator Fire()
    {
        shootDone = false;
        if (target != null)
        {

            soundIndex = UnityEngine.Random.Range(0, 2);
            if (soundIndex == 0) AudioSource.PlayClipAtPoint(fireSound[0], Camera.main.transform.position, 0.08f);
            else AudioSource.PlayClipAtPoint(fireSound[soundIndex], Camera.main.transform.position, 1f);
            arrow = Instantiate(arrowPrefab, firePos.transform.position, Quaternion.Euler(0.0F, 0.0F, Mathf.Atan2((target.transform.position.y - transform.position.y + 0.5f), (target.transform.position.x - transform.position.x)) * Mathf.Rad2Deg));
            arrow.transform.parent = gameObject.transform;
            arrow.GetComponent<ArrowMove>().target = target;
            if (target.transform.position.x > transform.position.x) arrow.GetComponent<ArrowMove>().knockBack = true;
        }
        yield return new WaitForSeconds(gameObject.GetComponent<TowerProperties>().attackSpeed);
        shootDone = true;
    }
}
