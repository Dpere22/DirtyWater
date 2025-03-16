using System;

namespace Events
{
    public class InputEvents
    {
        public event Action Pause;

        public void PausePressed()
        {
            Pause?.Invoke();
        }

    }
}