using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameState/NewGameState")]
public class GameState : ScriptableObject
{
    #region Fields

    [Header("Only values with override at true will be considered when activating this state")]
    [Space(1)]
    public StateValues StateValues;

    [Tooltip("Determines if the event should be invoked when the state is activated or deactivated")]
    [SerializeField] private bool invokeEventOnActiveChange;

    [Space(3)]
    [Header("Determines if the state is currently active")]
    [Space(1)]
    private bool _isActive;

    #endregion

    #region Properties

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (_isActive == value)
            {
                return;
            }
            _isActive = value;
            
            if (invokeEventOnActiveChange)
            {
                if (_isActive)
                {
                    OnGameStateActive?.Invoke();
                }
                else
                {
                    OnGameStateInactive?.Invoke();
                }
            }
            
        }
    }

    #endregion
    
    #region Events

    public Action OnGameStateActive;
    
    public Action OnGameStateInactive;
    
    #endregion
}