using System.Collections;
using Events;
using UnityEngine;

public class BGMusicController : MonoBehaviour
{

    private void OnEnable()
    {
        GameEventsManager.Instance.SoundEvents.OnFadeOutMusic += FadeOutSound;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.SoundEvents.OnFadeOutMusic -= FadeOutSound;
    }
    private void FadeOutSound(float time)
    {
        StartCoroutine(FadeOutCoroutine(time));
    }


    private IEnumerator FadeOutCoroutine(float duration)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        float startVolume = audioSource.volume;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
    }
}
