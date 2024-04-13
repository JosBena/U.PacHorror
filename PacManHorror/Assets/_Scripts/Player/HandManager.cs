using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandManager : MonoBehaviour
{
    //XR
    public enum Buttons
    {
        TRIGGERBUTTON, GRIPBUTTONBUTTON, MENUBUTTON, PRIMARYBUTTON, SECONDARYBUTTON
    }

    private InputDevice leftHand, rightHand;
    public bool leftHandPressed, rightHandPressed, triggerPressed;
    

    //singleton
    public static HandManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

       

        //leftHand = GameObject.Find("LeftHand Controller").GetComponent<InputDevice>();
        //rightHand = GameObject.Find("rightHand Controller").GetComponent<InputDevice>();
    }

    private void Start()
    {
        var leftHandList = new List<InputDevice>();
        var rightHandList = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerChar = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerChar = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

        InputDevices.GetDevicesWithCharacteristics(rightControllerChar, rightHandList);
        InputDevices.GetDevicesWithCharacteristics(leftControllerChar, leftHandList);

        foreach (var item in leftHandList)
        {
            Debug.Log($"Device found with name '{item.name}' and role '{item.characteristics}'");
            leftHand = item;
        }

        foreach (var item in rightHandList)
        {
            Debug.Log($"Device found with name '{item.name}' and role '{item.characteristics}'");
            rightHand = item;
        }
    }

    public bool WasTriggerPressed()
    {
        bool leftHandPressed = leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out leftHandPressed) && leftHandPressed;
        bool rightHandPressed = rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out rightHandPressed) && rightHandPressed;
        if (leftHandPressed||rightHandPressed)
        {
            print("Trigger Pressed");
            return triggerPressed = true;
        }else return triggerPressed = false;
    }

    public bool WasButtonPressed(Buttons button)
    {
        InputFeatureUsage<bool> buttons;
        switch (button)
        {
            case Buttons.TRIGGERBUTTON:
                buttons = CommonUsages.triggerButton;
                break;
            case Buttons.GRIPBUTTONBUTTON:
                buttons = CommonUsages.gripButton;
                break;
            case Buttons.MENUBUTTON:
                buttons = CommonUsages.menuButton;
                break;
            case Buttons.PRIMARYBUTTON:
                buttons = CommonUsages.primaryButton;
                break;
            case Buttons.SECONDARYBUTTON:
                buttons = CommonUsages.secondaryButton;
                break;
            default:
                print("button does not exist");
                return false;
        }
        bool leftHandPressed = leftHand.TryGetFeatureValue(buttons, out leftHandPressed) && leftHandPressed;
        bool rightHandPressed = rightHand.TryGetFeatureValue(buttons, out rightHandPressed) && rightHandPressed;
        if (leftHandPressed)
        {
            print("leftHand Pressed button");
            return true;
        }
        else if (rightHandPressed)
        {
            print("rightHand Pressed button");
            return true;
        }
        else return false;
    }

}
