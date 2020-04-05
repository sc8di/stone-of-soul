using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    private NetworkService _network;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicFirstSource;
    [SerializeField] private AudioSource musicSecondSource;
    [SerializeField] private AudioClip[] clips;

    private AudioSource _firstMusic;
    private AudioSource _secondMusic;

    public float crossFadeRate = 1.5f;
    private bool _crossFading;

    private float _musicVolume;
    private int _currentClipNumber = -1;
    private bool toggleMusic = true;

    public float musicVolume
    {
        get { return _musicVolume; }
        set
        {
            _musicVolume = value;
            if (musicFirstSource != null && !_crossFading)
            {
                musicFirstSource.volume = _musicVolume;
                musicSecondSource.volume = _musicVolume;
            }
        }
    }

    public bool musicMute
    {
        get
        {
            if (musicFirstSource != null)
            {
                return musicFirstSource.mute;
            }

            return false;
        }
        set
        {
            if (musicFirstSource != null)
            {
                musicFirstSource.mute = value;
                musicSecondSource.mute = value;
            }
        }
    }

    public float soundVolume
    {
        get { return AudioListener.volume; }
        set { AudioListener.volume = value; }
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    public void Startup(NetworkService service)
    {
        Debug.Log("Audio manager starting...");

        musicFirstSource.ignoreListenerVolume = true;
        musicFirstSource.ignoreListenerPause = true;
        musicSecondSource.ignoreListenerVolume = true;
        musicSecondSource.ignoreListenerPause = true;

        soundVolume = 1;
        musicVolume = .1f;

        _network = service;

        musicFirstSource.clip = clips[0];

        _firstMusic = musicFirstSource;
        _secondMusic = musicSecondSource;

        status = ManagerStatus.Started;
    }

    public void PlaySound(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }

    public void NextMusic()
    {
        if (!toggleMusic) toggleMusic = true;
        
        if (_currentClipNumber >= clips.Length - 1) _currentClipNumber = 0;
        else _currentClipNumber++;
        PlayMusic(Resources.Load("Music/" + clips[_currentClipNumber].name) as AudioClip);
    }

    public void PreviousMusic()
    {
        if (!toggleMusic) toggleMusic = true;
        
        if (_currentClipNumber <= 0) _currentClipNumber = clips.Length - 1;
        else _currentClipNumber--;
        PlayMusic(Resources.Load("Music/" + clips[_currentClipNumber].name) as AudioClip);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (_crossFading)
        {
            return;
        }

        StartCoroutine(CrossFadeMusic(clip));
    }

    private void FixedUpdate()
    {
        if (toggleMusic && !_firstMusic.isPlaying && !_secondMusic.isPlaying)
        {
            NextMusic();
        }
    }

    private IEnumerator CrossFadeMusic(AudioClip clip)
    {
        _crossFading = true;

        _secondMusic.clip = clip;
        _secondMusic.volume = 0;
        _secondMusic.Play();

        float scaledRate = crossFadeRate * _musicVolume;
        while (_firstMusic.volume > 0)
        {
            _firstMusic.volume -= scaledRate * Time.deltaTime;
            _secondMusic.volume += scaledRate * Time.deltaTime;
            yield return null;
        }

        AudioSource temp = _firstMusic;
        _firstMusic = _secondMusic;
        _firstMusic.volume = _musicVolume;

        _secondMusic = temp;
        _secondMusic.Stop();

        _crossFading = false;
    }

    public void StopMusic()
    {
        _firstMusic.Stop();
        _secondMusic.Stop();
        toggleMusic = false;
    }
}
