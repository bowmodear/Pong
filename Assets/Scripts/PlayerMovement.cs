using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private float currentInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player1.Enable();

        playerInput.Player1.Move.performed += context => currentInput = context.ReadValue<float>();
        playerInput.Player1.Move.canceled += context => currentInput = 0f;
    }

    public float CurrentInput()
    {
        return currentInput;
    }
}
