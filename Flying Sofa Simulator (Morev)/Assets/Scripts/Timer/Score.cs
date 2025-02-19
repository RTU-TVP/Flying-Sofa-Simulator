using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private TimerConfig timerConfig;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private TextMeshProUGUI loading;
    
    private TextMeshProUGUI text;
    private bool r_pressed = false;

    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;

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
        stopButton.onClick.AddListener(StopDelay);
        
        StartCoroutine(DelayedStart());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            LaunchGame();
        }
    }

    private void LaunchGame()
    {
        if (r_pressed)
        {
            return;
        }
        
        r_pressed = true;
        
        startButton.onClick.RemoveListener(LaunchGame);

        loading.text = "��������";
        playerConfig.SetNewCheckpoint(0);
        PlayerPrefs.SetInt("checkpoint", 0);
        timerConfig.EraseData();
        loadSceneAsync.allowSceneActivation = true;
    }
    
    private void StopDelay()
    {
        StopAllCoroutines();
        loading.text = "";
    }
    
    private IEnumerator DelayedStart()
    {
        loading.text = $"5";
        yield return new WaitForSeconds(1);
        loading.text = $"4";
        yield return new WaitForSeconds(1);
        loading.text = $"3";
        yield return new WaitForSeconds(1);
        loading.text = $"2";
        yield return new WaitForSeconds(1);
        loading.text = $"1";
        yield return new WaitForSeconds(1);
        
        LaunchGame();
    }
}