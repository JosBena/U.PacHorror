using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Coin : MonoBehaviour
{

    //Listen (observer pattern)
    public static event Action OnCoinCollected;
    private void OnTriggerEnter(Collider other) //On strigger stay for VR
    {
        //if (HandManager.instance.WasButtonPressed(HandManager.Buttons.TRIGGERBUTTON))
        //{
            if (other.CompareTag(PlayerTags.hands))
            {
                Destroy(gameObject);
                //Broadcast
                OnCoinCollected?.Invoke();
            }
        //}
    }
}
