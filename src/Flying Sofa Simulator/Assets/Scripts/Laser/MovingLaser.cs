using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    [SerializeField] float _timeOfOneMovement;
    [SerializeField] Transform _point1;
    [SerializeField] Transform _point2;
    [SerializeField] Transform _laser;

    private void Start()
    {
        StartCoroutine(LaserMove());
    }


    IEnumerator LaserMove()
    {
        while(true)
        {
            _laser.DOLocalMove(_point1.localPosition, _timeOfOneMovement).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(_timeOfOneMovement);
            _laser.DOLocalMove(_point2.localPosition, _timeOfOneMovement).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(_timeOfOneMovement);
        }
    }
}
