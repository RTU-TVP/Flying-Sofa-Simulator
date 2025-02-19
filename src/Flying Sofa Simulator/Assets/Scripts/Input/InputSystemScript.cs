using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class InputSystemScript : MonoBehaviour
{
    [SerializeField] private Button gamepadButton, oculusButton;

    private UnityEngine.XR.InputDevice rightController;
    private UnityEngine.XR.InputDevice leftController;

    private Gamepad currentGamePad;
    private bool isGamepadInitialized = false;

    [SerializeField] private InputType inputType = InputType.oculusControllers;

    private void Start()
    {
        if (PlayerPrefs.HasKey("InputType"))
        {
            inputType = (InputType)PlayerPrefs.GetInt("InputType");
        }

        if (gamepadButton)
            gamepadButton.onClick.AddListener(() =>
            {
                gamepadButton.GetComponent<Image>().color = Color.green;
                oculusButton.GetComponent<Image>().color = Color.white;
                SetInput(InputType.gamepad);
            });

        if (oculusButton)
            oculusButton.onClick.AddListener(() =>
            {
                gamepadButton.GetComponent<Image>().color = Color.white;
                oculusButton.GetComponent<Image>().color = Color.green;
                SetInput(InputType.oculusControllers);
            });

        switch (inputType)
        {
            case InputType.gamepad when gamepadButton:
                gamepadButton.GetComponent<Image>().color = Color.green;
                break;
            case InputType.oculusControllers when oculusButton:
                oculusButton.GetComponent<Image>().color = Color.green;
                break;
        }

        void SetInput(InputType inputType)
        {
            this.inputType = inputType;
            PlayerPrefs.SetInt("InputType", (int)inputType);
        }
    }

    private void Update()
    {
        switch (inputType)
        {
            case InputType.gamepad when isGamepadInitialized:
                GamepadInput();
                break;
            case InputType.gamepad:
                InitializeGamepad();
                break;
            case InputType.oculusControllers:
                UpdateInputForOculusControllers();
                break;
        }
    }

    // For Gamepad
    private void InitializeGamepad()
    {
        if (Gamepad.all.Count > 0)
        {
            currentGamePad = Gamepad.all[0];
            isGamepadInitialized = true;
        }
    }

    private void GamepadInput()
    {
        ControllerInputValues.leftStickValue = currentGamePad.leftStick.value;
        ControllerInputValues.rightStickValue = currentGamePad.rightStick.value;
        ControllerInputValues.leftTrigger = currentGamePad.leftShoulder.value;
        ControllerInputValues.rightTrigger = currentGamePad.rightShoulder.value;
    }


    // For Oculus Controllers
    private void UpdateInputForOculusControllers()
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

    private void InitializeDevice(InputDeviceCharacteristics characteristics, ref UnityEngine.XR.InputDevice device)
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        if (devices.Count > 0)
        {
            device = devices[0];
        }
    }

    private void InitializeDevices()
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

    private enum InputType
    {
        oculusControllers,
        gamepad
    }
}