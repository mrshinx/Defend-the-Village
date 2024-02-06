using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    ProjectileCount,
    ChainCount,
    AoE,
    RepeatChance
}

public class RandomUpgrade
{

    public UpgradeType type;
    public float magnitude;

    public RandomUpgrade()
    {
        Array values = Enum.GetValues(typeof(UpgradeType));
        System.Random random = new System.Random();
        type = (UpgradeType)values.GetValue(random.Next(values.Length));

        foreach (KeyValuePair<UpgradeType, float> item in RandomUpgradeDatabase.randomUpgradeDatabase)
        {
            if (type == item.Key)
            {
                magnitude = item.Value;
                break;
            }
        }

    }
}

public static class RandomUpgradeDatabase
{
    public static Dictionary<UpgradeType, float> randomUpgradeDatabase = new Dictionary<UpgradeType, float>()
        {
            {UpgradeType.ProjectileCount, 1f},
            {UpgradeType.ChainCount, 1f},
            {UpgradeType.AoE, 0.1f},
            {UpgradeType.RepeatChance, 0.5f},
        };
}