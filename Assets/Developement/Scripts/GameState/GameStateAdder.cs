using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameStateAdder : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private bool addStateOnStart;
    [SerializeField] private GameStateReseter reseter;
    [SerializeField] private bool removeOnExitScene;

    public bool IsDone => throw new System.NotImplementedException();

    private void Start()
    {
        if(addStateOnStart)
        {
            StartCoroutine(WaitForGameStateNotNull());
        }
    }
    private void OnDisable()
    {
        if(removeOnExitScene)
        {
            RemoveState();
        }
    }
    IEnumerator WaitForGameStateNotNull()
    {
        //WaitFor gamestate to initialize
        while(GameStateManager.Instance == null)
        {
            yield return null;
        }

        //Wait for reseter if there is one to reset all states
        if(reseter != null)
        {
            while (!reseter.HasBeenReset)
            {
                yield return null;
            }
        }

        AddState();
    }

    [EasyButtons.Button]
    public void AddState()
    {
        if(GameStateManager.Instance == null)
        {
            Debug.LogError("GameStatemanager is null");
            return;
        }
        
        GameStateManager.Instance.AddState(gameState);
    }
    
    [EasyButtons.Button]
    public void RemoveState()
    {
        if(GameStateManager.Instance == null)
        {
            return;
        }
        
        GameStateManager.Instance.RemoveState(gameState);
    }
}
