using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InputSystemScript : MonoBehaviour
{
    InputDevice rightController;
    InputDevice leftController;
    private void Start()
    {

    }
    private void Update()
    {
        InitializeDevices();
        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftStickValue);
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightStickValue);
        leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTrigger);
        rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTrigger);
        rightController.TryGetFeatureValue(CommonUsages.grip,out float rightPrimaryButton);
        leftController.TryGetFeatureValue(CommonUsages.grip, out float leftPrimaryButton);
        ControllerInputValues.leftStickValue = leftStickValue;
        ControllerInputValues.rightStickValue = rightStickValue;
        ControllerInputValues.leftTrigger = leftTrigger;
        ControllerInputValues.rightTrigger = rightTrigger;
    }

    void InitializeDevice(InputDeviceCharacteristics characteristics, ref InputDevice device)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        if(devices.Count > 0) { device = devices[0]; }
    }
    void InitializeDevices()
    {
        if (!rightController.isValid)
        {
            InitializeDevice(InputDeviceCharacteristics.Right, ref rightController);
        }
        if (!leftController.isValid)
        {
            InitializeDevice(InputDeviceCharacteristics.Left, ref leftController);
        }
    }
}
