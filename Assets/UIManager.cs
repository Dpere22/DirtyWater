using System.Globalization;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;

    [SerializeField] private Timer timer;
    

    private bool _hasStarted;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    private void Update()
    {
        if (!_hasStarted) return;
        float time = (timer.GetRemainingTime());
        tm.text = $"Time Left: {time:F2}";
    }

    private void Start()
    {
        timer.OnTimerStart += EnableTimerText;
    }

    private void EnableTimerText()
    {
        _hasStarted = true;
    }
}
