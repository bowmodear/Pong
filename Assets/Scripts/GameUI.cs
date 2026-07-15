using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;
    [SerializeField] private Ball ball;
    [SerializeField] private Player player1;
    [SerializeField] private SecondPlayer player2;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private PlayerScoreManager playerScoreManager;
    [SerializeField] private TextMeshProUGUI volumeValueText;

    private void Start()
    {
        StopGame();
    }

    private void StopGame()
    {
        menuObject.SetActive(true);
        if(player1 != null) player1.enabled = false;
        if (player2 != null) player2.enabled = false;
        if(ball != null) ball.StopBall();
    }

    private void ResumeGame()
    {
        menuObject.SetActive(false);
        if(player1 != null) player1.enabled = true;
        if(player2 != null) player2.enabled = true;
        if(ball != null) ball.BallDirection();
    }
    
    public void OnStartGameButtonClicked()
    {
        ResumeGame();
        playerScoreManager.ResetScore();
    }

    public void OnGameEnd(int winnerId)
    {
        StopGame();
        winText.text = $"Player {winnerId} won!";
    }
    
    public void OnVolumeValueChanged(float volume)
    {
        AudioListener.volume = volume;
        volumeValueText.text = $"{Mathf.RoundToInt(volume * 100)}%";
    }
}
