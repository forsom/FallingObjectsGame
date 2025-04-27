using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterList;
    public int selectedCharacterIndex;
    private void Start()
    {
        SelectedCharacter();
    }
    private void SelectedCharacter()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelecterCharacter",1);
        foreach (GameObject character in characterList)
        {
            character.SetActive(false);
        }
        characterList[selectedCharacterIndex].SetActive(true);
    }
}
