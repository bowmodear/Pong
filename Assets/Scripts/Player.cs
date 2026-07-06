using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;
    


    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float moveDistance = moveSpeed * Time.deltaTime;
        float inputVector = playerMovement.CurrentInput();
        if (inputVector == 0f) return;
        Vector2 moveDir =  new Vector2(0f, inputVector);
        bool canMove = !Physics2D.BoxCast(transform.position, Vector2.one, 0, moveDir, moveDistance, layerMask);
        if (canMove)
        {
            Vector3 newPosition = transform.position + (Vector3)(moveDir * moveDistance);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit, topLimit);
            transform.position = newPosition;
        }


    }
}
