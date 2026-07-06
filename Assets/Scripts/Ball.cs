using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialVelocity;

    private void Start()
    {
        BallDirection();
    }

    private void BallDirection()
    {
        float xVelocity = -1f;
        bool isRight = UnityEngine.Random.value >= 0.5f;

        if (isRight)
        {
            xVelocity = 1f;
        }
        
        float yVelocity = UnityEngine.Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(xVelocity , yVelocity) * initialVelocity;
    }
}
