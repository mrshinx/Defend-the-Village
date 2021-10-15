using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIndexer : MonoBehaviour {

    public static int towerIndex;
    public static bool buildAllowed;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(1))
        {
            if (buildAllowed)
            {
                buildAllowed = false;
            }
        }
    }

    public void TowerIndex(int index)
    {
        towerIndex = index;
    }

    public void BuildAllowed(bool state)
    {
        buildAllowed = state;
    }
}
