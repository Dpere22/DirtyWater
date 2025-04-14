using System;

public class SoundEvents
{
    public event Action<string> OnPlaySoundEffect;

    public void PlaySoundEffect(string soundName)
    {
        OnPlaySoundEffect?.Invoke(soundName);
    }
}
