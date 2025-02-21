using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameState inRoundState;
    [SerializeField] private GameState roundOverState;

    public UnityEvent OnRoundFinish;

    private int numberOfPlayersLeftInRound;

    private void OnEnable()
    {
        inRoundState.OnGameStateActive += ListenPlayersDeaths;
    }
    private void OnDisable()
    {
        inRoundState.OnGameStateActive -= ListenPlayersDeaths;
    }

    private void ListenPlayersDeaths()
    {
        foreach(PlayerData playerData in gameData.playersData)
        {
            playerData.playerObject.GetComponent<BallHealth>().OnDeath.AddListener((v) => Remove1ToPlayerCount());
        }
    }

    private void Remove1ToPlayerCount()
    {
        numberOfPlayersLeftInRound--;
        if(numberOfPlayersLeftInRound <= 1) 
        {
            FinishRound();
        }
    }
    private void FinishRound()
    {
        GameStateManager.Instance.RemoveState(inRoundState);
        GameStateManager.Instance.AddState(roundOverState);
        Debug.Log("RoundFinish");
        OnRoundFinish?.Invoke();
    }
    public void LaunchNextRound()
    {
        if(gameData.CurrentRound < gameData.TotalRoundsToPlay)
        {
            gameData.CurrentRound++;
            SceneManager.LoadScene(1);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
