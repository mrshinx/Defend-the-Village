using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
public enum Rarity
{
    None, // 0
    Common, // 1
    Uncommon, // 2
    Rare, // 3
    Exceptional, // 4
    Elite, // 5
    Champion, // 6
    Master, // 7
    Emperor, // 8
    Legend, // 9
}

public class EnemyRarity
{
    public Rarity rarity;
    public List<int> magnitude; // Life // Defense // Damage

    public EnemyRarity(Rarity _rarity)
    {
        rarity = _rarity;
        magnitude = RarityDatabase.rarityModifierDatabase[rarity];
    }

    public EnemyRarity()
    {
        int i = GetWeightIndex();
        System.Random random = new System.Random();
        int roll = random.Next(0, 100);
        foreach (KeyValuePair<Rarity, List<int>> item in RarityDatabase.rarityWeightDatabase)
        {
            if (roll > item.Value[i] - 1)
            {
                roll -= item.Value[i];
                continue;
            }
            else
            {
                rarity = item.Key;
                break;
            }
        }

        magnitude = RarityDatabase.rarityModifierDatabase[rarity];
    }

    public int GetWeightIndex()
    {
        return Mathf.Clamp(WaveController.GetWave()/20,0,7);
    }
}

public static class RarityDatabase
{
    // This contains the stat increase magnitude for each rarity
    public static Dictionary<Rarity, List<int>> rarityModifierDatabase = new Dictionary<Rarity, List<int>>()
        {
            {Rarity.Common, new List<int>
            {
                0,0,0
            } },
            {Rarity.Uncommon, new List<int>
            {
                20,20,20
            } },
            {Rarity.Rare, new List<int>
            {
                40,40,40
            } },

            {Rarity.Exceptional, new List<int>
            {
                60,60,60
            } },

            {Rarity.Elite, new List<int>
            {
                80,80,80
            } },

            {Rarity.Champion, new List<int>
            {
                100,100,100
            } },

            {Rarity.Master, new List<int>
            {
                120,120,120
            } },
            {Rarity.Emperor, new List<int>
            {
                140,140,140
            } },

            {Rarity.Legend, new List<int>
            {
                160,160,160
            } },
        };
    // This contains the weight for rolling each rarity
    public static Dictionary<Rarity, List<int>> rarityWeightDatabase = new Dictionary<Rarity, List<int>>()
        {
            // Life // Defense // Damage
            {Rarity.Common, new List<int>
            {
                50, 40, 35, 30, 20, 10, 10, 10
            } },
            {Rarity.Uncommon, new List<int>
            {
                30, 30, 30, 25, 15, 10, 10, 10
            } },
            {Rarity.Rare, new List<int>
            {
                10, 10, 10, 15, 20, 15, 13, 13
            } },

            {Rarity.Exceptional, new List<int>
            {
                10, 10, 10, 15, 20, 20, 18, 15
            } },

            {Rarity.Elite, new List<int>
            {
                0, 10, 10, 10, 15, 22, 22, 17
            } },

            {Rarity.Champion, new List<int>
            {
                0, 0, 5, 5, 5, 11, 13, 15
            } },

            {Rarity.Master, new List<int>
            {
                0, 0, 0, 5, 5, 11, 13, 15
            } },
            {Rarity.Emperor, new List<int>
            {
                0, 0, 0, 0, 0, 1, 1, 3
            } },

            {Rarity.Legend, new List<int>
            {
                0, 0, 0, 0, 0, 0, 0, 2
            } },
        };
}
