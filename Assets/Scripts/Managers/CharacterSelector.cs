using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] InputHandler inputHandler;
    [SerializeField] GameObject[] characterList;
    [SerializeField] private GameController gameController;
    [SerializeField] private BombFallController bombFallController;
    private int _selectedCharacterIndex;
    private GameObject _activeCharacter;
    private PlayerMovement _activePlayerMovement;
    private void Awake()
    {
        SelectedCharacter();
    }
    public void SelectedCharacter()
    {
        _selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject character in characterList)
        {
            character.SetActive(false);
        }

        _activeCharacter = characterList[_selectedCharacterIndex];
        _activeCharacter.SetActive(true);
        _activePlayerMovement = _activeCharacter.GetComponent<PlayerMovement>();
        if (gameController != null && inputHandler != null && bombFallController != null)
        {

            gameController.UpdatePlayerReference(_activePlayerMovement);
            inputHandler.SetActivePlayer(_activePlayerMovement);
        }
        switch (_selectedCharacterIndex)
        {
            case 0:
                _activePlayerMovement.moveSpeed = 10;
                break;
            case 1:
                _activePlayerMovement.moveSpeed = 12;
                break;
            case 2:
                _activePlayerMovement.moveSpeed = 14;
                break;
            case 3:
                _activePlayerMovement.moveSpeed = 16;
                break;
            case 4:
                _activePlayerMovement.moveSpeed = 18;
                break;
            case 5:
                _activePlayerMovement.moveSpeed = 20;
                break;
        }
    }
    public PlayerMovement GetActivePlayerMovement()
    {
        return _activePlayerMovement;
    }
}
