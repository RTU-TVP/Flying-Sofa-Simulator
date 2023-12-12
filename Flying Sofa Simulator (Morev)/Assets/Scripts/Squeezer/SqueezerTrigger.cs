using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueezerTrigger : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            playerConfig.SetAliveStatus(false);
        }
    }
}
