using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject[] characterPreview;
    public int selectedCharacterIndex = 0;
    public CharacterBluepring[] characters;
    [SerializeField] private TMP_Text _money;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _buyPrice;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _characterShop;
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterSpeed;

    private void Start()
    {
        IsCharacterLocked();
        CharacterSelection();
    }
    private void Update()
    {
        UpdateBuyButtonUI();
        _money.text = "Coins: " + PlayerPrefs.GetInt("UserCoins", 0);
    }
    public void IsCharacterLocked()
    {
        foreach (CharacterBluepring character in characters)
        {
            if (character.price == 0)
            {
                character.isUnlocked = true;
            }
            else
            {
                character.isUnlocked = PlayerPrefs.GetInt(character.name, 0) == 0 ? false : true;
            }
        }
    }
    public void NextButton()
    {
        characterPreview[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= characterPreview.Length)
        {
            selectedCharacterIndex = 0;
        }
        characterPreview[selectedCharacterIndex].SetActive(true);
        CharacterBluepring characterAvailable = characters[selectedCharacterIndex];
        CharacterSelection();
        if (!characterAvailable.isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
    }
    public void PreviousButton()
    {
        characterPreview[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
        {
            selectedCharacterIndex = characterPreview.Length - 1;
        }
        characterPreview[selectedCharacterIndex].SetActive(true);
        CharacterBluepring characterAvailable = characters[selectedCharacterIndex];
        CharacterSelection();
        if (!characterAvailable.isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
    }
    public void BuyCharacter()
    {
        CharacterBluepring characterAvailable = characters[selectedCharacterIndex];
        PlayerPrefs.SetInt(characterAvailable.name, 1);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacterIndex);
        characterAvailable.isUnlocked = true;
        CharacterSelection();
        PlayerPrefs.SetInt("UserCoins", PlayerPrefs.GetInt("UserCoins", 0) - characterAvailable.price);
        PlayerPrefs.Save();
        Debug.Log(selectedCharacterIndex);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void BackButton()
    {
        _characterShop.SetActive(false);
        _mainMenu.SetActive(true);
    }

    private void UpdateBuyButtonUI()
    {
        CharacterBluepring characterAvailable = characters[selectedCharacterIndex];
        if (characterAvailable.isUnlocked)
        {
            _buyButton.gameObject.SetActive(false);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
            _buyPrice.text = "Buy " + characterAvailable.price;
            if (characterAvailable.price <= PlayerPrefs.GetInt("UserCoins", 0))
            {
                _buyButton.interactable = true;
            }
            else
            {
                _buyButton.interactable = false;
            }
        }
    }
    private void CharacterSelection()
    {
        foreach (GameObject character in characterPreview)
        {
            character.SetActive(false);
        }
        characterPreview[selectedCharacterIndex].SetActive(true);
        _characterName.text = characters[selectedCharacterIndex].name;
        _characterSpeed.text = "Speed: " + characters[selectedCharacterIndex].speed;
    }
}

