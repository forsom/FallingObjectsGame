using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterList;
    public int selectedCharacterIndex;
    public InputHandler inputHandler;
    [SerializeField] private GameController gameController;
    [SerializeField] private BombFallController bombFallController;
    private GameObject _activeCharacter;
    private PlayerMovement _activePlayerMovement;
    private void Awake()
    {
        SelectedCharacter();
    }
    public void SelectedCharacter()
    {
        selectedCharacterIndex = PlayerPrefs.GetInt("SelecterCharacter", 0);
        foreach (GameObject character in characterList)
        {
            character.SetActive(false);
        }

        _activeCharacter = characterList[selectedCharacterIndex];
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
