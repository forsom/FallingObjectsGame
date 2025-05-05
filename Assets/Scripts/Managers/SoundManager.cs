using UnityEngine;

public enum SoundType
{
    HIT,
    BUTTONCLICK,
    BUTTONHOVER,
    GAMEOVER,
    BOMBEXPLOSION,
    COINPICKUP,
    HEARTPICKUP
}
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static bool isSoundEnable = true;
    [SerializeField] private AudioClip[] _soundList;
    private static SoundManager instance;
    private const string SoundEnabledKey = "SoundEnabled";
    private AudioSource _audioSource;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(transform.root.gameObject);
        GetSoundSettings();
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        if (!isSoundEnable)
        {
            return;
        }
        switch (sound)
        {
            case SoundType.HIT:
                instance._audioSource.pitch = 4;
                break;
            case SoundType.BUTTONHOVER:
                instance._audioSource.volume = 0.5f;
                instance._audioSource.pitch = 3;
                break;
            case SoundType.BOMBEXPLOSION:
                instance._audioSource.volume = 0.4f;
                instance._audioSource.pitch = 1.1f;
                break;
            case SoundType.HEARTPICKUP:
                instance._audioSource.pitch = 1.5f;
                break;
            default:
                instance._audioSource.pitch = 1;
                break;
        }
        instance._audioSource.PlayOneShot(instance._soundList[(int)sound], volume);
    }
    public static void ToggleSound()
    {
        isSoundEnable = !isSoundEnable;
        instance.SetSoundSettings();
    }
    public void ButtonClickSound()
    {
        PlaySound(SoundType.BUTTONCLICK);
    }
    public void ButtonHover()
    {
        PlaySound(SoundType.BUTTONHOVER);
    }
    // Метод для збереження налаштувань музики та конвертації Bool в Int,тому що PlayerPrefs зберігає тільки Int,Float,String
    private void SetSoundSettings()
    {
        PlayerPrefs.SetInt(SoundEnabledKey, isSoundEnable ? 1 : 0); // якщо змінна _isSoundEnabled == true то присвоюємо значення ключа SoundEnabledKey 1,якщо _isSoundEnabled == false присвоюємо значення ключа 0.
        PlayerPrefs.Save();
    }
    // Метод для отримання налаштувань користувача та конвертація int ключа назад в bool
    private void GetSoundSettings()
    {
        isSoundEnable = PlayerPrefs.GetInt(SoundEnabledKey, 1) == 1; // конвертуємо Int ключ в Bool назад і присвоюємо змінній _isSoundEnabled булеве значення,якщо ключ SoundEnabledKey 1 відбувається наступна перевірка 1 == 1 повертає true,якщо ключ 0,а 0 == 1 поверне false
    }
}