using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SofaMovement : MonoBehaviour
{
    [SerializeField] GameObject _sofaModelObject;
    [SerializeField] int _movingSpeed;
    [SerializeField] int _rotationSpeed;
    [SerializeField] int _pitchSoftness;
    void Start()
    {
        StartCoroutine(AngleChanger());
    }
    void FixedUpdate()
    {
        RotateInChosenDirection();
        MoveInChosenDirection();
        ChangeHigh();
    }
    void RotateInChosenDirection()
    {
        if(ControllerInputValues.rightTrigger != 0 || ControllerInputValues.leftTrigger != 0)
        {
            Vector3 currentRotationValue = transform.eulerAngles;
            currentRotationValue.y += (ControllerInputValues.rightTrigger - ControllerInputValues.leftTrigger)*Time.deltaTime * _rotationSpeed;
            transform.eulerAngles = currentRotationValue;
        }
    }
    void MoveInChosenDirection()
    {
        if(ControllerInputValues.leftStickValue != Vector2.zero)
        {
            transform.position = transform.position + new Vector3(transform.forward.x, 0, transform.forward.z) * Time.deltaTime * ControllerInputValues.leftStickValue.y * _movingSpeed;
            transform.position = transform.position + new Vector3(transform.right.x, 0, transform.right.z) * Time.deltaTime * ControllerInputValues.leftStickValue.x * _movingSpeed;
        }
    }
    void ChangeHigh()
    {
        if(ControllerInputValues.rightStickValue.y != 0)
        {
            Vector3 pos = transform.position;
            pos.y += ControllerInputValues.rightStickValue.y * Time.deltaTime;
            transform.position = pos;
        }
    }
    float GetXPitch()
    {
        float x = 0;
        x += ControllerInputValues.leftStickValue.y * 20;
        x -= ControllerInputValues.rightStickValue.y * 10;
        if(x > 30)
        {
            x = 30;
        }
        if(x < -30)
        {
            x = -30;
        }
        return x;
    }
    float GetZPitch()
    {
        float z = 0;
        z -= ControllerInputValues.leftStickValue.x * 20;
        z -= (ControllerInputValues.rightTrigger - ControllerInputValues.leftTrigger) * 10;
        if (z > 30)
        {
            z = 30;
        }
        if (z < -30)
        {
            z = -30;
        }
        return z;
    }
    IEnumerator AngleChanger()
    {
        Vector2 currentAngles = new Vector2(0, 0);
        Vector2 neededAngles = new Vector2(GetXPitch(), GetZPitch());
        while(true)
        {
            neededAngles = new Vector2(GetXPitch(), GetZPitch());
            if (currentAngles != neededAngles)
            {
                currentAngles -= (currentAngles - neededAngles) / _pitchSoftness;
            }
            _sofaModelObject.transform.localEulerAngles = new Vector3(currentAngles.x, _sofaModelObject.transform.localEulerAngles.y, currentAngles.y);
            yield return null;
        }
    }
}
