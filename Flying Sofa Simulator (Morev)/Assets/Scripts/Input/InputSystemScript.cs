using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputSystemScript : MonoBehaviour
{
    InputDevice rightController;
    InputDevice leftController;
    private void Start()
    {
        List<InputDevice> rightControllerList = new List<InputDevice>();
        List<InputDevice> leftControllerList = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, rightControllerList);
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, leftControllerList);
        if(rightControllerList.Count > 0)
        {
            rightController = rightControllerList[0];
        }
        if(leftControllerList.Count > 0)
        {
            leftController = leftControllerList[0];
        }
    }
    private void Update()
    {
        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftStickValue);
        leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTrigger);
        rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTrigger);
        rightController.TryGetFeatureValue(CommonUsages.grip,out float rightPrimaryButton);
        leftController.TryGetFeatureValue(CommonUsages.grip, out float leftPrimaryButton);
        ControllerInputValues.leftStickValue = leftStickValue;
        ControllerInputValues.leftTrigger = leftTrigger;
        ControllerInputValues.rightTrigger = rightTrigger;
        ControllerInputValues.rightGrip = rightPrimaryButton;
        ControllerInputValues.leftGrip = leftPrimaryButton;
    }
}
