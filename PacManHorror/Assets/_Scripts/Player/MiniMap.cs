using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour, IEUpgradable
{
    [SerializeField]

    private Camera MiniMapCam;

    private void Start()
    {
        
        MiniMapCam = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        MiniMapCam.orthographicSize = Scriptables.Minimap.UpgradableVariable;
    }

    //Upgrades Range
    public void UpgradeAbility()
    {
        int upgradeAmount = CollectibleManager.instance.DecreaseCollectibleMeta(Scriptables.Minimap.costsMeta, CollectibleManager.Items.MINIMAP);
        if (upgradeAmount != 0)
        {
            MiniMapCam.orthographicSize = upgradeAmount;
        }
        
    }
}
