using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool _isMusicEnabled = true;
    public static MusicManager _instance;
    private AudioSource _audioSource;

    void Awake()
    {
        InitializeSingleton();
    }
    void Update()
    {
        UpdateMusicState();
    }
    public void ToggleMusic()
    {
        _isMusicEnabled = !_isMusicEnabled;
    }
    private void InitializeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.5f;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void UpdateMusicState()
    {
        if (_isMusicEnabled)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Pause();
        }
    }
}
