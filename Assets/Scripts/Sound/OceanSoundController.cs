using Events;
using UnityEngine;

namespace Sound
{
    public class OceanSoundController : MonoBehaviour
    {
        private AudioSource _audioSource;
        private void OnEnable()
        {
            GameEventsManager.Instance.DayEvents.OnEnterWater += SetInWater;
            GameEventsManager.Instance.DayEvents.OnDayEnd += SetSurface;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.DayEvents.OnEnterWater -= SetInWater;
            GameEventsManager.Instance.DayEvents.OnDayEnd -= SetSurface;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void SetInWater()
        {
            _audioSource.pitch = 0.4f;
        }

        private void SetSurface()
        {
            _audioSource.pitch = 1f;
        }
    }
}
