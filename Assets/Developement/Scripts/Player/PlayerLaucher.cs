using UnityEngine;

public class PlayerLaucher : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private bool isGame;
    [SerializeField] private GameState inRound;

    private void OnEnable()
    {
        inRound.OnGameStateActive += LaunchPlayers;
        gameData.OnPlayerSpawn.AddListener(
            (playerData) => 
            {
                Debug.Log("Je suis appeler pour m activer");
                if (!isGame) { LauchPlayer(playerData); } 
            }
            );
    }
    private void OnDisable()
    {
        inRound.OnGameStateActive -= LaunchPlayers;
        gameData.OnPlayerSpawn.RemoveListener(
           (playerData) =>
           {
               if (!isGame) { LauchPlayer(playerData); }
           }
           );
    }

    public void LaunchPlayers ()
    {
        for (int i = 0; i < gameData.playersData.Count; i++)
        {
            LauchPlayer(gameData.playersData[i]);
        }
    }

    public void LauchPlayer (PlayerData playerData)
    {
        if (playerData.playerObject == null) { Debug.Log("Value is null"); return; }

        playerData.playerObject.GetComponent<PlayerController>().LauchPlayer();
    }

    public void StopPlayer(PlayerData playerData)
    {
        if (playerData.playerObject == null) { Debug.Log("Value is null"); return; }

        playerData.playerObject.GetComponent<PlayerController>().StopPlayer();
    }

}
