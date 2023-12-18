using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowingBorder : MonoBehaviour
{
    [SerializeField] float time;
    float rotateSpeed = 0;
    public bool testStart = false;
    void StartMoving()
    {
        StartCoroutine(RotateSpeedUp(10));
        transform.DOLocalMove(new Vector3(15880, transform.localPosition.y, transform.localPosition.z),time).SetEase(Ease.InCubic);
    }
    private void FixedUpdate()
    {
        if(testStart)
        {
            testStart = false;
            StartMoving();
        }
        transform.Rotate(new Vector3(1,0,0) * rotateSpeed);
    }

    IEnumerator RotateSpeedUp(float maxRotationSpeed)
    {
        while(rotateSpeed < maxRotationSpeed)
        {
            rotateSpeed += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
