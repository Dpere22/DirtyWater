using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecycleUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {
        int metalCount = PlayerManager.inventory["Metal"];
        int woodCount = PlayerManager.inventory["Wood"];
        int plasticCount = PlayerManager.inventory["Plastic"];
        text.text = $"Metal: {metalCount} \nWood: {woodCount} \nPlastic: {plasticCount}";
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene("Main");
    }
}
