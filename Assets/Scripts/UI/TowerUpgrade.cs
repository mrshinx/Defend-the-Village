using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour {

    GameObject tower;
    GameObject towerInfuse;
    GameObject towerSpawner;
    float EXP;
    float level;

	// Use this for initialization
	void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Infuse(int index)
    {
        
            tower = TowerSelector.currentTarget;
            if (!tower.GetComponent<TowerProperties>().upgrade)
            {
                EXP = tower.GetComponent<TowerProperties>().EXP;
                level = tower.GetComponent<TowerProperties>().level;
                towerSpawner = tower.transform.parent.gameObject;

                if (index == 0) towerInfuse = tower.GetComponent<TowerProperties>().infuseShadow;
                if (index == 1) towerInfuse = tower.GetComponent<TowerProperties>().infuseFire;

            if (towerInfuse != null)
            {
                Destroy(tower);

                GameObject newTower = Instantiate(towerInfuse, towerSpawner.transform.position + new Vector3(0, 0, -1), Quaternion.identity) as GameObject;
                newTower.transform.parent = towerSpawner.transform;
                newTower.GetComponent<TowerProperties>().Initialization(level, EXP);
            }
            }
    }
}
