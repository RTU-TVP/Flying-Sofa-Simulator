using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicDoor : MonoBehaviour
{
    [SerializeField] Transform _doorLeft;
    [SerializeField] Transform _doorRight;

    Vector3 _leftDoorStartPosition;
    Vector3 _rightDoorStartPosition;

    Vector3 _leftDoorOpenPosition;
    Vector3 _rightDoorOpenPosition;

    bool isColliderOff = false;
    private void Start()
    {
        _leftDoorStartPosition = _doorLeft.transform.localPosition;
        _rightDoorStartPosition = _doorRight.transform.localPosition;
        _leftDoorOpenPosition = _doorLeft.transform.localPosition + new Vector3(0,0,2);
        _rightDoorOpenPosition = _doorRight.transform.localPosition + new Vector3(0,0,-2);
    }

    private void Update()
    {
        if(!isColliderOff && GetComponent<BoxCollider>().enabled == false)
        {
            Close();
        }
    }

    void Open()
    {
        _doorLeft.DOLocalMove(_leftDoorOpenPosition, 1f);
        _doorRight.DOLocalMove(_rightDoorOpenPosition, 1f);
    }
    void Close()
    {
        _doorLeft.DOLocalMove(_leftDoorStartPosition, 1f);
        _doorRight.DOLocalMove(_rightDoorStartPosition, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            Open();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("sofa"))
        {
            Close();
        }
    }

    public void OffDoorTrigger()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
