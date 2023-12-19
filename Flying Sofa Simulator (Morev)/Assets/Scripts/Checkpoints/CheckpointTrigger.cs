using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] public Transform _respawnPlace;
    [SerializeField] PlayerConfig _playerConfig;
    [HideInInspector]
    public int checkPointNumber;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            _playerConfig.SetNewCheckpoint(checkPointNumber);
            PlayerPrefs.SetInt("checkpoint", checkPointNumber);
        }
    }
}
