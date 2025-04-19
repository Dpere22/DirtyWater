using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CreditsController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float audioFadeOutTime;
        public void FadeOutMusic()
        {
            FadeOutSound(audioFadeOutTime);
        }
    
        private void FadeOutSound(float time)
        {
            StartCoroutine(FadeOutCoroutine(time));
        }
    
        private IEnumerator FadeOutCoroutine(float duration)
        {
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
    
        public void OnCreditsEnd()
        {
            SceneManager.LoadScene(0);
        }
    }
}
