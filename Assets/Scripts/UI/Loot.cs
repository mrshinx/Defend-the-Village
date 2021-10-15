using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    [SerializeField] Transform popupLoot;
    [SerializeField] Transform popupCrit;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Bounty(int bounty, Vector3 pos)
    {
        Transform newPopup = Instantiate(popupLoot, pos, Quaternion.identity);
        newPopup.GetComponent<PopupContent>().isBounty = true;
        newPopup.GetComponent<PopupContent>().Setup(bounty);
        Resource.gold += bounty;
    }

    public void Crit(float damage, Vector3 pos)
    {
        Transform newPopup = Instantiate(popupCrit, pos, Quaternion.identity);
        newPopup.GetComponent<PopupContent>().isCrit = true;
        newPopup.GetComponent<PopupContent>().Setup(damage);
    }

}
