using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using static StatModifierObject;

[CreateAssetMenu(fileName = "New Stat Modifier Aura", menuName = "Aura/Stat Modifier")]
public class StatModifierObject : AuraObject
{
    public enum Stat
    {
        MoveSpeed,
        HP
    }
    public Stat statType;
    public float magnitude;
}

public class StatModifier: Aura
{
    public Stat statType;
    public float magnitude;

    public StatModifier(StatModifierObject aura) : base(aura)
    {
        statType = aura.statType;
        magnitude = aura.magnitude;
    }
}
