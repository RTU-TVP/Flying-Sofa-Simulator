using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] TextMeshProUGUI loading;
    TextMeshProUGUI text;
    bool r_pressed = false;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(timerConfig.currentStringTimerValue == "")
        {
            text.text = "��������� ��������� ������";
        }
        else
        {
            text.text = "�����: " + timerConfig.currentStringTimerValue;
        }
        if(GameObject.FindObjectOfType<TimerManager>() != null) Destroy(GameObject.FindObjectOfType<TimerManager>().gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && (!r_pressed))
        {
            loading.text = "��������";
            r_pressed = true;
            playerConfig.SetNewCheckpoint(0);
            PlayerPrefs.SetInt("checkpoint",0);
            timerConfig.EraseData();
            SceneManager.LoadSceneAsync("GameScene");
        }
    }
}