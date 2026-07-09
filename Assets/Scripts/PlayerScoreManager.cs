using System.Diagnostics;
using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    [Header("Triggers")]
    [SerializeField] private GoalTrigger player1Trigger;
    [SerializeField] private GoalTrigger player2Trigger;
    
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI player1Text;
    [SerializeField] private TextMeshProUGUI player2Text;
    
    [Header("Ball")]
    [SerializeField] private Ball ball;

    [SerializeField] private GameUI gameUI;
    
    [SerializeField] private int player1Score = 0;
    [SerializeField] private int player2Score = 0;
    [SerializeField] private int winScore = 5;

    //This automatically runs when any trigger fires its event
    private void HandleGoal(GoalSide sideHit)
    {
        if (sideHit == GoalSide.Player1)
        {
            AddScore(2);
        }
        else if (sideHit == GoalSide.Player2)
        {
            AddScore(1);
        }
        
        CheckWin();
    }

    //Subscribes to the event when this script is run
    private void OnEnable()
    {
        if (player1Trigger != null) player1Trigger.OnBallEntered += HandleGoal;
        if (player2Trigger != null) player2Trigger.OnBallEntered += HandleGoal;
    }

    //Unsubscribe from the event when the script turns off
    private void OnDisable()
    {
        if (player1Trigger != null) player1Trigger.OnBallEntered -= HandleGoal;
        if (player2Trigger != null) player2Trigger.OnBallEntered -= HandleGoal;
    }

    private void AddScore(int playerId)
    {
        switch(playerId)
        {
            case 1:
                player1Score++;
                player1Text.text = player1Score.ToString();
                break;
            case 2:
                player2Score++;
                player2Text.text = player2Score.ToString();
                break;
        }
    }

    public void ResetScore()
    {
        player1Score = 0;
        player2Score = 0;
        player1Text.text = player1Score.ToString();
        player2Text.text = player2Score.ToString();
    }

    private void CheckWin()
    {
        int winnerId = player1Score == winScore ? 1 : player2Score == winScore ? 2 : 0;

        if (winnerId != 0)
        {
            gameUI.OnGameEnd(winnerId);
        }
        else
        {
            ball.ResetBall();
        }
    }
}
