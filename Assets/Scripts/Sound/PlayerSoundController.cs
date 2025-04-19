using Player;
using UnityEngine;

namespace Sound
{
    public class PlayerSoundController : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        private AudioSource _audioSource;
        private PlayerMovement _playerMovement;
        private bool _started;
        private void Start()
        {
            _playerMovement = player.GetComponent<PlayerMovement>();
            _audioSource = GetComponent<AudioSource>();
        }
    
        private void Update()
        {
            if (_playerMovement.isWalking && !_started)
            {
                float timeStamp = Random.Range(0f, _audioSource.clip.length);
                _audioSource.time = timeStamp;
                _audioSource.Play();
                _started = true;
            }
            else if (!_playerMovement.isWalking)
            {
                _started = false;
                _audioSource.Stop();
            }
        }
    }
}
