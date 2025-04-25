using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject[] characterPreview;
    public int selectedCharacterIndex; 
    [SerializeField] private TMP_Text Money;
    public static ShopManager instance;
    private void Start()
    {
        CharacterSelection();
    }
    private void Update()
    {
        Money.text = "Coins: " + PlayerPrefs.GetInt("UserCoins", 0);
    }
    private void CharacterSelection()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelecterCharacter", 0);
        foreach (GameObject character in characterPreview)
        {
            character.SetActive(false);
        }
        characterPreview[selectedCharacterIndex].SetActive(true);
    }
    public void NextButton()
    {
        characterPreview[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex++;
        if (selectedCharacterIndex == characterPreview.Length)
        {
            selectedCharacterIndex = 0;
        }
        characterPreview[selectedCharacterIndex].SetActive(true);
        PlayerPrefs.SetInt("SelecterCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
    }
     public void PreviousButton()
    {
        characterPreview[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex--;
        if (selectedCharacterIndex == -1)
        {
            selectedCharacterIndex = characterPreview.Length - 1;
        }
        characterPreview[selectedCharacterIndex].SetActive(true);
        PlayerPrefs.SetInt("SelecterCharacter", selectedCharacterIndex);
        PlayerPrefs.Save();
    }
}

