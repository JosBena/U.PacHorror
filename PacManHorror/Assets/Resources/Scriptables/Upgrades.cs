using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="DefaultUpgrade",menuName ="UpgradeScriptable")]
public class Upgrades : ScriptableObject
{
    public new string name;

    public bool active;

    public int UpgradableVariable=5, rateOfChange=5;

    public int costsMeta, costCoin;

    private GameObject prefab;

    public void ChangeObjectActivity(bool state)
    {
        if (prefab == null)
        {
            Debug.Log($"prefab {name} no set");
            return;
        }
        
        active = state;
        prefab.SetActive(active);
    }

    public void SetPrefab(GameObject prefab)
    {
        if (prefab == null) return;
        this.prefab = prefab;
        this.prefab.SetActive(active);
    }

    public int UpgradeVariable()
    {
        return UpgradableVariable += rateOfChange;
    }
}