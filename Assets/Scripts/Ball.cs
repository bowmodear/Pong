using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float initialVelocity;

    private void Start()
    {
        BallDirection();
    }

    private float SetVelocity(float difficultyVelocity)
    {
        initialVelocity = Mathf.Min(initialVelocity + difficultyVelocity, 15f);
        return initialVelocity;
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

    public void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;  //To make it stop spinning
        
        transform.position = Vector3.zero;
        initialVelocity = 5f;

        StartCoroutine(LaunchAfterDelay());
    }

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        BallDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerTag>(out var playerTag))
        {
            SetVelocity(difficultyVelocity:0.5f);
            rb.linearVelocity = rb.linearVelocity.normalized * initialVelocity;  //Normalize makes the velocity equal to 1 while maintaining the direction then multiplying by the new velocity
        }
    }
}
