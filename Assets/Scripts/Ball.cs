using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BallAudio ballAudio;
    [SerializeField] private float initialVelocity;
    [SerializeField] private float newVelocity;

    private void Start()
    {
        newVelocity = initialVelocity;
    }

    private float SetVelocity(float difficultyVelocity)
    {
        newVelocity = Mathf.Min(newVelocity + difficultyVelocity, 15f);
        return newVelocity;
    }

    public void BallDirection()
    {
        bool isRight = Random.value >= 0.5f;
        float xVelocity = isRight? 1f : -1f;  //This is so we can make it move every time horizontally with a consistent speed, if it was a Range it might be equal to 0 which won't make it move.
        
        float yVelocity = Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(xVelocity , yVelocity) * initialVelocity;
    }

    public void ResetBall()
    {
        StopBall();

        StartCoroutine(LaunchAfterDelay());
    }

    public void StopBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;  //To make it stop spinning
        
        transform.position = Vector3.zero;
        newVelocity = initialVelocity;
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
            ballAudio.PlayPaddleHitSound();
            rb.linearVelocity = rb.linearVelocity.normalized * SetVelocity(difficultyVelocity:0.5f);  //Normalize makes the velocity equal to 1 while maintaining the direction then multiplying by the new velocity
        }

        if (collision.gameObject.TryGetComponent<WallTag>(out var wallTag))
        {
            ballAudio.PlayWallHitSound();
        }
    }
}
