using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCountdown : MonoBehaviour
{
    [SerializeField] GameObject _border;
    [SerializeField] float _time;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            StartCoroutine(counter(_time));
        }
    }


    IEnumerator counter(float time)
    {
        yield return new WaitForSeconds(time);
        _border.GetComponent<FollowingBorder>().StartMoving();
        yield break;
    }
}
