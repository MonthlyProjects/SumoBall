using System.Collections;
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
    private bool _finishHasBeenLaunched;

    private void OnEnable()
    {
        inRoundState.OnGameStateActive += ListenPlayersDeaths;
        numberOfPlayersLeftInRound = gameData.playersData.Count;
        Debug.Log("Number Of Player = " + numberOfPlayersLeftInRound);
    }
    private void OnDisable()
    {
        inRoundState.OnGameStateActive -= ListenPlayersDeaths;
        RemovePlayerDeathListeners();
    }

    private void ListenPlayersDeaths()
    {
        foreach(PlayerData playerData in gameData.playersData)
        {
            playerData.playerObject.GetComponent<BallHealth>().OnDeath.AddListener((v) => Remove1ToPlayerCount());
        }
    }
    private void RemovePlayerDeathListeners()
    {
        foreach (PlayerData playerData in gameData.playersData)
        {
            playerData.playerObject.GetComponent<BallHealth>().OnDeath.RemoveListener((v) => Remove1ToPlayerCount());
        }
    }

    private void Remove1ToPlayerCount()
    {
        numberOfPlayersLeftInRound--;
        Debug.Log("Players Left = " + numberOfPlayersLeftInRound);
        if(numberOfPlayersLeftInRound <= 1 && !_finishHasBeenLaunched) 
        {
            _finishHasBeenLaunched = true;
            StartCoroutine(LaunchFinishRound());
        }
    }
    IEnumerator LaunchFinishRound()
    {
        yield return new WaitForSeconds(0.5f);
        FinishRound();
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
            GameStateManager.Instance.RemoveState(roundOverState);
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
