using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Sound
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] clips;
        private readonly Dictionary<string, AudioClip> _clips = new();

        private void OnEnable()
        {
            GameEventsManager.Instance.SoundEvents.OnPlaySoundEffect += PlayAudioClip;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.SoundEvents.OnPlaySoundEffect -= PlayAudioClip;
        }
    
        private void Start()
        {
            foreach (var clip in clips)
            {
                _clips.Add(clip.name, clip);
            }
        }

        private void PlayAudioClip(string clipName)
        {
            if(_clips.TryGetValue(clipName, out var clip)) //safe code
                audioSource.PlayOneShot(clip);
            else Debug.LogWarning("SoundEffectManager::PlaySoundEffect: clip not found");
        }
    }
}
