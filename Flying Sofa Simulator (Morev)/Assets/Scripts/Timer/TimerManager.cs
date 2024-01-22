using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    public bool isAwaken { get; private set; } = false;
    bool isTimerWorking = true;
    int sec = 0;
    int min = 0;
    int totalSeconds = 0;
    private void Awake()
    {
        if(GameObject.FindObjectsOfType<TimerManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            timerConfig.EraseData();
            timerConfig.currentTimerManager = this;
        }
    }
    public void StartTimer()
    {
        StartCoroutine(timer());
    }
    public void StopTimer()
    {
        isTimerWorking = false;
    }
    IEnumerator timer()
    {
        while (isTimerWorking)
        {
            if (sec < 59)
            {
                sec++;
            }
            else
            {
                sec = 0;
                min++;
            }
            totalSeconds++;
            timerConfig.currentRealSecondsValue = RealSeconds();
            timerConfig.currentStringTimerValue = TimerString();
            yield return new WaitForSeconds(1);
        }
        yield break;
    }
    public string TimerString()
    {
        if (sec < 10)
        {
            return min + ":0" + sec;
        }
        else
        {
            return min + ":" + sec;
        }
    }
    public int RealSeconds()
    {
        return totalSeconds;
    }
}
