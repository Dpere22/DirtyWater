using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecycleUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject recycleButton;
    void Start()
    {
        UpdateInventory();
        GameEventsManager.Instance.PlayerManager.CurrentWeight = 0; //THIS IS BAD CODE YO FIX LATER ME!
    }



    void UpdateInventory()
    {
        int metalCount = GameEventsManager.Instance.PlayerManager.Inventory["Metal"];
        int woodCount = GameEventsManager.Instance.PlayerManager.Inventory["Wood"];
        int plasticCount = GameEventsManager.Instance.PlayerManager.Inventory["Plastic"];
        text.text = $"Metal: {metalCount} \nWood: {woodCount} \nPlastic: {plasticCount}";
    }

    /// <summary>
    /// Ran when the Continue button is pressed
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Ran when the recycle button is pressed
    /// </summary>
    public void Recycle()
    {

        GameEventsManager.Instance.PlayerManager.RecycleTrash();
        UpdateInventory();

        
        continueButton.SetActive(true);
        recycleButton.SetActive(false);

        

    }
}
