using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class Stim : MonoBehaviour, IEUpgradable
{

    private ActionBasedContinuousMoveProvider continousMovement;
    
    void Start()
    {
        continousMovement = GameObject.Find("/SetUp/Locomotion System").GetComponent<ActionBasedContinuousMoveProvider>();
    }
    public void UpgradeAbility()
    {

        int upgradeAmount = CollectibleManager.instance.DecreaseCollectibleMeta(Scriptables.Stim.costsMeta, CollectibleManager.Items.STIM);
        if (upgradeAmount != 0)
        {
            continousMovement.moveSpeed = Scriptables.Stim.UpgradableVariable;
        }

    }
}
