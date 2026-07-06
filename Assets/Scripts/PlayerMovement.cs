using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private float currentInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        playerInput.Player.Move.performed += context => currentInput = context.ReadValue<float>();
        playerInput.Player.Move.canceled += context => currentInput = 0f;
    }

    public float CurrentInput()
    {
        return currentInput;
    }
}
