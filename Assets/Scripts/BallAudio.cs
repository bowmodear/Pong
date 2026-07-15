using UnityEngine;

public class BallAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    [SerializeField] private AudioClip paddleHitSound;
    [SerializeField] private AudioClip wallHitSound;
    
    public void PlayPaddleHitSound()
    {
        audioSource.PlayOneShot(paddleHitSound);
    }

    public void PlayWallHitSound()
    {
        audioSource.PlayOneShot(wallHitSound);
    }
}
