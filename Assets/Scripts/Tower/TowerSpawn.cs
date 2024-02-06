using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour {

    [SerializeField] GameObject[] towerList;
    public bool haveTower = false;
    public Vector3 basePos;
    public GameObject tower;

    private void Awake()
    {
        
        
    }

    void Start () {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        basePos = transform.position;
    }
	
	void Update () {
        if ((!haveTower) && (TowerIndexer.buildAllowed))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        }
        if ((haveTower) || (!TowerIndexer.buildAllowed))
            transform.position = basePos;
    }

    private void OnMouseDown()
    {
        if ((haveTower == false)&&(TowerIndexer.buildAllowed)&&(towerList[TowerIndexer.towerIndex].GetComponent<TowerProperties>().cost<=Resource.gold))
        {
            Resource.gold -= towerList[TowerIndexer.towerIndex].GetComponent<TowerProperties>().cost;
            haveTower = true;
            SpawnTower(TowerIndexer.towerIndex);
        }
    }

    public void SpawnTower(int towerIndex)
    {
        tower = Instantiate(towerList[towerIndex], transform.position + new Vector3(0, 0, -1), Quaternion.identity);
        tower.transform.parent = gameObject.transform;
        TowerUpgrade.currentTowerList.Add(tower);
    }

    public void OnMouseOver()
    {
        if ((haveTower == false) && (TowerIndexer.buildAllowed))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

}





