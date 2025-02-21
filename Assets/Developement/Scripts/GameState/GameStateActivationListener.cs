using UnityEngine;
using UnityEngine.Events;

public class GameStateActivationListener : MonoBehaviour
{
    [SerializeField] private GameState gameState;

    public UnityEvent OnStateActive;
    public UnityEvent OnStateInactive;

    private void OnEnable()
    {
        gameState.OnGameStateActive -= CallOnStateActive;
        gameState.OnGameStateInactive -= CallOnStateInactive;
        gameState.OnGameStateActive += CallOnStateActive;
        gameState.OnGameStateInactive += CallOnStateInactive;
    }
    private void OnDisable()
    {
        gameState.OnGameStateActive -= CallOnStateActive;
        gameState.OnGameStateInactive -= CallOnStateInactive;
    }
    
    void CallOnStateActive()
    {
        OnStateActive?.Invoke();
    }
    
    void CallOnStateInactive()
    {
        OnStateInactive?.Invoke();
    }
}
