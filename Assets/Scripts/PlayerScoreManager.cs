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
    
    [SerializeField] private int player1Score = 0;
    [SerializeField] private int player2Score = 0;

    //This automatically runs when any trigger fires its event
    private void HandleGoal(GoalSide sideHit)
    {
        if (sideHit == GoalSide.Player1)
        {
            player2Score++;
            player2Text.text = player2Score.ToString();
        }
        else if (sideHit == GoalSide.Player2)
        {
            player1Score++;
            player1Text.text = player1Score.ToString();
        }

        ball.ResetBall();
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
}
