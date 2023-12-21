using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    public void FinishLevel(float secondsOfWaiting)
    {
        StartCoroutine(FinishTimer(secondsOfWaiting));
    }


    IEnumerator FinishTimer(float time)
    {
        yield return new WaitForSeconds(time - 1);
        GetComponent<PlayerLose>().ScreenFadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
        yield break;
    }
}
