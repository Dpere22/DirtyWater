using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameFinishedUI : MonoBehaviour
    {
        [SerializeField] private GameObject firstButton;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void OnEnable()
        {
            StartCoroutine(SelectButtonAfterDelay());
        }
        private IEnumerator SelectButtonAfterDelay()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(firstButton);
        }

        public void MainMenuButton()
        {
            SceneManager.LoadScene(0);
        }
    }
}
