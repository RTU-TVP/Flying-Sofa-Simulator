using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLose : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] GameObject _blackScreen;
    bool loseStarted = false;
    private void Update()
    {
        if(!playerConfig.GetAliveStatus() && !loseStarted)
        {
            Lose();
            loseStarted = true;
        }
    }
    private void Start()
    {
        ScreenFadeOut();
    }
    public void Lose()
    {
        ScreenFadeIn();
        StartCoroutine(WaitOnLose(2));
    }
    IEnumerator WaitOnLose(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        yield break;
    }
    public void ScreenFadeIn()
    {
        _blackScreen.GetComponent<Image>().DOFade(1, 0.1f);
    }
    public void ScreenFadeOut()
    {
        _blackScreen.GetComponent<Image>().DOFade(0, 0.5f);
    }
}
