using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScoreRoundController : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private BallHealth ballHealth;

    private BallController lastBallToBumpMe;
    public int score;
    public UnityEvent<float> OnScoreAdded;

    private void Awake()
    {
        score = 0;
    }
    private void OnEnable()
    {
        ballController.OnContactWithOtherBallController.AddListener(UpdateLastBallToBumpMe);
    }
    private void OnDisable()
    {
        ballController.OnContactWithOtherBallController.RemoveListener(UpdateLastBallToBumpMe);
        if (lastBallToBumpMe != null)
        {
            ConnectToBallDeath(false);
        }
    }
    private void UpdateLastBallToBumpMe(BallController ballController)
    {
        if(lastBallToBumpMe != null)
        {
            ConnectToBallDeath(false);
        }
        lastBallToBumpMe = ballController;
        ConnectToBallDeath(true);
    }

    private void ConnectToBallDeath(bool value)
    {
        if (value)
        {
            ballHealth.OnDeath.AddListener(lastBallToBumpMe.GetComponent<PlayerScoreRoundController>().AddScore);
        }
        else
        {
            ballHealth.OnDeath.RemoveListener(lastBallToBumpMe.GetComponent<PlayerScoreRoundController>().AddScore);
        }
    }
    private void AddScore(Vector3 temp)
    {
        score += 1;
        OnScoreAdded?.Invoke(score);
    }
}
