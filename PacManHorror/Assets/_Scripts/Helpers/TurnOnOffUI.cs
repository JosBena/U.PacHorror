using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOffUI : MonoBehaviour
{
    public List<GameObject> UIList;

    public bool isActive = true;

    public void TurnOnOff()
    {
        foreach(GameObject item in UIList)
        {
           item.SetActive(isActive);
        }
        isActive = !isActive;
    }
}
