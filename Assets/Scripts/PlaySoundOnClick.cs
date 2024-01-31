using UnityEngine;
using UnityEngine.Serialization;

public class PlaySoundOnClick : MonoBehaviour
{
    [SerializeField]private AudioClip _soundToPlay; // Звук для воспроизведения

    private AudioSource _audioSource;

   private void Start()
    {
        // Получаем компонент AudioSource на текущем объекте
        _audioSource = GetComponent<AudioSource>();

        // Устанавливаем звук для AudioSource
        if(_audioSource!=null)
        _audioSource.clip = _soundToPlay;
    }

   private void OnMouseDown()
    {
        // При нажатии на объект воспроизводим звук
        _audioSource.Play();
    }
}