using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public GameObject[] characterList;
    public int selectedCharacterIndex;
    public InputHandler inputHandler;
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
        if (gameController != null && inputHandler != null)
        {
            PlayerMovement newPlayerMovement = activeCharacter.GetComponent<PlayerMovement>();
            gameController.UpdatePlayerReference(newPlayerMovement);
            inputHandler.SetActivePlayer(newPlayerMovement);
        }
    }
}
