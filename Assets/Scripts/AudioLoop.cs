using UnityEngine;

public class AudioLoop : MonoBehaviour
{
    private AudioSource _audioSource;

   private void Start()
    {
        // Проверяем, что AudioSource установлен
        if (_audioSource != null)
        {
            // Включаем зацикливание
            _audioSource.loop = true;
        }

    }
}