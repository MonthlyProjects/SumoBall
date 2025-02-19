using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameState/GameStatePriority")]
public class GameStatePriority : ScriptableObject
{
    [Header("The State that has the priority is the one with the lowest index.")]
    public List<GameState> gameStatesSortedByPriority = new List<GameState>();
#if UNITY_EDITOR
    [EasyButtons.Button]
    void AddMissingGameStates()
    {
        string[] guids = AssetDatabase.FindAssets("t:GameState");

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameState gameState = AssetDatabase.LoadAssetAtPath<GameState>(assetPath);
            if (!gameStatesSortedByPriority.Contains(gameState))
            {
                gameStatesSortedByPriority.Add(gameState);
            }
        }
    }
#endif
}