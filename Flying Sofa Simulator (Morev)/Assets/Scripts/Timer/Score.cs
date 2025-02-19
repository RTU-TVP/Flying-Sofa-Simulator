using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] TimerConfig timerConfig;
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] TextMeshProUGUI loading;
    TextMeshProUGUI text;
    bool r_pressed = false;

    [SerializeField] private Button startButton;

    private AsyncOperation loadSceneAsync;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (timerConfig.currentStringTimerValue == "")
        {
            text.text = "Брось вызов логике!";
        }
        else
        {
            text.text = "�����: " + timerConfig.currentStringTimerValue;
        }

        if (GameObject.FindObjectOfType<TimerManager>() != null)
            Destroy(GameObject.FindObjectOfType<TimerManager>().gameObject);

        loadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadSceneAsync.allowSceneActivation = false;

        startButton.onClick.AddListener(LaunchGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (!r_pressed))
        {
            LaunchGame();
        }
    }

    private void LaunchGame()
    {
        loading.text = "��������";
        r_pressed = true;
        playerConfig.SetNewCheckpoint(0);
        PlayerPrefs.SetInt("checkpoint", 0);
        timerConfig.EraseData();
        loadSceneAsync.allowSceneActivation = true;
    }
}