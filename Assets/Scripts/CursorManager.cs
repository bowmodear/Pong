using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Texture2D pressedCursor;
    [SerializeField] private GameAudio audioSource;
    private static GameAudio _audioSource;
    
    [SerializeField] private Vector2 hotspot =  Vector2.zero;
    [SerializeField] private bool isHovering = false;
    [SerializeField] private static bool _isPressing = false;

    private void Start()
    {
        Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        if(audioSource != null) _audioSource = audioSource;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (!_isPressing)
        {
            Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (!_isPressing)
        {
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressing = true;
        Cursor.SetCursor(pressedCursor, hotspot, CursorMode.Auto);
        _audioSource.PlayClickSound();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressing = false;
        if (isHovering)
        {
            Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        }
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
