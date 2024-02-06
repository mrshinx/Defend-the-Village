using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TowerUpgrade : MonoBehaviour {

    public GameObject buttonPrefab;
    public GameObject randomUpgradeMenu;

    [System.NonSerialized] public static List<GameObject> currentTowerList = new List<GameObject>();
    [System.NonSerialized] public static float chainCount;
    [System.NonSerialized] public static float projectileCount = 1f;
    [System.NonSerialized] public static float repeatChance;

    // Add infuse buttons based on the amount of available infuse for target tower
    public void GenerateInfuseButtons()
    {
        List<GameObject> infuseList = new List<GameObject>();
        GameObject tower = TowerSelector.currentTarget;
        infuseList = tower.GetComponent<TowerProperties>().infuseList;
        GameObject infuseMenu = GameObject.FindWithTag("Tower Infuse Menu");

        foreach (GameObject infuse in infuseList) 
        { 
            GameObject go = Instantiate(buttonPrefab, infuseMenu.transform);
            go.transform.GetChild(0).gameObject.GetComponent<Text>().text = infuse.name;
            go.GetComponent<Button>().onClick.AddListener(() => Infuse(infuse.name));
            go.GetComponent<Button>().onClick.AddListener(() => ClearInfuseButtons());
        }
    }

    public void ClearInfuseButtons()
    {
        GameObject infuseMenu = GameObject.FindWithTag("Tower Infuse Menu");

        foreach (Transform child in infuseMenu.transform)
        {
            if (child.gameObject.name != "Exit Button") Destroy(child.gameObject);
        }

        infuseMenu.SetActive(false);
    }

    public void Infuse(string infuseName)
    {
        GameObject tower = TowerSelector.currentTarget;
        TowerProperties propertiesCache = tower.GetComponent<TowerProperties>();

        if (!propertiesCache.upgrade)
        {
            float EXP = propertiesCache.EXP;
            float level = propertiesCache.level;
            GameObject towerSpawner = tower.transform.parent.gameObject;

            GameObject towerInfuse = propertiesCache.infuseList.Find(go => go.name == infuseName);
            if (towerInfuse != null)
            {
                currentTowerList.Remove(tower);
                Destroy(tower);

                GameObject newTower = Instantiate(towerInfuse, towerSpawner.transform.position + new Vector3(0, 0, -1), Quaternion.identity) as GameObject;
                newTower.transform.parent = towerSpawner.transform;
                newTower.GetComponent<TowerProperties>().Initialization(level, EXP);
                currentTowerList.Add(tower);
            }
        }
    }

    // Add random upgrade buttons
    public void GenerateRandomUpgradeButtons(RandomUpgrade[] randomUpgrades)
    {
        Time.timeScale = 0;

        randomUpgradeMenu.SetActive(true);
        foreach (RandomUpgrade upgrade in randomUpgrades)
        {
            GameObject go = Instantiate(buttonPrefab, randomUpgradeMenu.transform);
            go.transform.GetChild(0).gameObject.GetComponent<Text>().text = upgrade.type.ToString();
            go.GetComponent<Button>().onClick.AddListener(() => Upgrade(upgrade));
            go.GetComponent<Button>().onClick.AddListener(() => ClearRandomUpgradeButtons());
        }
    }

    public void Upgrade(RandomUpgrade upgrade)
    {
        switch (upgrade.type)
        {
            case UpgradeType.ProjectileCount:
                projectileCount += upgrade.magnitude;
                break;
            case UpgradeType.ChainCount:
                chainCount += upgrade.magnitude;
                break;
            case UpgradeType.RepeatChance:
                repeatChance += upgrade.magnitude;
                break;
        }

        foreach(GameObject tower in currentTowerList)
        {
            tower.GetComponent<TowerProperties>().CalculateUpgrade();   
        }

        Time.timeScale = 1;
    }

    public void ClearRandomUpgradeButtons()
    {
        foreach (Transform child in randomUpgradeMenu.transform)
        {
            if (child.gameObject.name != "Exit Button") Destroy(child.gameObject);
        }

        randomUpgradeMenu.SetActive(false);
    }
}
