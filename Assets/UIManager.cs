using System.Globalization;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void Update()
    {
        tm.text = "Time Left: " + DayManager.GetRemainingTime().ToString(CultureInfo.InvariantCulture);
    }
}
