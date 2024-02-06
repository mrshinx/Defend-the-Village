using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Security.Policy;
using System.Linq;

public static class AuraManager
{
    public static float CalculateDamageOverTime(DamageOverTime debuff)
    {
        return debuff.tick / debuff.baseDuration * debuff.totalDamage * debuff.stack;
    }

    public static Aura CreateAuraInstance(AuraObject auraObj)
    {
        switch (auraObj.auraType)
        {
            case AuraObject.AuraType.DamageOvertime:
                return new DamageOverTime(auraObj as DamageOverTimeObject);
            case AuraObject.AuraType.StatModifier:
                return new StatModifier(auraObj as StatModifierObject);
            default:
                return new Aura(auraObj);
        }
    }
}
