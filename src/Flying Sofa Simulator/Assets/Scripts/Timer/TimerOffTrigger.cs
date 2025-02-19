using TMPro;
using UnityEngine;

public class TimerOffTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<SofaMovement>(out SofaMovement player))
        {
            foreach(Timer timer in GameObject.FindObjectsOfType<Timer>())
            {
                timer.gameObject.GetComponent<TextMeshProUGUI>().enabled = false;

            }
        }
    }
}
