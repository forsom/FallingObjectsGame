using System.ComponentModel;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterList;
    public int selectedCharacterIndex;
    [SerializeField] private GameController gameController;
    private void Start()
    {
        SelectedCharacter();
    }
    private void SelectedCharacter()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelecterCharacter", 0);
        foreach (GameObject character in characterList)
        {
            character.SetActive(false);
        }
        GameObject activeCharacter = characterList[selectedCharacterIndex];
        activeCharacter.SetActive(true);
        if (gameController != null)
        {
            PlayerMovement newPlayerMovement = activeCharacter.GetComponent<PlayerMovement>();
            gameController.UpdatePlayerReference(newPlayerMovement);
        }
    }
}
