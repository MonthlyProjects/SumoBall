using UnityEngine;

public class ScriptableInitializeur : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private void Awake()
    {
        gameData.Initialize(); 
    }
}
