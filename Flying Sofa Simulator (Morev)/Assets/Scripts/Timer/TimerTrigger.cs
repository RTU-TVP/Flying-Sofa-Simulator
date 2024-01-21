using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    [SerializeField] List<Timer> timers;
    private void Start()
    {
        if(timerConfig.isTimerWorking)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SofaMovement>(out SofaMovement player))
        {
            foreach (Timer timer in timers)
            {
                timer.gameObject.SetActive(true);
            }
            timerConfig.isTimerWorking = true;
            timerConfig.currentTimerManager.StartTimer();
            Destroy(gameObject);
        }
    }
}
