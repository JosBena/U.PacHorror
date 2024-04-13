using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour, IEUpgradable
{
    public void UpgradeAbility()
    {

        int upgradeAmount = CollectibleManager.instance.DecreaseCollectibleMeta(Scriptables.Magnet.costsMeta, CollectibleManager.Items.MAGNET);
        if (upgradeAmount != 0)
        {
            
        }
    }

}
