using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofBoundsScript : MonoBehaviour
{
    public GameObject outOfBoundCanvas;
    private void Start()
    {
        if (outOfBoundCanvas == null) outOfBoundCanvas = GameObject.Find("OutOfBoundScreen");
        outOfBoundCanvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name=="XR Origin")
        {
            if (outOfBoundCanvas != null)
            {
                outOfBoundCanvas.SetActive(false);
                Debug.Log("Player is in bounds");
            }
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "XR Origin")
        {
            outOfBoundCanvas.SetActive(true);
            Debug.Log("Player is out of bounds");
        }
        
    }
}
