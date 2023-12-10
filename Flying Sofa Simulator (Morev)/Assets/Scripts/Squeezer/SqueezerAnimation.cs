using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezerAnimation : MonoBehaviour
{
    [SerializeField] GameObject _squeezerPart1;
    [SerializeField] GameObject _squeezerPart2;
    [SerializeField] List<GameObject> _triggers;
    [SerializeField] float _timerOffset;
    [SerializeField] float _squeezeTime;
    [SerializeField] float _antiSqueezeTime;
    [SerializeField] float _afterSqueezeRest;
    [SerializeField] float _beforeSqueezeRest;
    Vector3 squeezedPosition1;
    Vector3 squeezedPosition2;
    Vector3 normalPosition1;
    Vector3 normalPosition2;
    private void Start()
    {
        normalPosition1 = _squeezerPart1.transform.localPosition;
        normalPosition2 = _squeezerPart2.transform.localPosition;
        squeezedPosition1 = new Vector3((_squeezerPart1.transform.localScale.x)/2,0,0);
        squeezedPosition2 = new Vector3((_squeezerPart2.transform.localScale.x) / (-2), 0, 0);
        StartCoroutine(SqueezerIterator(_timerOffset, _squeezeTime, _antiSqueezeTime, _afterSqueezeRest, _beforeSqueezeRest));
    }

    void OneSqueeze(float squeezeTime)
    {
        StartCoroutine(EnableTriggersAfterTime(_squeezeTime / 2));
        _squeezerPart1.transform.DOLocalMove(squeezedPosition1, squeezeTime).SetEase(Ease.InQuint);
        _squeezerPart2.transform.DOLocalMove(squeezedPosition2, squeezeTime).SetEase(Ease.InQuint);
    }
    void OneAntiSqueeze(float antiSqueezeTime)
    {
        StartCoroutine(DisableTriggersAfterTime(_squeezeTime / 2));
        _squeezerPart1.transform.DOLocalMove(normalPosition1, antiSqueezeTime).SetEase(Ease.InOutQuad);
        _squeezerPart2.transform.DOLocalMove(normalPosition2, antiSqueezeTime).SetEase(Ease.InOutQuad);
    }
    IEnumerator SqueezerIterator(float offset, float squeezeTime, float antiSqieezeTime, float relaxAfterSqueezeTime, float relaxBeforeSqueezeTime)
    {
        yield return new WaitForSeconds(offset);
        while(true)
        {
            OneSqueeze(squeezeTime);
            yield return new WaitForSeconds(squeezeTime);
            // boom sound
            yield return new WaitForSeconds(relaxAfterSqueezeTime);
            OneAntiSqueeze(antiSqieezeTime);
            yield return new WaitForSeconds(antiSqieezeTime);
            // piston relax sound
            yield return new WaitForSeconds(relaxBeforeSqueezeTime);
        }
    }
    void EnableSqueezerTriggers()
    {
        foreach(GameObject trigger in _triggers)
        {
            trigger.SetActive(true);
        }
    }
    void DisableSqueezerTriggers()
    {
        foreach (GameObject trigger in _triggers)
        {
            trigger.SetActive(false);
        }
    }

    IEnumerator DisableTriggersAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DisableSqueezerTriggers();
        yield break;
    }
    IEnumerator EnableTriggersAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        EnableSqueezerTriggers();
        yield break;
    }
}
