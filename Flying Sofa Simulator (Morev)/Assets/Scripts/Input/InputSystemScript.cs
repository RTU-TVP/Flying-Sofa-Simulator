using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR;
public class InputSystemScript : MonoBehaviour
{
    UnityEngine.XR.InputDevice rightController;
    UnityEngine.XR.InputDevice leftController;

    Gamepad currentGamePad;
    bool isGamepadInitialized = false;

    [SerializeField] InputType inputType;
    private void Update()
    {
        if(inputType == InputType.gamepad)
        {
            if (isGamepadInitialized) GamepadInput();
            else InitializeGamepad();
        }
        else UpdateInputForOculusControllers();
    }

    // For Gamepad
    void InitializeGamepad()
    {
        if (Gamepad.all.Count > 0)
        {
            currentGamePad = Gamepad.all[0];
            isGamepadInitialized = true;
        }
        //Debug.Log("Gamepad Initializing");
    }
    void GamepadInput()
    {
        ControllerInputValues.leftStickValue = currentGamePad.leftStick.value;
        ControllerInputValues.rightStickValue = currentGamePad.rightStick.value;
        ControllerInputValues.leftTrigger = currentGamePad.leftShoulder.value;
        ControllerInputValues.rightTrigger = currentGamePad.rightShoulder.value;

        //Debug.Log("Gamepad Working");
    }


    // For Oculus Controllers
    void UpdateInputForOculusControllers()
    {
        InitializeDevices();
        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 leftStickValue);
        rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 rightStickValue);
        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float leftTrigger);
        rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float rightTrigger);
        rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out float rightGrip);
        leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out float leftGrip);
        ControllerInputValues.leftStickValue = leftStickValue;
        ControllerInputValues.rightStickValue = rightStickValue;
        ControllerInputValues.leftTrigger = leftGrip;
        ControllerInputValues.rightTrigger = rightGrip;
    }
    void InitializeDevice(InputDeviceCharacteristics characteristics, ref UnityEngine.XR.InputDevice device)
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
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
    enum InputType {oculusControllers, gamepad}
}
