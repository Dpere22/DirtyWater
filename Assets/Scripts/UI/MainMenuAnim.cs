using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnim : MonoBehaviour
{
    private static readonly int FadeOut = Animator.StringToHash("fadeOut");
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    private MainMenuManager _mainMenuManager;

    public void Start()
    {
        _mainMenuManager = mainMenu.GetComponent<MainMenuManager>();
        _mainMenuManager.OnMainMenuClicked += HandleClicked;
    }

    private void HandleClicked()
    {
        StartCoroutine(FadeOutSound(1f));
        animator.SetTrigger(FadeOut);
    }
    private void OnCompleteAnim()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator FadeOutSound(float duration)
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
}
