using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    [SerializeField] AudioSource musicSource, sfxSource;

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
        }

        PlayMusic(SoundType.BGMusic);
    }
    public void PlayMusic(SoundType soundType)
    {
        Sound s = Array.Find(musicSounds, x => x.soundType == soundType);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(SoundType soundType)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundType == soundType);
        if (s == null)
        {
            Debug.Log("SFX not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}
    [Serializable]
    public class Sound 
    {
        public SoundType soundType;
        public AudioClip clip;
        
    }
