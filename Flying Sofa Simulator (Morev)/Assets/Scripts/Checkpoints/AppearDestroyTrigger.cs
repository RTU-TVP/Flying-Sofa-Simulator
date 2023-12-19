using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearDestroyTrigger : MonoBehaviour
{
    [SerializeField] List<GameObject> _destroyOnTriggered;
    [SerializeField] List<GameObject> _enableOnTriggered;
    [SerializeField] List<GameObject> _disableOnTriggered;

    [SerializeField] float _timer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            StartCoroutine(Timer(_timer));
        }
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        foreach(GameObject go in _destroyOnTriggered)
        {
            if (go != null) Destroy(go);
        }
        foreach(GameObject go in _enableOnTriggered)
        {
            if ((go != null) && (go.activeSelf == false)) go.SetActive(true);
        }
        foreach(GameObject go in _disableOnTriggered)
        {
            if ((go != null) && (go.activeSelf == true)) go.SetActive(false);
        }
        yield break;
    }
}


