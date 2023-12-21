using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] string _sound;
    private void Start()
    {
        GetComponent<AudioManager>().Play(_sound);
    }
}
