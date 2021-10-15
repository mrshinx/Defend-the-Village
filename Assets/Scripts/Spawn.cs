using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    bool spawn = true;
    public bool startSpawn = false;
    [SerializeField] float maxDelay;
    [SerializeField] float minDelay;
    [SerializeField] GameObject monster;
    public float monsterAmount;
    float baseMonsterAmount;
    List<GameObject> remainingMonster = new List<GameObject>();

	// Use this for initialization
	IEnumerator Start () {
        monsterAmount = WaveController.currentMonsterAmount;
        WaveController.currentAliveMonster += monsterAmount;
        while (spawn)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minDelay, maxDelay));
            if(monsterAmount >0)
            SpawnMonster();
        }
	}

    private void SpawnMonster()
    {
        monster.transform.localScale = new Vector2(-1f, 1f);
        GameObject tempMonster = Instantiate(monster, transform.position, transform.rotation);
        remainingMonster.Add(tempMonster);
        monsterAmount -= 1;
    }

    // Update is called once per frame
    void Update () {
        baseMonsterAmount = WaveController.currentMonsterAmount;
        foreach (GameObject Monster in remainingMonster)
        {
            if (Monster == null)
            {
                remainingMonster.Remove(Monster);
                WaveController.currentAliveMonster -= 1;
            }
        }
        if (startSpawn)
        {
            monsterAmount = baseMonsterAmount;
            WaveController.currentAliveMonster += monsterAmount;
            startSpawn = false;
        }
	}
}
