using System.Collections;
using Events;
using UnityEngine;

namespace Sound
{
    public class MusicTransitionManager : MonoBehaviour
    {
        public AudioSource musicTrack1;
        public AudioSource musicTrack1_5;
        public AudioSource musicTrack2;
        public AudioSource musicTrack2_5;
        public AudioSource musicTrack3;

        public float crossfadeDuration = 2f;

        private float _timeElapsed;
        private AudioSource _to;
        private AudioSource _from;
        private bool _fading;
        private float _fromStartVolume;
        private float _toStartVolume;

        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnFinishQuest += HandleQuestFinished;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnFinishQuest -= HandleQuestFinished;
        }

        private void Update()
        {
            if (!_fading) return;

            if (_timeElapsed < crossfadeDuration)
            {
                _timeElapsed += Time.deltaTime;
                float t = _timeElapsed / crossfadeDuration;

                _from.volume = Mathf.Lerp(_fromStartVolume, 0f, t);
                _to.volume = Mathf.Lerp(0f, _toStartVolume, t);
            }
            else
            {
                _from.Stop();
                _fading = false;
                _timeElapsed = 0f;
            }
        }

        private void HandleQuestFinished(string questId)
        {
            if (questId == "GetToolboxQuest")
            {
                StartCoroutine(PlaySequence(musicTrack1, musicTrack1_5, musicTrack2));
            }
            else if (questId == "ChestQuest")
            {
                StartCoroutine(PlaySequence(musicTrack2, musicTrack2_5, musicTrack3));
            }
        }

        private IEnumerator PlaySequence(AudioSource from, AudioSource transition, AudioSource to)
        {
            from.Stop();
            transition.Play();
            
            float fadeStartTime = transition.clip.length - crossfadeDuration;
            while (transition.time < fadeStartTime)
                yield return null;
            
            _from = transition;
            _to = to;
            _fromStartVolume = _from.volume;
            _toStartVolume = _to.volume;
            _to.volume = 0f;
            _to.Play();
            _fading = true;
            
            while (_fading)
                yield return null;
        }
    }
}