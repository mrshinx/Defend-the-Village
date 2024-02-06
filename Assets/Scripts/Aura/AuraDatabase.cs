using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Aura Database", menuName = "Aura/Aura Database")]
public class AuraDatabase : ScriptableObject
{
    public AuraObject[] Auras;
    public Dictionary<int, AuraObject> GetAura = new Dictionary<int, AuraObject>();

    // Re-assign and reconstruct GetAura dict
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Auras.Length; i++)
        {
            Auras[i].auraId = i;
            GetAura.Add(i, Auras[i]);
        }
    }

    // Clear GetAura dict
    public void OnBeforeSerialize()
    {
        GetAura = new Dictionary<int, AuraObject>();
    }
}
