using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RoundOverScoreInstancer : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private RoundOverScorePanelDisplayer ScoreUIPlayerPrefab;
    [SerializeField] private Transform ScoreUIPanel;
    private void OnEnable()
    {
        DisplayScore();
    }
    public void DisplayScore()
    {
        //dupliquer la liste sans reference direct a lancienne
        List<PlayerData> playersDataInOrder = new();

        foreach(PlayerData playerData in gameData.playersData)
        {
            playersDataInOrder.Add(playerData);
        }

        // Trier du plus grand au plus petit
        playersDataInOrder.Sort((a, b) => b.TotalPlayerScore.CompareTo(a.TotalPlayerScore));

        for(int i = 0; i < playersDataInOrder.Count; i++)
        {
            RoundOverScorePanelDisplayer panel = Instantiate(ScoreUIPlayerPrefab, ScoreUIPanel);
            panel.DisplayScore(playersDataInOrder[i], i+1);
        }
    }
}
