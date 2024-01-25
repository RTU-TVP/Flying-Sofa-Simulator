using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTimerTrigger : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SofaMovement>(out SofaMovement player))
        {
            GameObject.FindObjectOfType<TimerManager>().StopTimer();
            timerConfig.isTimerWorking = false;
        }
    }
}
