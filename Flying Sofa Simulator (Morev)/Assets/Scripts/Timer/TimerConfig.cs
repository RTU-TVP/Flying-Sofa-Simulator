using UnityEngine;

[CreateAssetMenu(fileName = "TimerConfig", menuName = "ScriptableObjects/TimerConfig", order = 2)]
public class TimerConfig : ScriptableObject
{
    public string currentStringTimerValue = "";
    public int currentRealSecondsValue = 0;
    public bool isTimerWorking = false;
    public int timerColorMode = 0;
    public TimerManager currentTimerManager;


    public void EraseData()
    {
        currentStringTimerValue = "";
        currentRealSecondsValue = 0;
        isTimerWorking = false;
        timerColorMode = 0;
    }
}
