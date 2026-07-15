using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float aiDeadZone;
    [SerializeField] private int direction = 0;
    [SerializeField] private LayerMask layerMask;
    
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;
    


    private void Update()
    {
        if (PlayerScoreManager.Instance.playMode == PlayerScoreManager.PlayMode.AiVsAi)
        {
            AiMove();
        }
        else
        {
            PlayerMove();
        }
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
    
    private void AiMove()
    {
        Vector2 ballPos = PlayerScoreManager.Instance.ball.transform.position;
        float moveSpeedMultiplier = 1f;
        float moveDistance = moveSpeed * Time.deltaTime;
        if (Mathf.Abs(ballPos.y - transform.position.y) > aiDeadZone && !PlayerScoreManager.Instance.ball.IsMovingRight())
        {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

        if (Random.value < 0.01f)
        {
            moveSpeedMultiplier = Random.Range(0.5f, 1.5f);
        }
        Vector2 moveDir =  new Vector2(0f, direction);
        bool canMove = !Physics2D.BoxCast(transform.position, Vector2.one, 0, moveDir, moveDistance, layerMask);
        if (canMove)
        {
            Vector3 newPosition = transform.position + (Vector3)(moveSpeedMultiplier * moveDistance * moveDir);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit, topLimit);
            transform.position = newPosition;
        }
    }

    public bool IsLeftPaddle()
    {
        return true;
    }
}
