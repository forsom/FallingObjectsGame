using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private PlayerMovement activePlayerMovement;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Зберігаємо між сценами
    }

    public void SetActivePlayer(PlayerMovement playerMovement)
    {
        activePlayerMovement = playerMovement;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (activePlayerMovement != null)
        {
            activePlayerMovement.Move(context);
        }
    }
}