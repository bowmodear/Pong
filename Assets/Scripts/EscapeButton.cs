using System;
using UnityEngine;

public class EscapeButton : MonoBehaviour
{
    private PlayerInput playerInput;
    public event Action OnEscapePressed;
    
    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Pause.Enable();

        playerInput.Pause.Escape.performed += context => OnEscapePressed?.Invoke();
    }
}
