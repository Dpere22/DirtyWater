using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public void OnCreditsEnd()
    {
        SceneManager.LoadScene(0);
    }
}
