using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    HIT,
    BUTTONCLICK,
    BUTTONHOVER,
    GAMEOVER,
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static bool isSoundEnable = true;
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        if (!isSoundEnable)
        {
            return;
        }
        if (sound == SoundType.HIT)
        {
            instance.audioSource.pitch = 4;
        }
        else if (sound == SoundType.BUTTONHOVER)
        {
            instance.audioSource.volume = 0.5f;
            instance.audioSource.pitch = 3;
        }
        else
        {
            instance.audioSource.pitch = 1;
        }
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
    public static void ToggleSound()
    {
        isSoundEnable = !isSoundEnable;
    }
    public void ButtonClickSound()
    {
        PlaySound(SoundType.BUTTONCLICK);
    }
    public void ButtonHover()
    {
        PlaySound(SoundType.BUTTONHOVER);
    }
}
