using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meta : MonoBehaviour
{
    //Listen (observer pattern)
    public static event Action<string> OnMetaCollected;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTags.hands))
        {
            Destroy(gameObject);
            //Broadcast
            OnMetaCollected("Scenes/Shop");
        }
    }
}
