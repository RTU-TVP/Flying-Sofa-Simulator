using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsScript : MonoBehaviour
{
    [SerializeField] List<CheckpointTrigger> _checkpoints;
    [SerializeField] PlayerConfig _playerConfig;
    GameObject player;
    private void Awake()
    {
        for (int i = 0; i < _checkpoints.Count; i++)
        {
            _checkpoints[i].checkPointNumber = i;
        }
        player = GameObject.FindObjectOfType<SofaMovement>().gameObject;
        _playerConfig.SetNewCheckpoint(PlayerPrefs.GetInt("checkpoint"));
        Debug.Log(_playerConfig.GetCurrentCheckpoint());
    }
    private void Start()
    {
        Respawn();
    }
    void Respawn()
    {
        player.transform.position = _checkpoints[_playerConfig.GetCurrentCheckpoint()]._respawnPlace.position;
        player.transform.rotation = _checkpoints[_playerConfig.GetCurrentCheckpoint()]._respawnPlace.rotation;
        player.GetComponent<PlayerLose>().ScreenFadeOut();
    }
}
