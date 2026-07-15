using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip winSound;
    

    public void PlayScoreSound()
    {
        audioSource.PlayOneShot(scoreSound);
    }

    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winSound);
    }
}
