using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    [SerializeField] PlayerConfig playerConfig;
    TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(timerConfig.currentStringTimerValue == "")
        {
            text.text = "—имул€тор летающего дивана";
        }
        else
        {
            text.text = "—чЄт: " + timerConfig.currentStringTimerValue;
        }
        if(GameObject.FindObjectOfType<TimerManager>() != null) Destroy(GameObject.FindObjectOfType<TimerManager>().gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            playerConfig.SetNewCheckpoint(0);
            PlayerPrefs.SetInt("checkpoint",0);
            timerConfig.EraseData();
            SceneManager.LoadSceneAsync("GameScene");
        }
    }
}