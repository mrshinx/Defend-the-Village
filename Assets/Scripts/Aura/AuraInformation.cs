using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraInformation : MonoBehaviour {

    public int AuraID;
    public string AuraName;
    public float baseDuration;
    public float duration;
    public float baseChance;
    public float chance;
    bool onTarget = false;
    public bool stackable = false;
    public int stacks=1;
    public int maxStack;
    public GameObject AuraSource;
    public Sprite sprite;
    AuraInformation cacheChildAura;

    void Start () {

        if (transform.parent.gameObject != null)
        {
            if ((transform.parent.gameObject.CompareTag("Monster")))
            {
                onTarget = true;
            }

            if ((transform.parent.gameObject.CompareTag("Tower")))
            {
                duration = baseDuration;
                AuraSource = transform.parent.gameObject;
                AuraSource.GetComponent<TowerProperties>().Proc += ApplyAura;
                chance = baseChance;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        if (onTarget)
        {
            if (duration > 0) duration -= Time.deltaTime;
            if (duration <= 0) Destroy(gameObject, 0.01f);
        }

    }

    public void ResetAuraDuration()
    {
        duration = baseDuration;
    }

    private void ApplyAura(GameObject target)
    {
        int debuffCount = 0;
        float roll = Random.Range(0f, 1f);
            if (roll <= chance)
            {
                foreach (Transform child in target.transform)
                {
                    if (cacheChildAura = child.gameObject.GetComponent<AuraInformation>())
                    {
                        if ((cacheChildAura.AuraID == AuraID) && (cacheChildAura.AuraSource == AuraSource))
                        {
                            cacheChildAura.ResetAuraDuration();
                            debuffCount++;
                        if ((stackable)&&(cacheChildAura.stacks < maxStack))
                        {
                            cacheChildAura.stacks += 1;
                        }
                        }
                    }
                }
                if (debuffCount == 0)
                {
                    GameObject newDebuff = Instantiate(gameObject, target.transform.position, Quaternion.identity);
                    newDebuff.transform.SetParent(target.transform);
                }
            }
    }

}
