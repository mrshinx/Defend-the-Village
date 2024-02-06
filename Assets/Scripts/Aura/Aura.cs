using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using static AuraObject;
using System;

public class AuraObject : ScriptableObject
{
    public enum AuraType
    {
        StatModifier,
        DamageOvertime
    }
    public AuraType auraType;
    public string auraName;
    public string auraDescription;
    public int auraId;
    public int maxStack = 1;
    [System.NonSerialized]
    public int stack = 1;
    public bool isPermanent = false;
    public float baseDuration;
    [System.NonSerialized]
    public float currentDuration;
    [System.NonSerialized]
    public GameObject source;

    public Aura CreateAura()
    {
        Aura newAura = new Aura(this);
        return newAura;
    }
}

public class Aura
{
    public AuraType auraType;
    public string auraName;
    public string auraDescription;
    public int auraId;
    public int maxStack = 1;
    public int stack = 1;
    public bool isPermanent = false;
    public float baseDuration;
    public float currentDuration;
    public GameObject source;

    public Aura(AuraObject aura)
    {
        auraType = aura.auraType;
        auraName = aura.auraName;
        auraDescription = aura.auraDescription;
        auraId = aura.auraId;
        maxStack = aura.maxStack;
        stack = aura.stack;
        isPermanent = aura.isPermanent;
        baseDuration = aura.baseDuration;
        currentDuration = aura.currentDuration;
        source = aura.source;
    }

}
