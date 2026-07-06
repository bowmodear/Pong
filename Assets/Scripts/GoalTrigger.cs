using System;
using UnityEngine;

public enum GoalSide
{
    Player1,
    Player2
}

public class GoalTrigger : MonoBehaviour
{
    public GoalSide goalSide;  //This is a dropdown in the inspector to specify which side this trigger represents
    public event Action<GoalSide> OnBallEntered;  //This passes the enum to whoever is listening
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<BallTag>(out var ballTag)) //This condition is trying to see if the object that have the component BallTag entered this trigger.
        {
            OnBallEntered?.Invoke(goalSide);  //Fire the event and passes the side along with it
        }
    }
}
