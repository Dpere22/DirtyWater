using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuAnim : MonoBehaviour
{
    private static readonly int FadeOut = Animator.StringToHash("fadeOut");
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private Animator animator;
    private MainMenuManager _mainMenuManager;

    public void Start()
    {
        _mainMenuManager = mainMenu.GetComponent<MainMenuManager>();
        _mainMenuManager.OnMainMenuClicked += HandleClicked;
    }

    private void HandleClicked()
    {
        animator.SetTrigger(FadeOut);
    }
    private void OnCompleteAnim()
    {
        SceneManager.LoadScene(1);
    }
}
