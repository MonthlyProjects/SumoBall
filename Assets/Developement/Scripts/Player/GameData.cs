using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public List<PlayerData> playersData;

    public UnityEvent<PlayerData> OnPlayerAdd;

    public UnityEvent<PlayerData> OnPlayerRemove;

    public UnityEvent OnAllPlayersSpawn;

    public void Initialize ()
    {
        playersData = new List<PlayerData>();
    }

    public void AddPlayer (PlayerData playerData)
    {
        if(playersData == null)
        {
            playersData = new List<PlayerData> ();
        }

        if (playersData.Contains(playerData)) { return; }

        playersData.Add (playerData);
        OnPlayerAdd?.Invoke(playerData);
    }

    public void RemovePlayer (int index)
    {
        playersData.Remove(playersData[index]);
        OnPlayerRemove?.Invoke(playersData[index]);
    }
    public void LauchPlayers()
    {
        for(int i = 0; i < playersData.Count; i++)
        {
            PlayerData playerData = playersData[i];
            if (playerData.playerObject == null) { return; }

            playerData.playerObject.GetComponent<PlayerController>().LauchPlayer();
        }
    }

    public void StopPlayers()
    {
        for (int i = 0; i < playersData.Count; i++)
        {
            PlayerData playerData = playersData[i];
            if (playerData.playerObject == null) { return; }

            playerData.playerObject.GetComponent<PlayerController>().StopPlayer();
        }
    }

    public void LauchPlayer(PlayerData playerData)
    {
        if (playerData.playerObject == null) { return; }

        playerData.playerObject.GetComponent<PlayerController>().LauchPlayer();
    }
}
