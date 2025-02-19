using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointsScript : MonoBehaviour
{
    [SerializeField] List<CheckpointLocationPair> _checkpoints;
    [SerializeField] PlayerConfig _playerConfig;
    [SerializeField] TimerConfig _timerConfig;
    GameObject player;
    private void Awake()
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            _checkpoints[i].trigger.checkPointNumber = i;
        }
        player = GameObject.FindObjectOfType<SofaMovement>().gameObject;
        _playerConfig.SetNewCheckpoint(PlayerPrefs.GetInt("checkpoint"));
        Respawn();
    }
    void Respawn()
    {
        player.transform.position = _checkpoints[_playerConfig.GetCurrentCheckpoint()].trigger._respawnPlace.position;
        player.transform.rotation = _checkpoints[_playerConfig.GetCurrentCheckpoint()].trigger._respawnPlace.rotation;
        ShowNeededLocationsOnRespawn();
        DestroyCompletedLocationsOnRespawn();
    }

    void DestroyCompletedLocationsOnRespawn()
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            if (_checkpoints[i].trigger.checkPointNumber < _playerConfig.GetCurrentCheckpoint())
            {
                Destroy(_checkpoints[i].location);
            }
            else
            {
                break;
            }
        }
    }
    void ShowNeededLocationsOnRespawn()
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            if ((_checkpoints[i].trigger.checkPointNumber == _playerConfig.GetCurrentCheckpoint()) || (_checkpoints[i].trigger.checkPointNumber == _playerConfig.GetCurrentCheckpoint() + 1) || (_checkpoints[i].trigger.checkPointNumber == _playerConfig.GetCurrentCheckpoint() + 2))
            {
                _checkpoints[i].location.SetActive(true);
            }
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            _timerConfig.EraseData();
            _playerConfig.SetNewCheckpoint(0);
            PlayerPrefs.SetInt("checkpoint",0);
            SceneManager.LoadScene("Victory");
        }
    }
}

[Serializable]
public class CheckpointLocationPair
{
    public CheckpointTrigger trigger;
    public GameObject location;
}
