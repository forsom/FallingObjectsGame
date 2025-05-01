using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private Image ScrImg;
    [SerializeField] private Image CoinsCount;
    [SerializeField] private TMP_Text TimerText;
    [SerializeField] private TMP_Text ScrTxt;
    [SerializeField] private TMP_Text BestScrTxt;
    [SerializeField] private TMP_Text CoinsTxt;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundButton;

    [SerializeField] private Sprite onButton;
    [SerializeField] private Sprite offButton;
    private PlayerMovement playerMovement;
    private float _bestScore;
    private float _currentScore;
    private const string BestScoreKey = "UserBestScore";

    void Awake()
    {
        FindAndSetupPlayer();
        UpdateButtonSprite(musicButton, MusicManager._instance._isMusicEnabled);
        UpdateButtonSprite(soundButton, SoundManager.isSoundEnable);
    }
    void Update()
    {
        PauseMenu();
        ScoreAndCoins();
    }
    void ActivateGameOverScreen()
    {
        BestScore();
        SoundManager.PlaySound(SoundType.GAMEOVER);
        GameOverCanvas.gameObject.SetActive(true);
        ScrImg.gameObject.SetActive(false);
        CoinsCount.gameObject.SetActive(false);
        TimerText.text = "Score: " + Mathf.Round(Time.timeSinceLevelLoad);
        BestScrTxt.text = "Best score: " + Mathf.Round(_bestScore);
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
        SoundManager.ToggleSound();
        UpdateButtonSprite(soundButton, SoundManager.isSoundEnable);
    }
    public void ScoreAndCoins()
    {
        if (ScrTxt != null && CoinsTxt != null)
        {
            ScrTxt.text = ": " + Mathf.Round(Time.timeSinceLevelLoad);
            CoinsTxt.text = ": " + CoinManager.instance.coinsCount;
        }
    }
    private void BestScore()
    {
        _currentScore = Mathf.Round(Time.timeSinceLevelLoad);
        _bestScore = PlayerPrefs.GetFloat(BestScoreKey, 0);
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetFloat(BestScoreKey, _currentScore);
            PlayerPrefs.Save();
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
    public void UpdatePlayerReference(PlayerMovement newPlayerMovement)
    {
        if (playerMovement != null)
        {
            playerMovement.PlayerDied -= ActivateGameOverScreen;
        }
        playerMovement = newPlayerMovement;
        if (playerMovement != null)
        {
            playerMovement.PlayerDied += ActivateGameOverScreen;
        }
    }
    private void FindAndSetupPlayer()
    {
        // Знаходимо активного гравця за тегом
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerMovement = playerObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayerDied += ActivateGameOverScreen;
            }
        }
    }
}
