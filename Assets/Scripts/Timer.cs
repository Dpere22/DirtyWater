using UnityEngine;
using System;
using Events;

public class Timer : MonoBehaviour
{
    private float TimerDuration { get; set; } // Duration for the timer
    private float _elapsedTime;
    private bool _isRunning;
    private bool _isPaused;

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
        GameEventsManager.Instance.InputEvents.PauseGameAction += PauseTimer;
        GameEventsManager.Instance.InputEvents.ResumeGameAction += ResumeTimer;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.InputEvents.PauseGameAction -= PauseTimer;
        GameEventsManager.Instance.InputEvents.ResumeGameAction -= ResumeTimer;
    }

    /// <summary>
    /// Starts the timer for a given duration
    /// </summary>
    public void StartTimer(float duration)
    {
        OnTimerStart?.Invoke();
        TimerDuration = duration;
        _elapsedTime = 0f;
        _isRunning = true;
        _isPaused = false;
    }

    /// <summary>
    /// Pauses the timer
    /// </summary>
    private void PauseTimer()
    {
        if (_isRunning) _isPaused = true;
    }

    /// <summary>
    /// Resumes the timer if paused
    /// </summary>
    private void ResumeTimer()
    {
        if (_isRunning) _isPaused = false;
    }

    /// <summary>
    /// Stops and resets the timer
    /// </summary>
    private void StopTimer()
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