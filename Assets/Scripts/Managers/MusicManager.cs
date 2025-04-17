using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool _isMusicEnabled = true;
    public static MusicManager _instance;
    private AudioSource _audioSource;
    private const string MusicEnabledKey = "MusicEnabled";

    private void Awake()
    {
        InitializeSingleton();
        GetMusicSettings();
    }
    private void Update()
    {
        UpdateMusicState();
    }
    public void ToggleMusic()
    {
        _isMusicEnabled = !_isMusicEnabled;
        SetMusicSettings();
    }
    private void InitializeSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource != null)
            {
                _audioSource.volume = 0.5f;
            }
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
        if (_audioSource == null)
        {
            return;
        }
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
    // Метод для збереження налаштувань музики та конвертації Bool в Int,тому що PlayerPrefs зберігає тільки Int,Float,String
    private void SetMusicSettings()
    {
        PlayerPrefs.SetInt(MusicEnabledKey, _isMusicEnabled ? 1 : 0); // якщо змінна _isMusicEnabled == true то присвоюємо значення ключа MusicEnabledKey 1,якщо _isMusicEnabled == false присвоюємо значення ключа 0.
        PlayerPrefs.Save();
    }
    // Метод для отримання налаштувань користувача та конвертація int ключа назад в bool
    private void GetMusicSettings()
    {
        _isMusicEnabled = PlayerPrefs.GetInt(MusicEnabledKey, 1) == 1; // конвертуємо Int ключ в Bool назад і присвоюємо змінній _isMusicEnabled булеве значення,якщо ключ MusicEnabledKey 1 відбувається наступна перевірка 1 == 1 повертає true,якщо ключ 0,а 0 == 1 поверне false
        UpdateMusicState();
    }
}
