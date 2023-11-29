using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SofaMovement : MonoBehaviour
{
    [SerializeField] int _movingSpeed;
    [SerializeField] int _rotationSpeed;
    [SerializeField] int _pitchSoftness;
    [Header("Test values")]
    [Range(0f, 1f)]
    [SerializeField] float turnLeft;
    [Range(0f, 1f)]
    [SerializeField] float turnRight;
    [SerializeField] Vector2 movementStick;
    [Range(0f, 1f)]
    [SerializeField] float goUp;
    [Range(0f, 1f)]
    [SerializeField] float goDown;
    void Start()
    {
        StartCoroutine(AngleChanger());
        //StartCoroutine(TestAngleChanger());
    }
    void FixedUpdate()
    {
        RotateInChosenDirection();
        MoveInChosenDirection();
        ChangeHigh();


        //TestRotateInChosenDirection();
        //TestMoveInChosenDirection();
        //TestChangeHigh();
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
    void TestRotateInChosenDirection()
    {
        if (turnLeft != 0 || turnRight != 0)
        {
            Vector3 currentRotationValue = transform.eulerAngles;
            currentRotationValue.y += (turnRight - turnLeft)*Time.deltaTime * _rotationSpeed;
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
    void TestMoveInChosenDirection()
    {
        if (movementStick != Vector2.zero)
        {
            transform.position = transform.position + new Vector3(transform.forward.x,0, transform.forward.z) * Time.deltaTime * movementStick.y * _movingSpeed;
            transform.position = transform.position + new Vector3(transform.right.x,0, transform.right.z) * Time.deltaTime * movementStick.x * _movingSpeed;
        }
    }
    void ChangeHigh()
    {
        if(ControllerInputValues.rightGrip != 0 || ControllerInputValues.leftGrip != 0)
        {
            Vector3 pos = transform.position;
            pos.y += (ControllerInputValues.rightGrip - ControllerInputValues.leftGrip) * Time.deltaTime;
            transform.position = pos;
        }
    }
    void TestChangeHigh()
    {
        if (goUp != 0 || goDown != 0)
        {
            Vector3 pos = transform.position;
            pos.y += (goUp - goDown) * Time.deltaTime;
            transform.position = pos;
        }
    }
    float GetXPitch()
    {
        float x = 0;
        x += ControllerInputValues.leftStickValue.x * 20;
        x -= (ControllerInputValues.rightGrip - ControllerInputValues.leftGrip) * 10;
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
        z -= ControllerInputValues.leftStickValue.y * 20;
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

    float TestGetXPitch()
    {
        float x = 0;
        x += movementStick.x * 20;
        x -= (goUp - goDown) * 10;
        if (x > 30)
        {
            x = 30;
        }
        if (x < -30)
        {
            x = -30;
        }
        return x;
    }
    float TestGetZPitch()
    {
        float z = 0;
        z -= movementStick.y * 20;
        z -= (turnRight - turnLeft) * 10;
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
            transform.eulerAngles = new Vector3(currentAngles.x, transform.eulerAngles.y, currentAngles.y);
            yield return null;
        }
    }
    IEnumerator TestAngleChanger()
    {
        Vector2 currentAngles = new Vector2(0, 0);
        Vector2 neededAngles = new Vector2(TestGetXPitch(), TestGetZPitch());
        while (true)
        {
            neededAngles = new Vector2(TestGetXPitch(), TestGetZPitch());
            if (currentAngles != neededAngles)
            {
                currentAngles -= (currentAngles - neededAngles) / _pitchSoftness;
            }
            transform.eulerAngles = new Vector3(currentAngles.x, transform.eulerAngles.y, currentAngles.y);
            yield return null;
        }
    }
}
