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
        PlayerManager.currentWeight = 0; //THIS IS BAD CODE YO FIX LATER ME!
    }



    void UpdateInventory()
    {
        int metalCount = PlayerManager.inventory["Metal"];
        int woodCount = PlayerManager.inventory["Wood"];
        int plasticCount = PlayerManager.inventory["Plastic"];
        text.text = $"Metal: {metalCount} \nWood: {woodCount} \nPlastic: {plasticCount}";
    }

    /// <summary>
    /// Ran when the Continue button is pressed
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Ran when the recycle button is pressed
    /// </summary>
    public void Recycle()
    {

        PlayerManager.inventory["Metal"] += PlayerManager.currentDayTrash["Metal"];
        PlayerManager.inventory["Wood"] += PlayerManager.currentDayTrash["Wood"];
        PlayerManager.inventory["Plastic"] += PlayerManager.currentDayTrash["Plastic"];
        UpdateInventory();

        
        continueButton.SetActive(true);
        recycleButton.SetActive(false);

        

    }
}
