using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFire : MonoBehaviour {

    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject firePos;
    bool shootDone = false;
    bool active = false;
    int layerMask = 1 << 8;
    Collider2D[] targets;
    GameObject target;
    GameObject arrow;

    void Start()
    {
        StartCoroutine(Active());
    }

    // Update is called once per frame
    void Update () {
        if (active)
        {
            targets = Physics2D.OverlapCircleAll(transform.position, 4f, layerMask);
            if (targets != null)
                target = targets[0].gameObject;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].gameObject.transform.position.x < target.transform.position.x)
                    target = targets[i].gameObject;
            }
            if ((target.CompareTag("Monster")) && (target != null) && (shootDone == false))
            {
                shootDone = true;
                StartCoroutine(Fire());
            }
        }

    }

    IEnumerator Fire()
    {
        if (target != null)
        {
            arrow = Instantiate(ballPrefab, firePos.transform.position, firePos.transform.rotation);
            arrow.transform.parent = gameObject.transform;
            arrow.GetComponent<BallMove>().target = target;
        }
        yield return new WaitForSeconds(gameObject.GetComponent<TowerProperties>().attackSpeed);
       
        shootDone = false;
    }

    IEnumerator Active()
    {
        yield return new WaitForSeconds(1);
        active = true;
    }


}
