using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSoundTrigger : MonoBehaviour
{
    [SerializeField] List<AudioManager> _audioPlayers;
    [SerializeField] string _sound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("sofa"))
        {
            foreach(AudioManager audio in _audioPlayers)
            {
                audio.Play(_sound);
            }
            Destroy(gameObject);
        }
    }
}
