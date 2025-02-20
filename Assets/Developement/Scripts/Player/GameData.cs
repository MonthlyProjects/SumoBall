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

}
