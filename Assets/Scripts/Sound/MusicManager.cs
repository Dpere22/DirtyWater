using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events;

public class MusicTransitionManager : MonoBehaviour
{
    public AudioSource musicTrack1;
    public AudioSource musicTrack1_5;
    public AudioSource musicTrack2;
    public AudioSource musicTrack2_5;
    public AudioSource musicTrack3;

    public float crossfadeDuration = 2f;

    private void OnEnable()
    {
        GameEventsManager.Instance.QuestEvents.OnFinishQuest += HandleQuestFinished;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.QuestEvents.OnFinishQuest -= HandleQuestFinished;
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
        yield return Crossfade(from, transition);
        yield return new WaitUntil(() => !transition.isPlaying);
        yield return Crossfade(transition, to);
    }

    private IEnumerator Crossfade(AudioSource from, AudioSource to)
    {
        to.Play();
        float timeElapsed = 0f;
        float fromStartVolume = from.volume;
        float toStartVolume = to.volume;
        to.volume = 0f;

        while (timeElapsed < crossfadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / crossfadeDuration;

            from.volume = Mathf.Lerp(fromStartVolume, 0f, t);
            to.volume = Mathf.Lerp(0f, toStartVolume, t);

            yield return null;
        }

        from.Stop();
        from.volume = fromStartVolume;
        to.volume = toStartVolume;
    }
}