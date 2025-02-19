using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserVentilator : MonoBehaviour
{
    [SerializeField] LaserVentilatorPart _leftUp;
    [SerializeField] LaserVentilatorPart _rightUp;
    [SerializeField] LaserVentilatorPart _leftDown;
    [SerializeField] LaserVentilatorPart _rightDown;


    [SerializeField] float _rotationSpeed;

    private void Start()
    {
        _leftUp.ventilatorPart.SetActive(_leftUp.isActive);
        _rightUp.ventilatorPart.SetActive(_rightUp.isActive);
        _leftDown.ventilatorPart.SetActive(_leftDown.isActive);
        _rightDown.ventilatorPart.SetActive(_rightDown.isActive);
    }
    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(1, 0, 0) * _rotationSpeed);
    }
}

[Serializable]
public class LaserVentilatorPart
{
    public GameObject ventilatorPart;
    public bool isActive;
}
