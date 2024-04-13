using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShop : MonoBehaviour
{
    private ChangeScene sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("Scene Manager").GetComponent<ChangeScene>();
    }

    private void OnTriggerEnter(Collider other) //On strigger stay for VR
    {
        //if (HandManager.instance.WasButtonPressed(HandManager.Buttons.TRIGGERBUTTON))
        //{
        if (other.CompareTag(PlayerTags.hands))
        {
            sceneManager.changeScene("Map");
        }
        //}
    }
}
