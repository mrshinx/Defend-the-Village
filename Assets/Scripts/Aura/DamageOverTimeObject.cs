using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;


[CreateAssetMenu(fileName = "New DoT", menuName = "Aura/Damage Over Time")]
public class DamageOverTimeObject: AuraObject
{
    public float totalDamage;
    public float tick;
    [System.NonSerialized]
    public float currentTick = 0;
}

public class DamageOverTime : Aura
{
    public float totalDamage;
    public float tick;
    [System.NonSerialized]
    public float currentTick = 0;

    public DamageOverTime(DamageOverTimeObject aura) : base(aura)
    {
        totalDamage = aura.totalDamage;
        tick = aura.tick;
        currentTick = aura.currentTick;
    }
}
