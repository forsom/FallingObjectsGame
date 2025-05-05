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
    }
    public PlayerMovement GetActivePlayerMovement()
    {
        return _activePlayerMovement;
    }
}
