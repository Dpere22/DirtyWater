using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
}
