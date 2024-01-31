using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void SetAudioMod()
    {
        _audioSource.mute = !_audioSource.mute;
    }
}
