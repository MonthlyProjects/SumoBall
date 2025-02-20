using UnityEditor;
using UnityEngine;

public static class GameStateTools
{
    [MenuItem("Tools/GameState/Open Deboger")]
    public static void OpenPreferenceDefaultSaveState()
    {
        GameStateDeboger gameStateDeboger = Resources.Load<GameStateDeboger>("GameStateDeboger");
        if(gameStateDeboger != null)
        {
            AssetDatabase.OpenAsset(gameStateDeboger);
        }
        else
        {
            Debug.LogError("there is no asset named CurrentGameStateValuesViewer in resources folder and type of GameState");
        }
    }
}
