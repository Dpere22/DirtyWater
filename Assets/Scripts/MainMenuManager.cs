using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit();
    }
}
