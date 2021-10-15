using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

[CreateAssetMenu(fileName = "New SaveLoad Object", menuName = "Manager/SaveLoad Object")]
public class SaveLoadSystem : ScriptableObject, ISerializationCallbackReceiver
{

    public List<SaveInfo> SaveTowerInfo = new List<SaveInfo>();
    public string savePath;
    public InventoryObject inventoryObject;
    public List<InventorySlot> Container = new List<InventorySlot>();

    [ContextMenu("Save")]
    public void Save()
    {
        GameObject[] tempvar = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject temp in tempvar)
        {
            SaveTowerInfo.Add(new SaveInfo(temp.GetComponent<TowerProperties>().TowerIndex, temp.GetComponent<TowerProperties>().spawnerName, 
                temp.GetComponent<TowerProperties>().level, temp.GetComponent<TowerProperties>().EXP, temp.GetComponent<TowerProperties>().activeResearch));
        }

        Container = inventoryObject.Container;

        string saveData = JsonUtility.ToJson(this, true);
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        BinaryFormatter BF = new BinaryFormatter();
        BF.Serialize(file, saveData);
        file.Close();

    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            BinaryFormatter BF = new BinaryFormatter();
            JsonUtility.FromJsonOverwrite(BF.Deserialize(file).ToString(), this);
            Debug.Log(Application.persistentDataPath);
            file.Close();

            for (int i = 0; i < SaveTowerInfo.Count; i++)
            {
                GameObject spawner = GameObject.Find(SaveTowerInfo[i].spawnerName);
                spawner.GetComponent<TowerSpawn>().SpawnTower(SaveTowerInfo[i].towerIndex);
                spawner.transform.GetChild(0).GetComponent<TowerProperties>().Initialization(SaveTowerInfo[i].level, SaveTowerInfo[i].EXP);
                spawner.transform.GetChild(0).GetComponent<TowerProperties>().activeResearch = SaveTowerInfo[i].activeResearch;
            }

            inventoryObject.Container = this.Container;
        }

        

    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        SaveTowerInfo.Clear();
        inventoryObject.Container = new List<InventorySlot>();
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        
    }

   
    private void OnEnable()
    {
        SaveTowerInfo.Clear();
    }
}

[System.Serializable]
public class SaveInfo
{
    public int towerIndex;
    public string spawnerName;
    public float level;
    public float EXP;
    public List<GameObject> activeResearch;

    public SaveInfo(int index, string name, float _level, float _EXP, List<GameObject> _activeResearch)
    {
        towerIndex = index;
        spawnerName = name;
        level = _level;
        EXP = _EXP;
        activeResearch = _activeResearch;
    }

}