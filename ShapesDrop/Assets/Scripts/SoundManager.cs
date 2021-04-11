using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    public AudioClip[] ImpactEffect;

    public AudioClip DeadSoundEffect;
    public AudioClip EndGameSoundEffect;

    public static SoundManager Instance = null;

    public AudioClip MenuMusic;
    public AudioClip[] GameMusic;

    public bool SoundOn { get; set; } = true;
    public bool MusicOn { get; set; } = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ToggleGameSounds()
    {
        if(SoundOn)
        {
            SoundOn = false;
            EffectsSource.mute = true;
        }
        else
        {
            SoundOn = true;
            EffectsSource.mute = false;
        }
    }

    public void ToggleGameMusic()
    {
        if (MusicOn)
        {
            MusicOn = false;
            MusicSource.mute = true;
        }
        else
        {
            MusicOn = true;
            MusicSource.mute = false;
        }
    }

    public void PlayMenuMusic()
    {
        MusicSource.clip = MenuMusic;
        MusicSource.Play();
    }

    public void PlayGameMusic()
    {
        //int idx = Random.Range(0, GameMusic.Length);
        //MusicSource.clip = GameMusic[0];
        MusicSource.Play();
    }

    public void PlayEndGameSound()
    {
        EffectsSource.clip = EndGameSoundEffect;
        EffectsSource.Play();
    }

    public void PlayImpactEffect()
    {
        int index = Random.Range(0, ImpactEffect.Length);

        EffectsSource.clip = ImpactEffect[index];
        EffectsSource.Play();
    }

    public void PlayDeadSound()
    {
        EffectsSource.clip = DeadSoundEffect;
        EffectsSource.Play();
    }
}
