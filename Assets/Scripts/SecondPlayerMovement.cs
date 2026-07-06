using UnityEngine;
using UnityEngine.InputSystem;

public class SecondPlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private float currentInput;
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player2.Enable();

        playerInput.Player2.Move.performed += context => currentInput = context.ReadValue<float>();
        playerInput.Player2.Move.canceled += context => currentInput = 0f;
    }

    public float CurrentInput()
    {
        return currentInput;
    }
}