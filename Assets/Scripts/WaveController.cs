using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour {

    public static float currentWave;
    float baseMonsterAmount;
    public static float currentMonsterAmount;
    public static float currentAliveMonster;

    [SerializeField] GameObject Lane1;
    [SerializeField] GameObject Lane2;
    [SerializeField] GameObject Lane3;
    [SerializeField] GameObject Lane4;
    [SerializeField] GameObject Lane5;
    [SerializeField] GameObject Lane6;

    // Use this for initialization

    void Awake()
    {
        currentWave = 1;
        baseMonsterAmount = 3;
        currentMonsterAmount = baseMonsterAmount;
    }

    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (currentAliveMonster == 0) 
            {
                currentWave += 1;
                currentMonsterAmount = Mathf.Floor(baseMonsterAmount + currentWave / 2f);
                foreach (Transform lane in transform)
                {
                    lane.gameObject.GetComponent<Spawn>().startSpawn = true;
                }
            }

        if (currentWave == 25)
            {
                Lane2.SetActive(true);
                currentAliveMonster -= currentMonsterAmount;
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

    public static float GetWave()
    {
        return currentWave;
    }

}
