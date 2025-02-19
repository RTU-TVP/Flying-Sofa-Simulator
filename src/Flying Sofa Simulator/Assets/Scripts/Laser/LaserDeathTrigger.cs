using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDeathTrigger : MonoBehaviour
{
    [SerializeField] PlayerConfig playerConfig;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa") && playerConfig.GetAliveStatus())
        {
            playerConfig.SetAliveStatus(false);
            FindObjectOfType<SofaMovement>().GetComponent<AudioManager>().Play("laserDeath");
        }
    }
}
