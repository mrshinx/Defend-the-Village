using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using System.Linq;

public interface IAffectableByAura
{
    public void ApplyAura(Aura aura);

    public void HandleAuraEffect();
}
