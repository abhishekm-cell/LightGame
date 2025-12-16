using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    public bool IsMusicOn { get; private set; } = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayMusic(SoundType.BGMusic);
    }

    public void PlayMusic(SoundType soundType)
    {
        if (!IsMusicOn) return;

        Sound s = Array.Find(musicSounds, x => x.soundType == soundType);
        if (s == null)
        {
            Debug.Log("Music not found");
            return;
        }

        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetMusic(bool on)
    {
        IsMusicOn = on;

        if (on)
        {
            musicSource.UnPause();
        }
        else
        {
            musicSource.Pause();
        }
    }

    public void PlaySFX(SoundType soundType)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundType == soundType);
        if (s == null)
        {
            Debug.Log("SFX not Found");
            return;
        }

        sfxSource.PlayOneShot(s.clip);
    }

    internal void StopSfx()
    {
        sfxSource.Stop();
    }
}


    [Serializable]
    public class Sound 
    {
        public SoundType soundType;
        public AudioClip clip;
        
    }
