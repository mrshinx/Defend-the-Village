using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public static int currentWave;
    float baseEnemyAmount;
    public static float currentWaveEnemyAmount;
    public static float currentWaveRemainingEnemy;

    [SerializeField] GameObject Lane1;
    [SerializeField] GameObject Lane2;
    [SerializeField] GameObject Lane3;
    [SerializeField] GameObject Lane4;
    [SerializeField] GameObject Lane5;
    [SerializeField] GameObject Lane6;

    // Use this for initialization

    void Awake()
    {
        currentWave = 0;
        baseEnemyAmount = 3;
        currentWaveEnemyAmount = baseEnemyAmount;
    }

	// Update is called once per frame
	void Update () {
        if (currentWaveRemainingEnemy == 0) 
        {
            if (currentWave % 3  == 0)
            {
                RandomUpgrade[] randomUpgrades = new RandomUpgrade[3] { new RandomUpgrade(), new RandomUpgrade(), new RandomUpgrade() };
                TowerUpgrade towerUpgradeObj = GameObject.FindWithTag("Tower Selector").GetComponent<TowerUpgrade>();
                towerUpgradeObj.GenerateRandomUpgradeButtons(randomUpgrades);
            }

            currentWave += 1;
            currentWaveEnemyAmount = Mathf.Floor(baseEnemyAmount + currentWave / 2f);
            foreach (Transform lane in transform)
            {
                lane.gameObject.GetComponent<Spawn>().startSpawn = true;
            }
        }

        if (currentWave == 25)
        {
            Lane2.SetActive(true);
        }

        if (currentWave == 50)
        {
            Lane3.SetActive(true);
        }

        if (currentWave == 100)
        {
            Lane4.SetActive(true);
        }

        if (currentWave == 200)
        {
            Lane5.SetActive(true);
        }

        if (currentWave == 400)
        {
            Lane6.SetActive(true);
        }
    }

    public static int GetWave()
    {
        return currentWave;
    }

}
