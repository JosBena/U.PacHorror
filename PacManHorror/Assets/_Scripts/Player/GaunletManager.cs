using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaunletManager : MonoBehaviour
{
    public static GaunletManager instance;
    Upgrades gaunletMagnetScriptable, gaunletMiniMapScriptable, gaunletStimScriptable;
    
    public enum UpgradeList{ 
        MAGNET, MINIMAP, STIM
    }
    public void Start()
    {
        
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        //set Magnet
        Scriptables.Magnet.SetPrefab(GameObject.Find(Scriptables.Magnet.name));
        Scriptables.Minimap.SetPrefab(GameObject.Find(Scriptables.Minimap.name));
        Scriptables.Stim.SetPrefab(GameObject.Find(Scriptables.Stim.name));
    }

    public void ChangeItemActivity(UpgradeList upgradeToEnable, bool state)
    {
        switch (upgradeToEnable)
        {
            case UpgradeList.MAGNET:
                Scriptables.Magnet.ChangeObjectActivity(state);
                break;
            case UpgradeList.MINIMAP:
                Scriptables.Minimap.ChangeObjectActivity(state);
                break;
            case UpgradeList.STIM:
                Scriptables.Stim.ChangeObjectActivity(state);
                break;
            default:
                Debug.LogError("Upgrade type invalid!");
                break;
        }
    }
}
