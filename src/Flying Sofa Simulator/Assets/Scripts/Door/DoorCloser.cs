using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    [SerializeField] GameObject _actualDoor;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            _actualDoor.GetComponent<MechanicDoor>().OffDoorTrigger();
            _actualDoor.GetComponent<MechanicDoor>().Close();
            Destroy(gameObject);
        }
    }
}
