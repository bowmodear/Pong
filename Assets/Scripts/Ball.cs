using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BallAudio ballAudio;
    [SerializeField] private float initialVelocity;
    [SerializeField] private float maxCollisionAngle = 45f;
    [SerializeField] private float newVelocity;
    [SerializeField] private float minVerticalVelocity;

    private void Start()
    {
        newVelocity = initialVelocity;
    }
    
    private void FixedUpdate()
    {
        Vector2 currentVelocity = rb.linearVelocity;
        
        if (currentVelocity.magnitude > 0 && Mathf.Abs(currentVelocity.y) < minVerticalVelocity)
        {
            float sign = (currentVelocity.y >= 0) ? 1f : -1f;
            currentVelocity.y = minVerticalVelocity * sign;

            rb.linearVelocity = currentVelocity.normalized * currentVelocity.magnitude;
        }
    }

    private float SetVelocity(float difficultyVelocity)
    {
        newVelocity = Mathf.Min(newVelocity + difficultyVelocity, 30f);
        return newVelocity;
    }

    public void BallDirection()
    {
        bool isRight = Random.value >= 0.5f;
        float xVelocity = isRight? 1f : -1f;  //This is so we can make it move every time horizontally with a consistent speed, if it was a Range it might be equal to 0 which won't make it move.
        
        float yVelocity = Random.Range(-1f, 1f);

        rb.linearVelocity = new Vector2(xVelocity , yVelocity) * initialVelocity;
    }

    public bool IsMovingRight()
    {
        if (rb.linearVelocity.x > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    private void AdjustAngle(PlayerTag playerTag, Collision2D collision)
    {
        Vector2 median = Vector2.zero;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            median += contact.point;
            //Debug.DrawRay(contact.point, Vector3.right, Color.red, 2f);
        }
        median /= collision.contactCount;
        //Debug.DrawRay(median, Vector3.right, Color.cyan, 2f);
        //Debug.Break();
        
        //Calculate relative distance from center (between -1 and 1)
        float absoluteDistanceFromCenter = median.y - transform.position.y;
        float relativeDistanceFromCenter = absoluteDistanceFromCenter * 2 / playerTag.transform.localScale.y; //(Paddle Height)
        
        //Calculate the rotations using quaternions
        int angleSign = (playerTag.GetComponent<Player>()?.IsLeftPaddle() == true) ? 1 : -1;
        float angle = relativeDistanceFromCenter * maxCollisionAngle * angleSign;
        Quaternion rot =  Quaternion.AngleAxis(angle, Vector3.forward);
        //Debug.DrawRay(median, Vector3.forward, Color.green,2f);
        
        //Calculate direction / velocity
        Vector2 dir = (playerTag.GetComponent<Player>()?.IsLeftPaddle() == true) ? Vector2.right : Vector2.left;
        Vector2 velocity = rot * dir * rb.linearVelocity.magnitude;
        rb.linearVelocity = velocity;
        //Debug.DrawRay(median, velocity, Color.yellow,2f);
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
            rb.linearVelocity = rb.linearVelocity.normalized * SetVelocity(difficultyVelocity:1.5f);  //Normalize makes the velocity equal to 1 while maintaining the direction then multiplying by the new velocity
            AdjustAngle(playerTag, collision);
        }

        if (collision.gameObject.TryGetComponent<WallTag>(out var wallTag))
        {
            ballAudio.PlayWallHitSound();
        }
    }
}
