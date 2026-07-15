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
    [SerializeField] private TextMeshProUGUI switchModeText;
    
    private void Start()
    {
        StopGame();
        AdjustSwitchModeText();
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

    private void AdjustSwitchModeText()
    {
        switch (PlayerScoreManager.Instance.playMode)
        {
            case PlayerScoreManager.PlayMode.PlayerVsPlayer:
                switchModeText.text = "Player VS Player";
                break;
            case PlayerScoreManager.PlayMode.PlayerVsAi:
                switchModeText.text = "Player VS Ai";
                break;
            case PlayerScoreManager.PlayMode.AiVsAi:
                switchModeText.text = "Ai VS Ai";
                break;
        }
    }
    
    public void OnSwitchModeButtonClicked()
    {
        PlayerScoreManager.Instance.SwitchMode();
        AdjustSwitchModeText();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
