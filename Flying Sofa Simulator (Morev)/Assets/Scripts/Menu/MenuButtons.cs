using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] Color _baseColor;
    [SerializeField] Color _chosenColor;
    [SerializeField] Color _pushedColor;

    [SerializeField] Image _newGame;
    [SerializeField] Image _continue;
    [SerializeField] Image _exit;

    AudioManager audio;

    [Range(-1f, 1f)]
    [SerializeField] float testControllerValue = 0;
    [Range(0f, 1f)]
    [SerializeField] float testButton = 0;

    int currentButton = 0;
    bool areButtonsSwitchable = true;
    bool buttonPushed = false;
    Action currentButtonAction;

    private void Start()
    {
        audio = GetComponent<AudioManager>();
        StartCoroutine(TestTurnOnSwitchButtons(0.5f));
        StartCoroutine(TurnOnSwitchButtons(0.5f));
    }
    private void Update()
    {
        TestButtonsSwitch();
        TestPushButton();
        ButtonsSwitch();
        PushButton();
    }

    void TestButtonsSwitch()
    {
        if (areButtonsSwitchable)
        {
            if (testControllerValue >= 0.5f)
            {
                SwitchButtonToUpper();
                SetCurrentButton(currentButton);
            }
            if (testControllerValue <= -0.5f)
            {
                SwitchButtonToLower();
                SetCurrentButton(currentButton);
            }
        }
    }
    void ButtonsSwitch()
    {
        if (areButtonsSwitchable)
        {
            if (ControllerInputValues.leftStickValue.y >= 0.5f)
            {
                SwitchButtonToUpper();
                SetCurrentButton(currentButton);
            }
            if (ControllerInputValues.leftStickValue.y <= -0.5f)
            {
                SwitchButtonToLower();
                SetCurrentButton(currentButton);
            }
        }
    }






    void SwitchButtonToUpper()
    {
        if(currentButton > 1)
        {
            currentButton--;
            audio.Play($"b{currentButton}");
        }
        areButtonsSwitchable = false;
    }
    void SwitchButtonToLower()
    {
        if(currentButton < 3)
        {
            currentButton++;
            audio.Play($"b{currentButton}");
        }
        areButtonsSwitchable = false;
    }

    IEnumerator TurnOnSwitchButtons(float time)
    {
        while(true)
        {
            if(Mathf.Abs(ControllerInputValues.leftStickValue.y) > 0.5f)
            {
                if (areButtonsSwitchable)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(time);
                    areButtonsSwitchable = true;
                }
            }
            else
            {
                areButtonsSwitchable = true;
                yield return null;
            }
        }
    }

    IEnumerator TestTurnOnSwitchButtons(float time)
    {
        while (true)
        {
            if (Mathf.Abs(testControllerValue) > 0.5f)
            {
                if (areButtonsSwitchable)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSeconds(time);
                    areButtonsSwitchable = true;
                }
            }
            else
            {
                areButtonsSwitchable = true;
                yield return null;
            }
        }
    }

    void SetCurrentButton(int index)
    {
        switch (index)
        {
            case 1:
                StartCoroutine(ColorChanger(_chosenColor, _newGame, 0.3f));
                StartCoroutine(ColorChanger(_baseColor, _continue, 0.3f));
                StartCoroutine(ColorChanger(_baseColor, _exit, 0.3f));

                //_newGame.color = _chosenColor;
                //_continue.color = _baseColor;
                //_exit.color = _baseColor;
                currentButtonAction = NewGame;
                break;
            case 2:
                StartCoroutine(ColorChanger(_chosenColor, _continue, 0.3f));
                StartCoroutine(ColorChanger(_baseColor, _newGame, 0.3f));
                StartCoroutine(ColorChanger(_baseColor, _exit, 0.3f));

                //_newGame.color = _baseColor;
                //_continue.color = _chosenColor;
                //_exit.color = _baseColor;
                currentButtonAction = Continue;
                break;
            case 3:
                StartCoroutine(ColorChanger(_chosenColor, _exit, 0.3f));
                StartCoroutine(ColorChanger(_baseColor, _newGame, 0.3f));
                StartCoroutine(ColorChanger(_baseColor, _continue, 0.3f));

                //_newGame.color = _baseColor;
                //_continue.color = _baseColor;
                //_exit.color = _chosenColor;
                currentButtonAction = Exit;
                break;
            default:
                break;
        }
    }


    IEnumerator ColorChanger(Color newColor, Image changedImage, float fadeTime)
    {
        float timer = 0;
        Color currentStartColor = changedImage.color;
        while (changedImage.color != newColor)
        {
            timer += Time.deltaTime;
            changedImage.color = Color.Lerp(currentStartColor, newColor, timer / fadeTime);
            yield return null;
        }
        yield break;
    }


    void NewGame()
    {
        PlayerPrefs.SetInt("checkpoint", 0);
        SceneManager.LoadScene("GameScene");
    }
    void Continue()
    {
        SceneManager.LoadScene("GameScene");
    }
    void Exit()
    {
        Application.Quit();
    }

    void StartButtonAction()
    {
        if(currentButtonAction != null)
        {
            currentButtonAction.Invoke();
            buttonPushed = true;
        }
    }

    void PushButton()
    {
        if(!buttonPushed && ControllerInputValues.leftTrigger > 0.5f)
        {
            StartButtonAction();
        }
    }
    void TestPushButton()
    {
        if (!buttonPushed && testButton > 0.5f)
        {
            StartButtonAction();
        }
    }
}
