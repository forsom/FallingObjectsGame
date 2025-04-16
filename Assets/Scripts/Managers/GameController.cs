using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private Image ScrImg;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text ScrTxt;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;

    [SerializeField] private Sprite onButton;
    [SerializeField] private Sprite offButton;

    void Awake()
    {
        playerMovement.PlayerDied += ActivateGameOverScreen; // підписка на подію
        UpdateButtonSprite(musicButton, MusicManager._instance._isMusicEnabled);
        UpdateButtonSprite(soundButton, SoundManager.isSoundEnable);
    }
    void Update()
    {
        PauseMenu();
        Score();
    }
    void ActivateGameOverScreen()
    {
        SoundManager.PlaySound(SoundType.GAMEOVER);
        GameOverCanvas.gameObject.SetActive(true);
        ScrImg.gameObject.SetActive(false);
        TimerText.text = "Record: " + Math.Round(Time.timeSinceLevelLoad);
        playerMovement.PlayerDied -= ActivateGameOverScreen;
    }
    void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu == null)
            {
                return;
            }
            if (settingsMenu.activeSelf)
            {
                settingsMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                settingsMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }


    }
    public void RetryClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackClick()
    {
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void MenuClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void EnableMusic()
    {
        MusicManager._instance.ToggleMusic();
        UpdateButtonSprite(musicButton, MusicManager._instance._isMusicEnabled);
    }
    public void EnableSound()
    {
        SoundManager.isSoundEnable = !SoundManager.isSoundEnable;
        UpdateButtonSprite(soundButton, SoundManager.isSoundEnable);
    }
    public void Score()
    {
        if (ScrTxt != null)
        {
            ScrTxt.text = ": " + Math.Round(Time.timeSinceLevelLoad);
        }
    }
    private void UpdateButtonSprite(Button button, bool isEnabled)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (isEnabled) // Якщо музика увімкнена
        {
            buttonImage.sprite = onButton; // Встановлюємо спрайт "увімкнено"
        }
        else // Якщо музика вимкнена
        {
            buttonImage.sprite = offButton; // Встановлюємо спрайт "вимкнено"
        }
    }
}
