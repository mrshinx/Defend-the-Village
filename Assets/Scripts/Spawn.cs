using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public bool startSpawn = false;
    [SerializeField] float maxDelay;
    [SerializeField] float minDelay;
    [SerializeField] GameObject enemyPrefab;
    float amountToSpawn;
    List<GameObject> spawnedEnemy = new List<GameObject>();

	// Use this for initialization
    private IEnumerator SpawnMonster()
    {
        enemyPrefab.transform.localScale = new Vector2(-1f, 1f);
        while (amountToSpawn > 0)
        {
            GameObject tempEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            spawnedEnemy.Add(tempEnemy);
            amountToSpawn -= 1;
            yield return new WaitForSeconds(UnityEngine.Random.Range(minDelay, maxDelay));
        }
    }

    // Update is called once per frame
    void Update () 
    {
        // Check for spawn flag and start spawning enemies
        if (startSpawn)
        {
            startSpawn = false;
            amountToSpawn = WaveController.currentWaveEnemyAmount;
            WaveController.currentWaveRemainingEnemy += amountToSpawn;
            StartCoroutine(SpawnMonster());
        }

        // Check for dead enemies
        foreach (GameObject enemy in spawnedEnemy)
        {
            if (enemy == null)
            {
                spawnedEnemy.Remove(enemy);
                WaveController.currentWaveRemainingEnemy -= 1;
            }
        }
	}
}
