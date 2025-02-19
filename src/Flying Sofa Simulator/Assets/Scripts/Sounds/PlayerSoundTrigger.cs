using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundTrigger : MonoBehaviour
{
    [SerializeField] string _soundName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            other.GetComponent<AudioManager>().Play(_soundName);
            Destroy(gameObject);
        }
    }
}
