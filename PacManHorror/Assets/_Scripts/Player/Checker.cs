using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scriptables.Magnet.SetPrefab(GameObject.Find(Scriptables.Magnet.name));
        Scriptables.Minimap.SetPrefab(GameObject.Find(Scriptables.Minimap.name));
        Scriptables.Stim.SetPrefab(GameObject.Find(Scriptables.Stim.name));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
