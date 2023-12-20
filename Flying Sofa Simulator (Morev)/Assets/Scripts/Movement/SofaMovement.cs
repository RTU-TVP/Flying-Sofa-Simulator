using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SofaMovement : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] float _movingSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] int _pitchSoftness;
    [SerializeField] float _deadlyVelocity;

    Vector3 currentPos;
    Vector3 previousPos;
    void Start()
    {
        playerConfig.SetAliveStatus(true);
        currentPos = transform.position;
        previousPos = transform.position;
        StartCoroutine(AngleChanger());
    }
    void FixedUpdate()
    {
        if(playerConfig.GetAliveStatus())
        {
            RotateInChosenDirection();
            MoveInChosenDirection();
            ChangeHigh();
        }
        SetVelocity();
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
            pos.y += ControllerInputValues.rightStickValue.y * Time.deltaTime * _movingSpeed * 0.5f;
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
            if (playerConfig.GetAliveStatus())
            {
                neededAngles = new Vector2(GetXPitch(), GetZPitch());
            }
            else
            {
                neededAngles = Vector2.zero;
            }
            if (currentAngles != neededAngles)
            {
                currentAngles -= (currentAngles - neededAngles) / _pitchSoftness;
            }
            transform.localEulerAngles = new Vector3(currentAngles.x, transform.localEulerAngles.y, currentAngles.y);
            yield return null;
        }
    }
    void SetVelocity()
    {
        previousPos = currentPos;
        currentPos = transform.position;
        playerConfig.SetVelocity((currentPos - previousPos).magnitude / Time.fixedDeltaTime);
    }

    public void SetMovementSpeed(float value)
    {
        _movingSpeed = value;
    }
    public void SoftSetMovementSpeed(float value, float softness)
    {
        StartCoroutine(SpeedChanger(value, softness));
    }

    IEnumerator SpeedChanger(float value, float softness)
    {
        while (Mathf.Abs(_movingSpeed - value) > 1)
        {
            _movingSpeed += (value - _movingSpeed) / softness;
            if (Mathf.Abs(_movingSpeed - value) < 1)
            {
                _movingSpeed = value;
                yield break;
            }
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(playerConfig.GetVelocity());
        if (playerConfig.GetVelocity() > _deadlyVelocity)
        {
            playerConfig.SetAliveStatus(false);
        }
    }
}
