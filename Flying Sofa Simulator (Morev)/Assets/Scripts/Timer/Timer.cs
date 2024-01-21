using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    [SerializeField] List<TimerMakeNoise> _timerColorSwitches;
    TextMeshProUGUI timerText;
    int i;
    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        i = timerConfig.timerColorMode;
        if(i != 0)
        {
            timerText.color = _timerColorSwitches[i-1].newColor;
        }
        if(!timerConfig.isTimerWorking)
        {
            gameObject.SetActive(false);
        }
        
    }
    private void Update()
    {
        timerText.text = timerConfig.currentStringTimerValue;
        if ((i < _timerColorSwitches.Count) && (timerConfig.currentRealSecondsValue == _timerColorSwitches[i].time))
        {
            timerText.color = _timerColorSwitches[i].newColor;
            //audioManager.Play(_timerColorSwitches[i].soundName);
            i++;
            timerConfig.timerColorMode = i;
        }
    }
}
[Serializable]
public class TimerMakeNoise
{
    public int time;
    public Color newColor;
    public string soundName;
}