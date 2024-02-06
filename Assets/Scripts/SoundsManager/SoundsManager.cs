using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.SoundsManager
{
    public class SoundsManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioSource _background;
        
        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _collision;
        [SerializeField] private AudioClip _victory;
        [SerializeField] private AudioClip _defeat;
        [SerializeField] private AudioClip _laser;
        
        
        public void PlayBackground()
        {
            _audioSource.Play();
        }

        public void StopBackground()
        {
            _audioSource.Stop();
        }

        public void PlayClick()
        {
            _audioSource.PlayOneShot(_click);
        }
        
        public void PlayCollision()
        {
            _audioSource.PlayOneShot(_collision);
        }
        
        public void PlayVictory()
        {
            _audioSource.PlayOneShot(_victory);
        }
        
        public void PlayDefeat()
        {
            _audioSource.PlayOneShot(_defeat);
        }

        public void PlayShotLaser()
        {
            _audioSource.PlayOneShot(_laser);
        }

        public void MuteUnmute()
        {
            _audioSource.mute = !_audioSource.mute;
            _background.mute = !_background.mute;
        }

    }
}