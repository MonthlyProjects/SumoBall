using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    public List<PlayerData> playersData;

    public void AddPlayer (PlayerData playerData)
    {
        if(playersData == null)
        {
            playersData = new List<PlayerData> ();
        }

        if (playersData.Contains(playerData)) { return; }

        playersData.Add (playerData);
    }

    public void RemovePlayer (int index)
    {
        playersData.Remove(playersData[index]);
    }
}
