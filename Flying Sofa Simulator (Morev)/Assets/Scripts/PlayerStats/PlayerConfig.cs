using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class PlayerConfig : ScriptableObject
{
    bool isAlive;
    float velocity;
    int currentCheckpoint;

    public bool GetAliveStatus()
    {
        return isAlive;
    }
    public void SetAliveStatus(bool value)
    {
        isAlive = value;
    }
    public float GetVelocity()
    {
        return velocity;
    }
    public void SetVelocity(float value)
    {
        velocity = value;
    }
    public void SetNewCheckpoint(int newCheckpoint)
    {
        currentCheckpoint = newCheckpoint;
    }
    public int GetCurrentCheckpoint()
    {
        return currentCheckpoint;
    }
}
