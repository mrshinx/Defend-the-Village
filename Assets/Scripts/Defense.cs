using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense : MonoBehaviour {

    public enum damageTypes
    {
        Pierce, //0
        Blunt, //1
        Cold, //2
        Lightning, //3
        Fire
    }

    public int baseArmor;
    public int armor;
    public float coldResist;
    public float lightningResist;
    public float fireResist;

    float pierceMulti = 1.3f;
    float bluntMulti = 0.85f;
    float frostMulti = 1f;
    float thunderMulti = 1f;
    float flameMulti = 1f;

    public float damage;
    float damageReduction;

	void Start () {

        armor = baseArmor;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageCalc(float rawDamage, int damageType, float dmgAmp, float armorPen)
    {
        damageReduction = (1f - (0.01f * (armor-armorPen)) / (0.9f + 0.05f * (armor - armorPen)));

        switch (damageType)
        {
            case 0:
                damage = dmgAmp * rawDamage * pierceMulti * damageReduction;
                break;
            case 1:
                damage = dmgAmp * rawDamage * bluntMulti * damageReduction;
                break;
            case 2:
                damage = dmgAmp * rawDamage * frostMulti * (1f-coldResist);
                break;
            case 3:
                damage = dmgAmp * rawDamage * thunderMulti * (1f - lightningResist);
                break;
            case 4:
                damage = dmgAmp * rawDamage * flameMulti * (1f - fireResist);
                break;
        }
        gameObject.GetComponent<Monster>().TakeDamage(damage);
    }


}
