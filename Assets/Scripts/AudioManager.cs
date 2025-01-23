using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; 

    [Header("Audio Sources")]
    public AudioSource musicSource;      
    public AudioSource sfxSourcePrefab;   
    public int maxSfxSources = 10;          

    [Header("Audio Clips")]
    public AudioClip backgroundMusic; 

    private List<AudioSource> sfxSources;  

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
        sfxSources = new List<AudioSource>();
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic(backgroundMusic, true, 0.4f);
    }

    public void PlayMusic(AudioClip musicClip, bool loop = true, float volume = 1f)
    {
        if (musicSource != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = loop;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip, float volume = 1f)
    {
        AudioSource sfxSource = GetAvailableSfxSource();
        if (sfxSource != null)
        {
            sfxSource.clip = sfxClip;
            sfxSource.volume = volume;
            sfxSource.Play();
        }
    }

    private AudioSource GetAvailableSfxSource()
    {
        foreach (var source in sfxSources)
        {
            if (!source.isPlaying)
                return source;
        }

        if (sfxSources.Count < maxSfxSources)
        {
            AudioSource newSource = Instantiate(sfxSourcePrefab, transform);
            sfxSources.Add(newSource);
            return newSource;
        }

        return null;
    }
}
