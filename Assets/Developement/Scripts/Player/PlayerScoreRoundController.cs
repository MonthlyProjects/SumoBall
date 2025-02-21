using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScoreRoundController : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private BallHealth ballHealth;

    private BallController lastBallToBumpMe;
    public UnityEvent<float> OnScoreAdded;

    private PlayerData playerData;

    [SerializeField] private GameState roundOverState;

    private bool _scoreSettedToplayerData;

    private void Start()
    {
        playerData.InRoundPlayerScore = 0;
    }
    private void OnEnable()
    {
        ballController.OnContactWithOtherBallController.AddListener(UpdateLastBallToBumpMe);
        roundOverState.OnGameStateActive += SetScoreToPlayerData;
    }
    private void OnDisable()
    {
        ballController.OnContactWithOtherBallController.RemoveListener(UpdateLastBallToBumpMe);
        if (lastBallToBumpMe != null)
        {
            ConnectToBallDeath(false);
        }
        roundOverState.OnGameStateActive -= SetScoreToPlayerData;
        SetScoreToPlayerData();

    }
    public void SetPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
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
        playerData.InRoundPlayerScore += 1;
        OnScoreAdded?.Invoke(playerData.InRoundPlayerScore);
    }
    private void SetScoreToPlayerData()
    {
        if(!_scoreSettedToplayerData)
        {
            playerData.TotalPlayerScore += playerData.InRoundPlayerScore;
            _scoreSettedToplayerData = true;
        }
    }
}
