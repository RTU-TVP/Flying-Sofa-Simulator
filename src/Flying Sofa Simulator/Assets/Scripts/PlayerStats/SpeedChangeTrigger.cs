using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeTrigger : MonoBehaviour
{
    [SerializeField] float _newSpeed;
    [SerializeField] bool _immediate;
    [SerializeField] float _softness;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            if (_immediate) other.gameObject.GetComponent<SofaMovement>().SetMovementSpeed(_newSpeed);
            else other.gameObject.GetComponent<SofaMovement>().SoftSetMovementSpeed(_newSpeed, _softness);
            Destroy(gameObject);
        }
    }
}
