using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource mainAudio;
    [SerializeField] private AudioSource dieAudio;
    [SerializeField] private AudioSource loseAudio;
    [SerializeField] private AudioSource winAudio;
    [SerializeField] private AudioSource throwAudio;

    public void PlayMainAudio()
    {
        mainAudio.Play();
    }

    public void PlayDieAudio()
    {
        dieAudio.Play();
    }

    public void PlayLoseAudio()
    {
        loseAudio.Play();
    }

    public void PlayWinAudio()
    {
        winAudio.Play();
    }

    public void PlayThrowAudio()
    {
        throwAudio.Play();
    }

    public void ResetAudio()
    {
        winAudio.Stop();
        loseAudio.Stop();
        dieAudio.Stop();
        throwAudio.Stop();
    }
}
