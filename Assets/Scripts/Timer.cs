using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public float TimerDuration { get; private set; } // Duration for the timer
    private float _elapsedTime = 0f;
    private bool _isRunning = false;
    private bool _isPaused = false;

    public Action OnTimerComplete; // Event that fires when the timer completes
    public Action OnTimerStart;

    private void Update()
    {
        if (_isRunning && !_isPaused)
        {
            _elapsedTime += Time.deltaTime;
            if (_elapsedTime >= TimerDuration)
            {
                StopTimer();
                OnTimerComplete?.Invoke();
            }
        }
    }

    private void Start()
    {
        PauseManager.PauseGameAction += PauseTimer;
        PauseManager.ResumeGameAction += ResumeTimer;
    }

    private void OnDestroy()
    {
        PauseManager.PauseGameAction -= PauseTimer;
        PauseManager.ResumeGameAction -= ResumeTimer;
    }

    /// <summary>
    /// Starts the timer for a given duration
    /// </summary>
    public void StartTimer(float duration)
    {
        Debug.Log("StartTimer");
        OnTimerStart?.Invoke();
        TimerDuration = duration;
        _elapsedTime = 0f;
        _isRunning = true;
        _isPaused = false;
    }

    /// <summary>
    /// Pauses the timer
    /// </summary>
    public void PauseTimer()
    {
        if (_isRunning) _isPaused = true;
    }

    /// <summary>
    /// Resumes the timer if paused
    /// </summary>
    public void ResumeTimer()
    {
        if (_isRunning) _isPaused = false;
    }

    /// <summary>
    /// Stops and resets the timer
    /// </summary>
    public void StopTimer()
    {
        _isRunning = false;
        _isPaused = false;
        _elapsedTime = 0f;
    }

    /// <summary>
    /// Checks remaining time
    /// </summary>
    public float GetRemainingTime()
    {
        return Mathf.Max(TimerDuration - _elapsedTime, 0f);
    }
}