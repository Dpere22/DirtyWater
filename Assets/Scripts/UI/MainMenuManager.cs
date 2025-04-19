using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject firstButton;
        public event Action OnMainMenuClicked;

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
        public void StartGame()
        {
            OnMainMenuClicked?.Invoke();
            //SceneManager.LoadScene(1);
        }

        public void EndGame()
        {
            Debug.Log("Game Over");
            Application.Quit();
        }
    }
}
